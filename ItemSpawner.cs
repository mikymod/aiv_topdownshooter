using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class ItemSpawner
    {
        private Player player;
        private List<GameObject> items; // Powerup or Medikit

        private float nextSpawnTime;

        private Random rand;

        public ItemSpawner(int listSize, Player player)
        {
            this.player = player;

            rand = new Random();

            nextSpawnTime = (float)(rand.NextDouble() * 2 + 10);

            items = new List<GameObject>();
            for (int i = 0; i < listSize; i++)
            {
                GameObject item;
                if (i % 2 == 0)
                {
                    item = new PowerUp();
                }
                else
                {
                    item = new Medikit();
                }
                items.Add(item);
                GameManager.AddGameObject(item);
            }
        }

        public void Update()
        {
            nextSpawnTime -= GameManager.DeltaTime;
            if (nextSpawnTime <= 0)
            {
                SpawnItem();
                nextSpawnTime = (float)(rand.NextDouble() * 2 + 10);
            }
        }

        private void SpawnItem()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (!items[i].Enabled)
                {
                    items[i].Position = GetRandomPositionNearPlayer();
                    items[i].Enabled = true;
                    break;
                }
            }
        }

        private Vector2 GetRandomPositionNearPlayer()
        {
            // TODO: Check if position is available or inside bounds
            return player.Position + new Vector2(rand.Next(0, GameManager.Window.Width / 2), rand.Next(0, GameManager.Window.Height / 2));
        }
    }
}
