using Aiv.Fast2D;

namespace TopDownShooterAIV
{
    class TopDownShooter
    {
        
        public TopDownShooter()
        {
            GameManager.Init();

            var player = new Player();
            var rifle = new Rifle(player);
            var enemy = new Enemy();

            GameManager.AddGameObject(player);
            GameManager.AddGameObject(rifle);
            GameManager.AddGameObject(enemy);

        }

        public void Run()
        {
            while (GameManager.Window.IsOpened)
            {
                GameManager.Update();
                GameManager.Draw();

                GameManager.Window.Update();

                if (GameManager.Window.GetKey(KeyCode.Esc))
                {
                    break;
                }
            }
        }
    }
}
