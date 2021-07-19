using Aiv.Fast2D;

namespace TopDownShooterAIV
{
    class TopDownShooter
    {
        private Window window;
        private Camera camera;

        private Player player;

        public TopDownShooter()
        {
            window = new Window(640, 480, "Top Down Shooter", false);
            camera = new Camera();
            window.SetCamera(camera);

            player = new Player(window);
        }

        void Update()
        {
            player.Update();
        }

        public void Run()
        {
            while (window.IsOpened)
            {
                Update();
                window.Update();
            }
        }

    }
}
