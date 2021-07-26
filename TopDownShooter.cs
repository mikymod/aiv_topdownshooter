using Aiv.Fast2D;
using OpenTK;
using System.Collections.Generic;

namespace TopDownShooterAIV
{
    class TopDownShooter
    {
        EnemySpawner enemySpawner;
        private ItemSpawner itemSpawner;

        public TopDownShooter()
        {
            GameManager.Init();

            AssetsManager.Init();
            LoadAssets();

            var background = new BackGround();
            GameManager.AddGameObject(background);

            var player = new Player();
            var rifle = new Rifle(player);

            GameManager.AddGameObject(player);
            GameManager.AddGameObject(rifle);

            var spawnPoints = new List<SpawnPoint>()
            {
                new SpawnPoint(new Vector2(100, 100)),
                new SpawnPoint(new Vector2(500, 100)),
                new SpawnPoint(new Vector2(100, 300)),
                new SpawnPoint(new Vector2(500, 300)),
            };
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                GameManager.AddGameObject(spawnPoints[i]);
            }

            enemySpawner = new EnemySpawner(20, spawnPoints, player);

            itemSpawner = new ItemSpawner(20, player);

            // GUI
            GuiHealth guiHealth = new GuiHealth(player, new Vector2(20, 20));
            GameManager.AddGameObject(guiHealth);

            // Follow Camera
            GameManager.Camera = new FollowCamera(GameManager.Window.OrthoWidth * 0.5f, GameManager.Window.OrthoHeight * 0.5f, player);
            GameManager.Camera.pivot = new Vector2(GameManager.Window.OrthoWidth * 0.5f, GameManager.Window.OrthoHeight * 0.5f);
            GameManager.Window.SetCamera(GameManager.Camera);
        }

        public void Run()
        {
            while (GameManager.Window.IsOpened)
            {
                GameManager.Update();
                GameManager.Draw();

                enemySpawner.Update();

                itemSpawner.Update();

                ((FollowCamera)(GameManager.Camera)).Update();

                GameManager.Window.Update();

                if (GameManager.Window.GetKey(KeyCode.Esc))
                {
                    break;
                }
            }
        }

        private void LoadAssets()
        {
            AssetsManager.AddTexture("background", "Assets/background.png");
            AssetsManager.AddTexture("bullet", "Assets/bullets.png");
            AssetsManager.AddTexture("enemy", "Assets/enemy.png");
            AssetsManager.AddTexture("medikit", "Assets/medikit.png");
            AssetsManager.AddTexture("player_damaged", "Assets/player_damaged.png");
            AssetsManager.AddTexture("player_dead", "Assets/player_dead.png");
            AssetsManager.AddTexture("player_idle", "Assets/player_idle.png");
            AssetsManager.AddTexture("player_run", "Assets/player_run.png");
            AssetsManager.AddTexture("powerup", "Assets/powerup.png");
            AssetsManager.AddTexture("rifle", "Assets/rifle.png");
            AssetsManager.AddTexture("spawn_points", "Assets/spawn_points.png");
            AssetsManager.AddTexture("heart", "Assets/gui/heart.png");

            AssetsManager.AddClip("rifle_shot", "Assets/shot.wav");
        }
    }
}
