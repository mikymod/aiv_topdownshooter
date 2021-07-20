using Aiv.Fast2D;
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

        public static void Init()
        {
            Window = new Window(640, 480, "Top Down Shooter", false);
            Window.SetVSync(false);
            //Window.SetDefaultViewportOrthographicSize(48);

            Camera = new Camera();
            Window.SetCamera(Camera);

            Texture = new Texture("Assets/Player.png");
        }

        public static void Update()
        {
            gameObjects.ForEach((obj) => obj.Update());
        }

        public static void Draw()
        {
            gameObjects.ForEach((obj) => obj.Draw());
        }

        public static void AddGameObject(GameObject go) => gameObjects.Add(go);
    }
}
