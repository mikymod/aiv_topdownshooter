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
        public PowerUp()
        {
            texture = new Texture("Assets/powerup.png");

            sprite = new Sprite(16, 16);
            sprite.pivot = new Vector2(16 / 2, 16 / 2);

            collider = new BoxCollider(this, sprite.Width, sprite.Height);

            Enabled = false;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();

            sprite.DrawTexture(texture, 0, 0, 16, 16);
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
