﻿using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    public static class GameManager
    {
        public static Window Window { get; private set; }
        public static Camera Camera { get; private set; }
        public static Texture Texture { get; private set; }

        public static float DeltaTime { get { return Window.DeltaTime; } }

        private static List<GameObject> gameObjects = new List<GameObject>();

        private static Collision collision = new Collision();

        public static void Init()
        {
            Window = new Window(640, 360, "Top Down Shooter", true);
            Window.SetResolution(320, 180);
            Window.SetVSync(false);

            Camera = new Camera(Window.OrthoWidth * 0.5f, Window.OrthoHeight * 0.5f);
            Camera.pivot = new Vector2(Window.OrthoWidth * 0.5f, Window.OrthoHeight * 0.5f);
            Window.SetCamera(Camera);

            Texture = new Texture("Assets/Player.png");
        }

        public static void Update()
        {
            CheckCollisions();
            gameObjects.ForEach((obj) => obj.Update());
        }

        public static void Draw()
        {
            gameObjects.ForEach((obj) => obj.Draw());
        }

        public static void AddGameObject(GameObject go) => gameObjects.Add(go);

        public static void CheckCollisions()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (!gameObjects[i].Enabled)
                {
                    continue;
                }

                for (int j = i + 1; j < gameObjects.Count; j++)
                {
                    if (!gameObjects[j].Enabled)
                    {
                        continue;
                    }

                    if (gameObjects[i].Collider == null || gameObjects[j].Collider == null)
                    {
                        continue;
                    }

                    if (gameObjects[i].Collider.Collides(gameObjects[j].Collider, ref collision))
                    {
                        collision.other = gameObjects[j];
                        gameObjects[i].OnCollide(collision);
                    }

                    if (gameObjects[j].Collider.Collides(gameObjects[i].Collider, ref collision))
                    {
                        collision.other = gameObjects[i];
                        gameObjects[j].OnCollide(collision);
                    }
                }
            }
        }
    }
}
