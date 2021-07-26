using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class BackGround : GameObject
    {
        public BackGround()
        {
            texture = new Texture("Assets/background.png");

            sprite = new Sprite(texture.Width, texture.Height);
            //sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            //sprite.position = new Vector2(GameManager.Window.Width / 2, GameManager.Window.Height / 2);
        }

        public override void Draw()
        {
            base.Draw();

            sprite.DrawTexture(texture, 0, 0, texture.Width, texture.Height);
        }
    }
}
