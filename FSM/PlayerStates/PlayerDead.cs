using OpenTK;
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

        private float knockSpeed = 90f;

        private SoundEffect deathSFX;

        public PlayerDead(Player player, AnimatedSprite sprite)
        {
            this.player = player;
            this.sprite = sprite;

            deathSFX = new SoundEffect(AssetsManager.GetClip("player_death"));
        }

        public void OnAnimationEnd()
        {
            // end game
            Console.WriteLine("Dead");

            player.Velocity = Vector2.Zero;
        }

        public void OnDraw()
        {
            sprite.Draw();
        }

        public void OnEnter()
        {
            sprite.Play();

            deathSFX.Play(0.2f);

            player.Velocity = player.KnockDirection * knockSpeed;
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
