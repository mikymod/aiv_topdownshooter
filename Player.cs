using Aiv.Fast2D;
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
        private float speed = 50f;
        private int frameDim = 24;

        private float health = 5f;

        private bool damageGrace;
        private float damageGraceTime = 2f; // sec
        private float damageGraceTimer = 0f;

        public bool FacingRight
        {
            get => !sprite.FlipX;
        }

        public Player() : base()
        {
            texture = new Texture("Assets/player.png");

            sprite = new Sprite(frameDim, frameDim);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            sprite.position = new Vector2(GameManager.Window.Width / 2, GameManager.Window.Height / 2);

            collider = new BoxCollider(this, sprite.Width, sprite.Height);
        }

        public override void Update()
        {
            base.Update();

            if (!Enabled)
            {
                return;
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
        }

        public override void Draw()
        {
            base.Draw();

            if (!Enabled)
            {
                return;
            }

            sprite.DrawTexture(texture, 0, 0, frameDim, frameDim);
        }

        private void Move()
        {
            if (GameManager.Window.GetKey(KeyCode.D))
            {
                sprite.position += new Vector2(1, 0) * speed * GameManager.Window.DeltaTime;
                sprite.FlipX = false;
            }
            if (GameManager.Window.GetKey(KeyCode.A))
            {
                sprite.position += new Vector2(-1, 0) * speed * GameManager.Window.DeltaTime;
                sprite.FlipX = true;
            }
            if (GameManager.Window.GetKey(KeyCode.W))
            {
                sprite.position += new Vector2(0, -1) * speed * GameManager.Window.DeltaTime;
            }
            if (GameManager.Window.GetKey(KeyCode.S))
            {
                sprite.position += new Vector2(0, 1) * speed * GameManager.Window.DeltaTime;
            }
        }

        public override void OnCollide(Collision collision)
        {
            base.OnCollide(collision);

            if (collision.other is Enemy)
            {
                TakeDamage(1);
            }
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
