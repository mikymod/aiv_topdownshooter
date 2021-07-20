using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    public abstract class GameObject
    {
        protected Sprite sprite;
        protected Texture texture;

        public virtual Vector2 Position
        {
            get { return sprite.position; }
            set { sprite.position = value; }
        }

        public float X { get { return sprite.position.X; } set { sprite.position.X = value; } }
        public float Y { get { return sprite.position.Y; } set { sprite.position.Y = value; } }

        public bool Enabled { get; set; }

        public virtual void Update() {}

        public virtual void Draw() {}

        public virtual void Destroy()
        {
            sprite = null;
            texture = null;
        }
    }
}
