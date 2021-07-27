using OpenTK;
using System;

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
            var result = Position.X < other.Position.X + other.Width &&
                         Position.X + Width > other.Position.X &&
                         Position.Y < other.Position.Y + other.Height &&
                         Position.Y + Height > other.Position.Y;

            if (result)
            {
                collisionInfo.type = CollisionType.RectsIntersection;

                // Evaluate delta for wall collisions
                var distance = other.Position - Position;
                float dx = Math.Abs(distance.X) - (other.halfWidth + halfWidth);
                float dy = Math.Abs(distance.Y) - (other.halfHeight + halfHeight);

                collisionInfo.delta = new Vector2(-dx, -dy);
            }

            return result;
        }

        public override bool Collides(CircleCollider other, ref Collision collisionInfo)
        {
            float deltaX = other.Position.X - Math.Max(Position.X - halfWidth, Math.Min(other.Position.X, Position.X + halfWidth));
            float deltaY = other.Position.Y - Math.Max(Position.Y - halfHeight, Math.Min(other.Position.Y, Position.Y + halfHeight));
            var result = (deltaX * deltaX + deltaY * deltaY) < (other.Radius * other.Radius);

            if (result)
            {
                collisionInfo.type = CollisionType.CircleRectIntersection;
            }

            return result;
        }
    }
}
