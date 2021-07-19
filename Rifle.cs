using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class Rifle
    {
        private Player player;

        private Sprite rifleSprite;

        private int frameDim = 24;
        private int offsetX = 8;

        public Rifle(Player player)
        {
            this.player = player;

            rifleSprite = new Sprite(frameDim, frameDim);
            rifleSprite.pivot = new Vector2(rifleSprite.Width / 2, rifleSprite.Height / 2);
            rifleSprite.position = player.Position;
        }

        public void Update()
        {
            rifleSprite.position = player.Position + new Vector2(offsetX, 0);
            var rifleDirection = RifleDirection();
            rifleSprite.Rotation = (float)Math.Atan2(rifleDirection.Y, rifleDirection.X);
            rifleSprite.DrawTexture(player.Texture, 24 * 5, 24 * 3, frameDim, frameDim);
        }

        private Vector2 RifleDirection()
        {
            var mouseAbsolutePos = GameManager.Window.MousePosition + GameManager.Window.CurrentCamera.position - GameManager.Window.CurrentCamera.pivot;
            return (mouseAbsolutePos - player.Position).Normalized();
        }
    }
}
