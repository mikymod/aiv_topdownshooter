using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class Bullet
    {
        private Rifle rifle;

        private Sprite sprite;

        public Vector2 Direction { get; set; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; }

        public Bullet(Rifle rifle)
        {
            this.rifle = rifle;

            sprite = new Sprite(24, 24);
            sprite.position = rifle.Position;
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);

            Speed = 200;
        }

        public void Update()
        {
            sprite.position += Direction * Speed * GameManager.Window.DeltaTime;
            sprite.DrawTexture(GameManager.Texture, 24 * 4, 24 * 3, 24, 24);
        }
    }
}
