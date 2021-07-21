using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class Enemy : GameObject
    {
        public Enemy() : base()
        {
            texture = new Texture("Assets/enemy.png");

            sprite = new Sprite(24, 24);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            sprite.position = new Vector2(100, 100);

            collider = new BoxCollider(this, sprite.Width, sprite.Height);
        }

        public override void Draw()
        {
            base.Draw();

            sprite.DrawTexture(texture, 0, 0, 24, 24);
        }

        //public override void OnCollide(Collision collision)
        //{
        //    base.OnCollide(collision);

        //    if (collision.other is Bullet)
        //    {
        //        Console.WriteLine("SBAM");
        //    }
            
        //    Console.WriteLine("SBAM");
        //}
    }
}
