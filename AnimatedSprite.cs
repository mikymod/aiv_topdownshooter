using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class AnimatedSprite : Animation
    {
        Texture texture;
        Sprite sprite;
        GameObject owner;

        public AnimatedSprite(Sprite sprite, Texture texture, GameObject owner, int numFrames, int frameWidth, int frameHeight, float framesPerSecond, bool loop = true)
            : base(owner, numFrames, frameWidth, frameHeight, framesPerSecond, loop)
        {
            this.sprite = sprite;
            this.texture = texture;
            this.owner = owner;
        }


        public void Draw() => sprite.DrawTexture(texture, (int)Offset.X, (int)Offset.Y, FrameWidth, FrameHeight);
 
        protected override void OnAnimationEnd()
        {
            base.OnAnimationEnd();

            owner.OnAnimationEnd();
        }
    }
}
