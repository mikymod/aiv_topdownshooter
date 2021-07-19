using Aiv.Fast2D;

namespace TopDownShooterAIV
{
    class TopDownShooter
    {
        

        private Player player;

        public TopDownShooter()
        {
            GameManager.Init();

            player = new Player();
        }

        void Update()
        {
            player.Update();
        }

        public void Run()
        {
            while (GameManager.Window.IsOpened)
            {
                Update();
                GameManager.Window.Update();

                if (GameManager.Window.GetKey(KeyCode.Esc))
                {
                    break;
                }
            }
        }
    }
}
