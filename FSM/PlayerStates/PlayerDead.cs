using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooterAIV.FSM;

namespace TopDownShooterAIV
{
    class PlayerDead : IState
    {
        private Player player;
        private AnimatedSprite sprite;

        public PlayerDead(Player player, AnimatedSprite sprite)
        {
            this.player = player;
            this.sprite = sprite;
        }

        public void OnAnimationEnd()
        {
            // end game
            Console.WriteLine("Dead");
        }

        public void OnDraw()
        {
            sprite.Draw();
        }

        public void OnEnter()
        {
            sprite.Play();
        }

        public void OnExit()
        {
            sprite.Stop();
        }

        public void OnLogic()
        {
            sprite.Update();
        }
    }
}
