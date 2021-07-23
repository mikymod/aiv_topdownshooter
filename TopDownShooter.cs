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

            var spawnPoints = new List<SpawnPoint>()
            {
                new SpawnPoint(new Vector2(100, 100)),
                new SpawnPoint(new Vector2(500, 100)),
                new SpawnPoint(new Vector2(100, 300)),
                new SpawnPoint(new Vector2(500, 300)),
            };

            GameManager.AddGameObject(player);
            GameManager.AddGameObject(rifle);
            GameManager.AddGameObject(medikit);
            GameManager.AddGameObject(powerup);

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                GameManager.AddGameObject(spawnPoints[i]);
            }

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
