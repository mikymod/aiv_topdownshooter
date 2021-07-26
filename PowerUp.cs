using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class PowerUp : GameObject
    {
        private float buffTime = 10f;
        private AnimatedSprite animatedSprite;

        public float BuffTime { get => buffTime; }

        public PowerUp()
        {
            texture = AssetsManager.GetTexture("powerup");

            sprite = new Sprite(16, 16);
            sprite.pivot = new Vector2(16 / 2, 16 / 2);

            collider = new BoxCollider(this, sprite.Width, sprite.Height);

            Enabled = false;

            animatedSprite = new AnimatedSprite(sprite, texture, this, 7, 16, 16, 12, false);
            animatedSprite.Play();
        }

        public override void Update()
        {
            base.Update();

            animatedSprite.Update();
        }

        public override void Draw()
        {
            base.Draw();

            //sprite.DrawTexture(texture, 0, 0, 16, 16);
            animatedSprite.Draw();
        }

        public override void OnCollide(Collision collision)
        {
            base.OnCollide(collision);

            if (collision.other is Player)
            {
                Enabled = false;
            }
        }
    }
}
