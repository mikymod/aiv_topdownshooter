﻿using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class Player : GameObject
    {
        enum PlayerState
        {
            Idle,
            Run
        }

        private float speed = 50f;
        private int frameDim = 24;

        private float initialHealth = 5f;
        private float health;
        public float Health { get => health; }

        private bool damageGrace;
        private float damageGraceTime = 2f; // sec
        private float damageGraceTimer = 0f;

        private List<PowerUp> powerUps = new List<PowerUp>();
        public int PowerUpLevel { get => powerUps.Count; }

        public bool FacingRight
        {
            get => !sprite.FlipX;
        }

        PlayerState currentState;
        Dictionary<PlayerState, AnimatedSprite> animatedSprites = new Dictionary<PlayerState, AnimatedSprite>();
        AnimatedSprite currentAnimatedSprite;

        public Player() : base()
        {
            texture = new Texture("Assets/player_idle.png");

            sprite = new Sprite(frameDim, frameDim);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            sprite.position = new Vector2(GameManager.Window.Width / 2, GameManager.Window.Height / 2);

            collider = new BoxCollider(this, sprite.Width, sprite.Height);

            health = initialHealth;

            currentState = PlayerState.Idle;

            // Sprites init
            animatedSprites.Add(PlayerState.Idle, new AnimatedSprite(
                sprite,
                new Texture("Assets/player_idle.png"),
                this, 4, frameDim, frameDim, 12, true
            ));
            animatedSprites.Add(PlayerState.Run, new AnimatedSprite(
                sprite,
                new Texture("Assets/player_run.png"),
                this, 6, frameDim, frameDim, 12, true
            ));

            currentAnimatedSprite = animatedSprites[currentState];
            currentAnimatedSprite.Play();
        }

        public override void Update()
        {
            base.Update();

            if (GameManager.Window.GetKey(KeyCode.D) || GameManager.Window.GetKey(KeyCode.A) || GameManager.Window.GetKey(KeyCode.W) || GameManager.Window.GetKey(KeyCode.S))
            {
                // FIXME: need FSM
                currentState = PlayerState.Run;
                currentAnimatedSprite = animatedSprites[currentState];
                currentAnimatedSprite.Play();
            }
            else
            {
                // FIXME: need FSM
                currentState = PlayerState.Idle;
                currentAnimatedSprite = animatedSprites[currentState];
                currentAnimatedSprite.Play();
            }

            Move();

            if (damageGrace)
            {
                damageGraceTimer += GameManager.Window.DeltaTime;
                if (damageGraceTimer > damageGraceTime)
                {
                    damageGrace = false;
                    damageGraceTimer = 0;
                }
            }


            currentAnimatedSprite.Update();
        }

        public override void Draw()
        {
            base.Draw();

            currentAnimatedSprite.Draw();
        }

        private void Move()
        {
            if (GameManager.Window.GetKey(KeyCode.D))
            {
                Position += new Vector2(1, 0) * speed * GameManager.Window.DeltaTime;
                sprite.FlipX = false;
            }
            if (GameManager.Window.GetKey(KeyCode.A))
            {
                Position += new Vector2(-1, 0) * speed * GameManager.Window.DeltaTime;
                sprite.FlipX = true;
            }
            if (GameManager.Window.GetKey(KeyCode.W))
            {
                Position += new Vector2(0, -1) * speed * GameManager.Window.DeltaTime;
            }
            if (GameManager.Window.GetKey(KeyCode.S))
            {
                Position += new Vector2(0, 1) * speed * GameManager.Window.DeltaTime;
            }

            CheckBounds();
        }

        private void CheckBounds()
        {
            if (Position.X < 0)
                sprite.position.X = 0;
            if (Position.X > GameManager.Window.Width)
                sprite.position.X = GameManager.Window.Width;
            if (Position.Y < 0)
                sprite.position.Y = 0;
            if (Position.Y > GameManager.Window.Height)
                sprite.position.Y = GameManager.Window.Height;
        }

        public override void OnCollide(Collision collision)
        {
            base.OnCollide(collision);

            if (collision.other is Enemy)
            {
                TakeDamage(1);
            }
            if (collision.other is Medikit)
            {
                RestoreHealth(1);
            }
            if (collision.other is PowerUp)
            {
                PickPowerUp((PowerUp)collision.other);
            }
        }

        private void PickPowerUp(PowerUp powerUp) => powerUps.Add(powerUp);

        private void RestoreHealth(float restore)
        {
            health = Math.Min(health + restore, initialHealth);
            Console.WriteLine($"Current health: {health}");
        }

        private void TakeDamage(float damage)
        {
            if (damageGrace)
            {
                return;
            }

            Console.WriteLine("Damage Taken");

            health -= damage;
            damageGrace = true;

            if (health <= 0)
            {
                Enabled = false;
            }
        }
    }
}
