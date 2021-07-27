using Aiv.Audio;
using Aiv.Fast2D;
using OpenTK;
using System.Collections.Generic;

namespace TopDownShooterAIV
{
    class TopDownShooter
    {
        EnemySpawner enemySpawner;
        private ItemSpawner itemSpawner;

        private AudioSource bgSource;

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
                // TODO: avoid magic values
                new SpawnPoint(new Vector2(100, 100)),
                new SpawnPoint(new Vector2(640, 100)),
                new SpawnPoint(new Vector2(1180, 100)),
                new SpawnPoint(new Vector2(100, 360)),
                new SpawnPoint(new Vector2(1180, 360)),
                new SpawnPoint(new Vector2(100, 620)),
                new SpawnPoint(new Vector2(640, 620)),
                new SpawnPoint(new Vector2(1180, 620)),
            };
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                GameManager.AddGameObject(spawnPoints[i]);
            }

            enemySpawner = new EnemySpawner(40, spawnPoints, player);
            itemSpawner = new ItemSpawner(20, player);

            Player.OnPlayerDeath += enemySpawner.Disable;
            Player.OnPlayerDeath += itemSpawner.Disable;
            Player.OnPlayerDeath += () => rifle.Enabled = false;

            // GUI
            GuiHealth guiHealth = new GuiHealth(player, new Vector2(20, 20));
            GameManager.AddGameObject(guiHealth);

            // Follow Camera
            GameManager.Camera = new FollowCamera(GameManager.Window.OrthoWidth * 0.5f, GameManager.Window.OrthoHeight * 0.5f, player);
            GameManager.Camera.pivot = new Vector2(GameManager.Window.OrthoWidth * 0.5f, GameManager.Window.OrthoHeight * 0.5f);
            GameManager.Window.SetCamera(GameManager.Camera);

            // Background music
            bgSource = new AudioSource();
            bgSource.Volume = 0.2f;
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

                bgSource.Stream(AssetsManager.GetClip("music_bg"), GameManager.DeltaTime, true);

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

            AssetsManager.AddClip("rifle_shot", "Assets/rifle_shot.wav");
            AssetsManager.AddClip("enemy_hurt", "Assets/enemy_hurt.wav");
            AssetsManager.AddClip("item_spawn", "Assets/item_spawn.wav");
            AssetsManager.AddClip("powerup_pickup", "Assets/powerup_pickup.wav");
            AssetsManager.AddClip("medikit_pickup", "Assets/medikit_pickup.wav");
            AssetsManager.AddClip("player_hurt", "Assets/player_hurt.wav");
            AssetsManager.AddClip("player_death", "Assets/player_death.wav");

            AssetsManager.AddClip("music_bg", "Assets/music_bg.ogg");
        }
    }
}
