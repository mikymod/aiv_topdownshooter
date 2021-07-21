using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    public enum CollisionType { None, RectsIntersection, CirclesIntersection, CircleRectIntersection }

    public class Collision
    {
        public GameObject collider;
        public GameObject other;
        public CollisionType type;
        public Vector2 delta;
    }
}
