using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    public class BoxCollider : Collider
    {
        protected float halfWidth;
        protected float halfHeight;

        public float Width { get { return halfWidth * 2; } }
        public float Height { get { return halfHeight * 2; } }

        public BoxCollider(GameObject owner, float width, float height) : base(owner)
        {
            halfWidth = width * 0.5f;
            halfHeight = height * 0.5f;
        }

        public override bool Collides(Collider collider, ref Collision collisionInfo)
        {
            return collider.Collides(this, ref collisionInfo);
        }

        public override bool Collides(BoxCollider other, ref Collision collisionInfo)
        {
            return Position.X < other.Position.X + other.Width &&
                   Position.X + Width > other.Position.X &&
                   Position.Y < other.Position.Y + other.Height &&
                   Position.Y + Height > other.Position.Y;
        }

        public override bool Collides(CircleCollider collider, ref Collision collisionInfo)
        {
            throw new NotImplementedException();
        }
    }
}
