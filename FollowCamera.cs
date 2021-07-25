using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class FollowCamera : Camera
    {
        private GameObject target;
        private float speed = 5f;

        public FollowCamera(GameObject target) : base()
        {
            this.target = target;
        }

        public FollowCamera(float x, float y, GameObject target) : base(x, y)
        {
            this.target = target;
        }

        public void Update()
        {
            position = Vector2.Lerp(position, target.Position, speed * GameManager.DeltaTime);
            FixBounds();
        }

        private void FixBounds()
        {
            position.X = MathHelper.Clamp(position.X, 0, GameManager.Window.Width * 2);
            position.Y = MathHelper.Clamp(position.Y, 0, GameManager.Window.Height * 2);
        }
    }
}
