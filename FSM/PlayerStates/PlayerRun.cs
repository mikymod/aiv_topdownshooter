using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooterAIV.FSM;

namespace TopDownShooterAIV
{
    class PlayerRun : IState
    {
        private Player player;
        private AnimatedSprite sprite;

        public PlayerRun(Player player, AnimatedSprite sprite)
        {
            this.player = player;
            this.sprite = sprite;
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

            player.Move();

            if (!(GameManager.Window.GetKey(KeyCode.D) || GameManager.Window.GetKey(KeyCode.A) || GameManager.Window.GetKey(KeyCode.W) || GameManager.Window.GetKey(KeyCode.S)))
            {
                player.StateMachine.ChangeState("Idle");
            } 
        }
    }
}
