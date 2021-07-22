using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class Bullet : GameObject
    {
        private Rifle rifle;

        public Vector2 Direction { get; set; }
        public float Speed { get; set; }

        public Bullet(Rifle rifle) : base()
        {
            this.rifle = rifle;

            texture = new Texture("Assets/bullets.png");

            sprite = new Sprite(24, 24);
            sprite.position = rifle.Position;
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);

            Speed = 200;

            collider = new CircleCollider(this, 4);
        }

        public override void Update()
        {
            base.Update();

            sprite.position += Direction * Speed * GameManager.Window.DeltaTime;
        }

        public override void Draw()
        {
            base.Draw();

            sprite.DrawTexture(texture, 0, 0, 16, 16);
        }

        public override void OnCollide(Collision collision)
        {
            base.OnCollide(collision);

            if (collision.other is Enemy)
            {
                Enabled = false;
            }
        }
    }
}
