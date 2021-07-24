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
        protected Collider collider;


        public virtual Vector2 Position
        {
            get { return sprite.position; }
            set { sprite.position = value; }
        }

        public Collider Collider { get => collider; }

        public float X { get { return sprite.position.X; } set { sprite.position.X = value; } }
        public float Y { get { return sprite.position.Y; } set { sprite.position.Y = value; } }

        public bool Enabled { get; set; }

        protected GameObject()
        {
            Enabled = true;
        }

        public virtual void Update() {}

        public virtual void Draw() {}

        public virtual void Destroy()
        {
            sprite = null;
            texture = null;
        }

        public virtual void OnCollide(Collision collision)
        {
            //Console.WriteLine($"{collision.collider} collides with {collision.other}");
        }

        public virtual void OnAnimationEnd()
        {

        }
    }
}
