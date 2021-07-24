﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV
{
    class Animation
    {
        protected int numFrames;
        protected float frameDuration;
        protected bool isPlaying;
        protected int currentFrame;
        protected float elapsedTime;
        public bool IsPlaying { get { return isPlaying; } }
        public int FrameWidth { get; protected set; }
        public int FrameHeight { get; protected set; }

        public bool Loop;
        private bool isFinished;

        public Vector2 Offset { get; protected set; }

        public Animation(GameObject owner, int numFrames, int frameWidth, int frameHeight, float framesPerSecond, bool loop = true)
        {
            this.numFrames = numFrames;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            Loop = loop;

            this.frameDuration = 1 / framesPerSecond;

            isFinished = false;
        }

        public virtual void Play()
        {
            isPlaying = true;
        }

        public virtual void Restart()
        {
            currentFrame = 0;
            isPlaying = true;
        }

        public virtual void Stop()
        {
            isPlaying = false;
            currentFrame = 0;
            elapsedTime = 0;
        }

        public virtual void Pause()
        {
            isPlaying = false;
        }

        public virtual void Update()
        {
            isFinished = false;
            if (isPlaying)
            {
                elapsedTime += GameManager.DeltaTime;

                if (elapsedTime >= frameDuration)
                {
                    currentFrame++;
                    elapsedTime = 0;

                    if (currentFrame >= numFrames)
                    {
                        if (Loop)
                        {
                            currentFrame = 0;
                        }
                        else
                        {
                            OnAnimationEnd();
                            isFinished = true;
                            return;
                        }
                    }
                    Offset = new Vector2(FrameWidth * currentFrame, Offset.Y);
                }
            }
        }

        protected virtual void OnAnimationEnd()
        {
            isPlaying = false;
        }

        public bool AnimationIsFinished()
        {
            return isFinished;
        }
    }
}
