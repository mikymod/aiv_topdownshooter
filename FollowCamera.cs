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
        private Random rand;

        private float speed = 5f;
        private bool earthquakeStarted = false;
        private float earthquakeDuration = 0.5f;
        private float earthquakeTimer = 0f;

        public FollowCamera(GameObject target) : base()
        {
            this.target = target;
            rand = new Random();
        }

        public FollowCamera(float x, float y, GameObject target) : base(x, y)
        {
            this.target = target;
            rand = new Random();
        }

        public void Update()
        {
            position = Vector2.Lerp(position, target.Position, speed * GameManager.DeltaTime);

            if (earthquakeStarted)
            {
                EarthQuake(1.2f);

                earthquakeTimer += GameManager.DeltaTime;
                if (earthquakeTimer >= earthquakeDuration)
                {
                    earthquakeStarted = false;
                    earthquakeTimer = 0f;
                }
            }

            FixBounds();
        }

        private void FixBounds()
        {
            position.X = MathHelper.Clamp(position.X, 0, GameManager.Window.Width * 2);
            position.Y = MathHelper.Clamp(position.Y, 0, GameManager.Window.Height * 2);
        }

        private void EarthQuake(float magnitude)
        {
            float x = (float)(rand.NextDouble() * magnitude);
            float y = (float)(rand.NextDouble() * magnitude);
            position = new Vector2(target.Position.X + x, target.Position.Y + y);
        }

        public void StartEarthQuake() => earthquakeStarted = true;
    }
}
