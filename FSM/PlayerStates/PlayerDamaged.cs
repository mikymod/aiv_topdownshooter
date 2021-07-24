using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooterAIV.FSM;

namespace TopDownShooterAIV
{
    class PlayerDamaged : IState
    {
        private Player player;
        private AnimatedSprite sprite;

        private float knockSpeed = 70f;

        public PlayerDamaged(Player player, AnimatedSprite sprite)
        {
            this.player = player;
            this.sprite = sprite;
        }

        public void OnAnimationEnd()
        {
            player.StateMachine.ChangeState("Idle");
        }

        public void OnDraw()
        {
            sprite.Draw();
        }

        public void OnEnter()
        {
            sprite.Restart();

            player.Velocity = player.KnockDirection * knockSpeed;
        }

        public void OnExit()
        {
            sprite.Stop();
        }

        public void OnLogic()
        {
            sprite.Update();

            player.Position += player.Velocity * GameManager.DeltaTime;
        }
    }
}
