using Aiv.Fast2D;
using OpenTK;

namespace TopDownShooterAIV
{
    class Heart : GameObject
    {
        public bool Filled { get; set; }

        public Heart()
        {
            texture = new Texture("Assets/gui/heart.png");

            sprite = new Sprite(16, 16);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            sprite.Camera = GameManager.GuiCamera;
        }

        public override void Draw()
        {
            base.Draw();

            var offsetX = Filled ? 0 : 16;
            sprite.DrawTexture(texture, offsetX, 0, 16, 16);
        }
    }
}
