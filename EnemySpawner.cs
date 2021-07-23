using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class EnemySpawner
    {
        private Player player;
        private List<SpawnPoint> spawnPoints;
        private List<Enemy> enemies;

        private float nextSpawnTime;

        private Random rand;

        public EnemySpawner(int listSize, List<SpawnPoint> spawnPoints, Player player)
        {
            this.spawnPoints = spawnPoints;
            this.player = player;
            enemies = new List<Enemy>();

            for (int i = 0; i < listSize; i++)
            {
                var enemy = new Enemy(this.player);
                enemies.Add(enemy);
                GameManager.AddGameObject(enemy);
            }

            rand = new Random(); 

            nextSpawnTime = (float)(rand.NextDouble() * 2 + 2);
        }

        public void Update()
        {
            nextSpawnTime -= GameManager.DeltaTime;
            if (nextSpawnTime <= 0)
            {
                SpawnEnemy();
                nextSpawnTime = (float)(rand.NextDouble() * 2 + 2);
            }
        }

        private void SpawnEnemy()
        {
            int spawnPointIndex = rand.Next(0, spawnPoints.Count);
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].Enabled)
                {
                    enemies[i].Position = spawnPoints[spawnPointIndex].Position;
                    enemies[i].Enabled = true;
                    break;
                }
            }
        }
    }
}
