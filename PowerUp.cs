using Aiv.Fast2D;
using OpenTK;

namespace TopDownShooterAIV
{
    class PowerUp : GameObject
    {
        public const float BuffTime = 10f;

        private AnimatedSprite animatedSprite;

        private SoundEffect pickUpSFX;

        public PowerUp()
        {
            texture = AssetsManager.GetTexture("powerup");

            sprite = new Sprite(16, 16);
            sprite.pivot = new Vector2(16 / 2, 16 / 2);

            collider = new BoxCollider(this, sprite.Width, sprite.Height);

            Enabled = false;

            animatedSprite = new AnimatedSprite(sprite, texture, this, 7, 16, 16, 12, false);
            animatedSprite.Play();

            pickUpSFX = new SoundEffect(AssetsManager.GetClip("powerup_pickup"));
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
                pickUpSFX.Play(0.25f);

                Enabled = false;
            }
        }
    }
}
