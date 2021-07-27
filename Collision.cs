using OpenTK;

namespace TopDownShooterAIV
{
    public enum CollisionType { None, RectsIntersection, CirclesIntersection, CircleRectIntersection }

    public class Collision
    {
        public GameObject other;
        public CollisionType type;
        public Vector2 delta;
    }
}
