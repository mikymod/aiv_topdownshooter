using Aiv.Fast2D;
using OpenTK;
using System.Collections.Generic;

namespace TopDownShooterAIV
{
    class TopDownShooter
    {
        EnemySpawner enemySpawner;

        public TopDownShooter()
        {
            GameManager.Init();

            var player = new Player();
            var rifle = new Rifle(player);
            var medikit = new Medikit();
            var powerup = new PowerUp();

            GameManager.AddGameObject(player);
            GameManager.AddGameObject(rifle);
            GameManager.AddGameObject(medikit);
            GameManager.AddGameObject(powerup);

            var spawnPoints = new List<Vector2>()
            {
                new Vector2(100, 100),
                new Vector2(300, 100),
                new Vector2(100, 300),
                new Vector2(300, 300),
            };

            enemySpawner = new EnemySpawner(20, spawnPoints, player);
        }

        public void Run()
        {
            while (GameManager.Window.IsOpened)
            {
                GameManager.Update();
                GameManager.Draw();

                enemySpawner.Update();

                GameManager.Window.Update();

                if (GameManager.Window.GetKey(KeyCode.Esc))
                {
                    break;
                }
            }
        }
    }
}
