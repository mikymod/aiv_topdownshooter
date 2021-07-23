using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class Enemy : GameObject
    {
        private Player player;

        private float health = 5f;

        private float speed = 50;

        public Enemy(Player player) : base()
        {
            this.player = player;

            texture = new Texture("Assets/enemy.png");

            sprite = new Sprite(24, 24);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            sprite.position = new Vector2(100, 100);

            collider = new BoxCollider(this, sprite.Width, sprite.Height);
        }

        public override void Update()
        {
            base.Update();

            //Position = Vector2.Lerp(Position, player.Position, GameManager.Window.DeltaTime);
            MoveTowardsPlayer();
        }

        public override void Draw()
        {
            base.Draw();

            if (!Enabled)
            {
                return;
            }

            sprite.DrawTexture(texture, 0, 0, 24, 24);
        }

        public override void OnCollide(Collision collision)
        {
            base.OnCollide(collision);

            if (collision.other is Bullet)
            {
                TakeDamage((Bullet)collision.other);
            }
        }

        private void TakeDamage(Bullet bullet)
        {
            Console.WriteLine($"Damage Taken: {bullet.Damage}");
            health -= bullet.Damage;
            if (health <= 0)
            {
                Enabled = false;
            }
        }

        private void MoveTowardsPlayer()
        {
            var direction = (player.Position - Position).Normalized();
            var distance = (player.Position - Position).LengthSquared;
            if (distance > 0.1f)
            {
                Position += direction * speed * GameManager.DeltaTime;
            }
        }
    }
}
