using Aiv.Fast2D;
using OpenTK;

namespace TopDownShooterAIV
{
    class Bullet : GameObject
    {
        public enum BulletType
        {
            Normal = 1,
            Big = 2,
        }

        public Rifle Rifle { get; private set; }

        public Vector2 Direction { get; set; }
        public float Speed { get; set; }

        public BulletType Type { get; set; }
        private float damage = 1f;
        private float damageMultiplier = 1f;

        public float Damage { get => damage * damageMultiplier; }

        public Bullet(Rifle rifle) : base()
        {
            this.Rifle = rifle;

            texture = AssetsManager.GetTexture("bullet");

            sprite = new Sprite(24, 24);
            sprite.position = rifle.Position;
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);

            Speed = 200;

            collider = new CircleCollider(this, 4);

            Type = BulletType.Normal;
        }

        public override void Update()
        {
            base.Update();

            EvaluateDamage();

            sprite.position += Direction * Speed * GameManager.Window.DeltaTime;
        }

        public override void Draw()
        {
            base.Draw();

            int offsetX;
            int offsetY;

            switch (Type)
            {
                case BulletType.Big:
                    offsetX = 16;
                    offsetY = 0;
                    break;
                case BulletType.Normal:
                default:
                    offsetX = 0;
                    offsetY = 0;
                    break;
            }

            sprite.DrawTexture(texture, offsetX, offsetY, 16, 16);
        }

        public override void OnCollide(Collision collision)
        {
            base.OnCollide(collision);

            if (collision.other is Enemy)
            {
                Enabled = false;
            }
        }

        // Very simple logic, just fo testing
        public void EvaluateDamage() => damageMultiplier = (float)(Type);
    }
}
