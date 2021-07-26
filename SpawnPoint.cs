using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    public class SpawnPoint : GameObject
    {
        public SpawnPoint(Vector2 position)
        {
            texture = AssetsManager.GetTexture("spawn_points");

            sprite = new Sprite(texture.Width, texture.Height);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);

            Position = position;
            
            Enabled = true;
        }

        public override void Draw()
        {
            base.Draw();

            sprite.DrawTexture(texture);
        }
    }
}
