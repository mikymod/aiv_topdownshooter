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

        SoundEffect hurtSFX;


        public PlayerDamaged(Player player, AnimatedSprite sprite)
        {
            this.player = player;
            this.sprite = sprite;

            hurtSFX = new SoundEffect(AssetsManager.GetClip("player_hurt"));
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

            hurtSFX.Play(0.2f);
        }

        public void OnExit()
        {
            sprite.Stop();

            player.Velocity = Vector2.Zero;
        }

        public void OnLogic()
        {
            sprite.Update();

            player.Position += player.Velocity * GameManager.DeltaTime;
        }
    }
}
