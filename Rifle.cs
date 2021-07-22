using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class Rifle : GameObject
    {
        private Player player;

        private int frameDim = 24;
        private int offsetX = 8;

        private List<Bullet> bullets = new List<Bullet>();

        private int maxNumBullet = 16;
        private int numShoot = 0;

        private float shootCooldown = 0.25f;
        private float timer = 0;
        private bool shooted;

        public Rifle(Player player) : base()
        {
            this.player = player;

            texture = new Texture("Assets/rifle.png");

            sprite = new Sprite(frameDim, frameDim);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            sprite.position = player.Position;

            for (int i = 0; i < maxNumBullet; i++)
            {
                var bullet = new Bullet(this);
                bullet.Enabled = false;

                bullets.Add(bullet);
                GameManager.AddGameObject(bullet);
            }
        }

        public override void Update()
        {
            base.Update();

            sprite.position = player.Position + new Vector2(offsetX, 0);
            var rifleDirection = RifleDirection();
            sprite.Rotation = (float)Math.Atan2(rifleDirection.Y, rifleDirection.X);

            if (GameManager.Window.MouseLeft && !shooted)
            {
                Shoot();
            }

            if (shooted)
            {
                timer += GameManager.DeltaTime;
                if (timer >= shootCooldown)
                {
                    shooted = false;
                    timer = 0;
                }
            }
        }

        public override void Draw()
        {
            base.Draw();

            sprite.DrawTexture(texture);
        }

        private Vector2 RifleDirection()
        {
            var mouseAbsolutePos = GameManager.Window.MousePosition + GameManager.Window.CurrentCamera.position - GameManager.Window.CurrentCamera.pivot;
            return (mouseAbsolutePos - player.Position).Normalized();
        }

        private void Shoot()
        {
            var direction = RifleDirection();
            var bullet = bullets[numShoot % maxNumBullet];
            bullet.Enabled = true;
            bullet.Position = Position;
            bullet.Direction = direction;

            Console.WriteLine($"Fire {numShoot % maxNumBullet} Bullet. Total:{numShoot}");

            numShoot++;

            shooted = true;
        }
    }
}
