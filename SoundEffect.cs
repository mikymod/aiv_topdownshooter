using Aiv.Audio;

namespace TopDownShooterAIV
{
    class SoundEffect
    {
        AudioClip clip;
        AudioSource source;

        public float Volume { get { return source.Volume; } set { source.Volume = value; } }
        public float Pitch { get { return source.Pitch; } set { source.Pitch = value; } }

        public SoundEffect(AudioClip clip)
        {
            this.clip = clip;
            source = new AudioSource();
        }

        public void Play(float volume = 1f, float pitch = 1f)
        {
            source.Volume = volume;
            source.Pitch = pitch;
            source.Play(clip);
        }
    }
}
