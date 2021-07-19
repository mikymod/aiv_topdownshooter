using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class Player
    {
        private Window window;
        private Texture texture;
        private Sprite sprite;
        private float speed = 50f;

        private int frameDim = 24;


        public Player(Window window)
        {
            this.window = window;
            texture = new Texture("Assets/Player.png");
            sprite = new Sprite(24, 24);
            sprite.position = new Vector2(window.Width / 2, window.Height / 2);
        }

        public void Update()
        {
            Move();

            if (window.MouseLeft)
            {
                Fire();
            }

            // TODO: Animation
            sprite.DrawTexture(texture, 0, 0, frameDim, frameDim);
        }

        private void Move()
        {
            if (window.GetKey(KeyCode.D))
            {
                sprite.position += new Vector2(1, 0) * speed * window.DeltaTime;
                sprite.FlipX = false;
            }
            if (window.GetKey(KeyCode.A))
            {
                sprite.position += new Vector2(-1, 0) * speed * window.DeltaTime;
                sprite.FlipX = true;
            }
            if (window.GetKey(KeyCode.W))
            {
                sprite.position += new Vector2(0, -1) * speed * window.DeltaTime;
            }
            if (window.GetKey(KeyCode.S))
            {
                sprite.position += new Vector2(0, 1) * speed * window.DeltaTime;
            }
        }

        private void Fire()
        {
            var mouseAbsolutePos = window.MousePosition + window.CurrentCamera.position - window.CurrentCamera.pivot;
            var direction = (mouseAbsolutePos - sprite.position).Normalized();
            Console.WriteLine("Fire! : " + direction);
        }
    }
}
