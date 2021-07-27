using OpenTK;
using System;
using System.Collections.Generic;

namespace TopDownShooterAIV
{
    class ItemSpawner
    {
        private int listSize;
        private Player player;
        private List<GameObject> items; // Powerup or Medikit

        private float nextSpawnTime;

        private Random rand;

        SoundEffect spawnSFX;
        private bool enabled;

        public ItemSpawner(int listSize, Player player)
        {
            this.listSize = listSize;
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

            spawnSFX = new SoundEffect(AssetsManager.GetClip("item_spawn"));

            enabled = true;
        }

        public void Update()
        {
            if (!enabled)
            {
                return;
            }

            nextSpawnTime -= GameManager.DeltaTime;
            if (nextSpawnTime <= 0)
            {
                SpawnItem();
                nextSpawnTime = (float)(rand.NextDouble() * 2 + 10);
            }
        }

        private void SpawnItem()
        {
            var index = rand.Next(0, listSize);

            do
            {
                if (items[index].Enabled)
                {
                    continue;
                }

                items[index].Enabled = true;
                items[index].Position = GetRandomPositionNearPlayer();
            }
            while (!items[index].Enabled);

            spawnSFX.Play(0.2f);
        }

        private Vector2 GetRandomPositionNearPlayer()
        {
            // TODO: Check if position is available or inside bounds
            return player.Position + new Vector2(rand.Next(0, GameManager.Window.Width / 2), rand.Next(0, GameManager.Window.Height / 2));
        }

        public void Disable()
        {
            enabled = false;

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Enabled = false;
            }
        }
    }
}
