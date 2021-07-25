using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooterAIV.FSM;

namespace TopDownShooterAIV
{
    class Player : GameObject
    {
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

        public StateMachine StateMachine { get; }

        private Vector2 velocity;
        public Vector2 Velocity { get => velocity; set => velocity = value; }

        public Vector2 KnockDirection { get; private set; }

        public Player() : base()
        {
            //texture = new Texture("Assets/player_idle.png");

            sprite = new Sprite(frameDim, frameDim);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            sprite.position = new Vector2(GameManager.Window.Width / 2, GameManager.Window.Height / 2);

            collider = new BoxCollider(this, sprite.Width, sprite.Height);

            health = initialHealth;

            Velocity = Vector2.Zero;

            StateMachine = new StateMachine(this);
            StateMachine.AddState(
                "Idle",
                new PlayerIdle(
                    this,
                    new AnimatedSprite(
                        sprite,
                        new Texture("Assets/player_idle.png"),
                        this, 4, frameDim, frameDim, 12, true
                    )
                )
            );
            StateMachine.AddState(
                "Run",
                new PlayerRun(
                    this,
                    new AnimatedSprite(
                        sprite,
                        new Texture("Assets/player_run.png"),
                        this, 6, frameDim, frameDim, 12, true
                    )
                )
            );

            StateMachine.AddState(
                "Damaged",
                new PlayerDamaged(
                    this,
                    new AnimatedSprite(
                        sprite,
                        new Texture("Assets/player_damaged.png"),
                        this, 2, frameDim, frameDim, 6, false
                    )
                )
            );
            StateMachine.AddState(
                "Dead",
                new PlayerDead(
                    this,
                    new AnimatedSprite(
                        sprite,
                        new Texture("Assets/player_dead.png"),
                        this, 3, frameDim, frameDim, 6, false
                    )
                )
            );

            StateMachine.SetInitialState("Idle");
        }

        public override void Update()
        {
            base.Update();

            StateMachine.Update();

            if (damageGrace)
            {
                damageGraceTimer += GameManager.Window.DeltaTime;
                if (damageGraceTimer > damageGraceTime)
                {
                    damageGrace = false;
                    damageGraceTimer = 0;
                }
            }

            Position += Velocity * GameManager.Window.DeltaTime;

            CheckBounds();
        }

        public override void Draw()
        {
            base.Draw();

            StateMachine.Draw();
        }

        public void Move()
        {
            // Horizontal movement
            if (GameManager.Window.GetKey(KeyCode.D))
            {
                velocity.X = speed;
                sprite.FlipX = false;
            }
            else if (GameManager.Window.GetKey(KeyCode.A))
            {
                velocity.X = -speed;
                sprite.FlipX = true;
            }
            else
            {
                velocity.Y = 0;
            }
            
            // Vertical movement
            if (GameManager.Window.GetKey(KeyCode.W))
            {
                velocity.Y = -speed;
            }
            else if (GameManager.Window.GetKey(KeyCode.S))
            {
                velocity.Y = speed;
            }
            else
            {
                velocity.Y = 0;
            }
        }

        private void CheckBounds()
        {
            if (Position.X < 0)
                sprite.position.X = 0;
            if (Position.X > GameManager.Window.Width * 2)
                sprite.position.X = GameManager.Window.Width * 2;
            if (Position.Y < 0)
                sprite.position.Y = 0;
            if (Position.Y > GameManager.Window.Height * 2)
                sprite.position.Y = GameManager.Window.Height * 2;
        }

        public override void OnCollide(Collision collision)
        {
            base.OnCollide(collision);

            if (collision.other is Enemy)
            {
                KnockDirection = (Position - collision.other.Position).Normalized();
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
                StateMachine.ChangeState("Dead");
            }
            else
            {
                StateMachine.ChangeState("Damaged");
            }
        }

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();

            StateMachine.OnAnimationEnd();
        }
    }
}
