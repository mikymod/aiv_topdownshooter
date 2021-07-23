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
            var medikit = new Medikit();
            var powerup = new PowerUp();

            GameManager.AddGameObject(player);
            GameManager.AddGameObject(rifle);
            GameManager.AddGameObject(enemy);
            GameManager.AddGameObject(medikit);
            GameManager.AddGameObject(powerup);
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
