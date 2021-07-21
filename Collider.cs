using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    abstract class Collider
    {
        public GameObject GameObject;

        public Vector2 Offset;
        public Vector2 Position { get => GameObject.Position + Offset; }

        public Collider(GameObject owner)
        {
            GameObject = owner;
            Offset = Vector2.Zero;
        }

        public abstract bool Collides(Collider collider, ref Collision collisionInfo);
    }
}
