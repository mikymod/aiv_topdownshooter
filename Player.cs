﻿using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class Player : GameObject
    {
        private float speed = 50f;
        private int frameDim = 24;

        public bool FacingRight
        {
            get => !sprite.FlipX;
        }

        public Player() : base()
        {

            sprite = new Sprite(frameDim, frameDim);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            sprite.position = new Vector2(GameManager.Window.Width / 2, GameManager.Window.Height / 2);

            collider = new BoxCollider(this, sprite.Width, sprite.Height);
        }

        public override void Update()
        {
            base.Update();

            Move();
        }

        public override void Draw()
        {
            base.Draw();

            sprite.DrawTexture(GameManager.Texture, 0, 0, frameDim, frameDim);
        }

        private void Move()
        {
            if (GameManager.Window.GetKey(KeyCode.D))
            {
                sprite.position += new Vector2(1, 0) * speed * GameManager.Window.DeltaTime;
                sprite.FlipX = false;
            }
            if (GameManager.Window.GetKey(KeyCode.A))
            {
                sprite.position += new Vector2(-1, 0) * speed * GameManager.Window.DeltaTime;
                sprite.FlipX = true;
            }
            if (GameManager.Window.GetKey(KeyCode.W))
            {
                sprite.position += new Vector2(0, -1) * speed * GameManager.Window.DeltaTime;
            }
            if (GameManager.Window.GetKey(KeyCode.S))
            {
                sprite.position += new Vector2(0, 1) * speed * GameManager.Window.DeltaTime;
            }
        }
    }
}
