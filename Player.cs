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
        private Texture texture;
        private Sprite playerSprite;

        private float speed = 50f;

        private int frameDim = 24;

        private Rifle rifle;

        public Vector2 Position
        {
            get => playerSprite.position;
            set => playerSprite.position = value;
        }

        public bool FacingRight
        {
            get => !playerSprite.FlipX;
        }

        public Texture Texture { get => texture; }

        public Player()
        {
            texture = new Texture("Assets/Player.png");

            playerSprite = new Sprite(frameDim, frameDim);
            playerSprite.pivot = new Vector2(playerSprite.Width / 2, playerSprite.Height / 2);
            playerSprite.position = new Vector2(GameManager.Window.Width / 2, GameManager.Window.Height / 2);

            rifle = new Rifle(this);
        }

        public void Update()
        {
            Move();

            if (GameManager.Window.MouseLeft)
            {
                Fire();
            }

            // TODO: Animation
            playerSprite.DrawTexture(texture, 0, 0, frameDim, frameDim);

            rifle.Update();
        }

        private void Move()
        {
            if (GameManager.Window.GetKey(KeyCode.D))
            {
                playerSprite.position += new Vector2(1, 0) * speed * GameManager.Window.DeltaTime;
                playerSprite.FlipX = false;
            }
            if (GameManager.Window.GetKey(KeyCode.A))
            {
                playerSprite.position += new Vector2(-1, 0) * speed * GameManager.Window.DeltaTime;
                playerSprite.FlipX = true;
            }
            if (GameManager.Window.GetKey(KeyCode.W))
            {
                playerSprite.position += new Vector2(0, -1) * speed * GameManager.Window.DeltaTime;
            }
            if (GameManager.Window.GetKey(KeyCode.S))
            {
                playerSprite.position += new Vector2(0, 1) * speed * GameManager.Window.DeltaTime;
            }
        }

        private Vector2 RifleDirection()
        {
            var mouseAbsolutePos = GameManager.Window.MousePosition + GameManager.Window.CurrentCamera.position - GameManager.Window.CurrentCamera.pivot;
            return (mouseAbsolutePos - playerSprite.position).Normalized();
        }

        private void Fire()
        {
            var direction = RifleDirection();
            Console.WriteLine("Fire! : " + direction);
        }
    }
}
