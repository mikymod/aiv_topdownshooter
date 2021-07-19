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
        public static float DeltaTime { get { return Window.DeltaTime; } }

        public static void Init()
        {
            Window = new Window(640, 480, "Top Down Shooter", true);
            Window.SetVSync(false);
            //Window.SetDefaultViewportOrthographicSize(48);

            Camera = new Camera();
            Window.SetCamera(Camera);
        }
    }
}
