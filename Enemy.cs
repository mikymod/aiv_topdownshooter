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

        private AnimatedSprite animatedSprite;

        public Enemy(Player player) : base()
        {
            this.player = player;

            texture = new Texture("Assets/enemy.png");

            sprite = new Sprite(24, 24);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            //sprite.position = new Vector2(100, 100);

            collider = new BoxCollider(this, sprite.Width, sprite.Height);

            Enabled = false;

            animatedSprite = new AnimatedSprite(sprite, texture, this, 6, 24, 24, 12);
            animatedSprite.Play();
        }

        public override void Update()
        {
            base.Update();

            MoveTowardsPlayer();

            animatedSprite.Update();
        }

        public override void Draw()
        {
            base.Draw();

            if (!Enabled)
            {
                return;
            }

            //sprite.DrawTexture(texture, 0, 0, 24, 24);
            animatedSprite.Draw();
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
            health -= bullet.Damage;
            if (health <= 0)
            {
                Disable();
            }
        }

        private void MoveTowardsPlayer()
        {
            var direction = (player.Position - Position).Normalized();
            var distance = (player.Position - Position).LengthSquared;
            if (distance > 0.1f)
            {
                Position += direction * speed * GameManager.DeltaTime;
                sprite.FlipX = Math.Sign(direction.X) == -1;
            }
        }

        private void Disable()
        {
            Enabled = false;
            health = 5;
        }
    }
}
