using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class GuiHealth : GameObject
    {

        private Player player;

        private int initialNumberOfHearts;
        private List<Heart> hearts;

        private int numberOfActiveHearts;
        private int itemPosX;
        private int itemPosY;
        private float itemsHorizontalDistance;


        public GuiHealth(Player player, Vector2 position) : base()
        {
            this.player = player;
            hearts = new List<Heart>();

            initialNumberOfHearts = (int)player.Health;
            for (int i = 0; i < initialNumberOfHearts; i++)
            {
                var heart = new Heart();
                hearts.Add(heart);
                GameManager.AddGameObject(heart);
            }

            numberOfActiveHearts = initialNumberOfHearts;

            itemPosX = itemPosY = 20;
        }

        public override void Update()
        {
            base.Update();

            numberOfActiveHearts = (int)player.Health;

            for (int i = 0; i < hearts.Count; i++)
            {
                hearts[i].Position = new Vector2(20 + i * itemPosX, itemPosY);
                hearts[i].Filled = i < player.Health;
            }
        }
    }
}
