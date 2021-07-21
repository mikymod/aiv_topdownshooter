using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    public class CircleCollider : Collider
    {
        public float Radius { get; }

        public CircleCollider(GameObject owner, float radius) : base(owner)
        {
            Radius = radius;
        }


        public override bool Collides(Collider collider, ref Collision collisionInfo)
        {
            return collider.Collides(this, ref collisionInfo);
        }

        public override bool Collides(BoxCollider collider, ref Collision collisionInfo)
        {
            throw new NotImplementedException();
        }

        public override bool Collides(CircleCollider collider, ref Collision collisionInfo)
        {
            var distance = Position - collider.Position;
            var squaredDistance = Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
            return squaredDistance < Radius + collider.Radius;
        }
    }
}
