using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;

namespace TopDownShooterAIV
{
    class Rifle : GameObject
    {
        public Player Player { get; private set; }

        private int frameDim = 24;
        private int offsetX = 8;

        private List<Bullet> bullets = new List<Bullet>();

        private int maxNumBullet = 16;
        private int numShoot = 0;

        private float shootCooldown = 0.25f;
        private float timer = 0;
        private bool shooted;

        SoundEffect shootSFX;

        public Rifle(Player player) : base()
        {
            this.Player = player;

            texture = AssetsManager.GetTexture("rifle");

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

            shootSFX = new SoundEffect(AssetsManager.GetClip("rifle_shot"));
        }

        public override void Update()
        {
            base.Update();

            sprite.position = Player.Position + new Vector2(offsetX, 0);
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

            if (!Player.Enabled)
            {
                Enabled = false;
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
            return (mouseAbsolutePos - Player.Position).Normalized();
        }

        private void Shoot()
        {
            var direction = RifleDirection();
            var bullet = bullets[numShoot % maxNumBullet];
            bullet.Enabled = true;
            bullet.Position = Position;
            bullet.Direction = direction;
            bullet.Type = Player.PoweredUp ? Bullet.BulletType.Big : Bullet.BulletType.Normal;

            Console.WriteLine($"Fire {numShoot % maxNumBullet} Bullet. Total:{numShoot}");

            numShoot++;

            shooted = true;

            shootSFX.Play(Player.PoweredUp ? 0.35f : 0.2f);
        }
    }
}
