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
        public GameObject Collider;
        public Vector2 Delta;
        public CollisionType Type;
    }
}
