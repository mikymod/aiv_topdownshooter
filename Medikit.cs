using Aiv.Fast2D;
using OpenTK;

namespace TopDownShooterAIV
{
    public class Medikit : GameObject
    {
        AnimatedSprite animatedSprite;

        SoundEffect healSFX;

        public Medikit()
        {
            texture = AssetsManager.GetTexture("medikit");

            sprite = new Sprite(16, 16);
            sprite.pivot = new Vector2(16 / 2, 16 / 2);

            collider = new BoxCollider(this, sprite.Width, sprite.Height);

            Enabled = false;

            animatedSprite = new AnimatedSprite(sprite, texture, this, 7, 16, 15, 12, false);
            animatedSprite.Play();

            healSFX = new SoundEffect(AssetsManager.GetClip("medikit_pickup"));
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
                healSFX.Play(0.25f);

                Enabled = false;
            }
        }
    }
}
