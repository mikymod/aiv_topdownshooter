using OpenTK;

namespace TopDownShooterAIV
{
    public abstract class Collider
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
        public abstract bool Collides(BoxCollider collider, ref Collision collisionInfo);
        public abstract bool Collides(CircleCollider collider, ref Collision collisionInfo);
    }
}
