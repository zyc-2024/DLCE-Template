using DG.Tweening;
using UnityEngine;

namespace DancingLineFanmade.Level
{
    public static class AudioManager
    {
        public static void PlayClip(AudioClip clip, float volume)
        {
            AudioSource audioSource = new GameObject("One shot sound: " + clip.name).AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
            Object.Destroy(audioSource.gameObject, clip.length);
        }

        public static AudioSource PlayTrack(AudioClip clip, float volume)
        {
            AudioSource audioSource = new GameObject(clip.name).AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
            return audioSource;
        }

        public static float Time
        {
            get => Player.Instance.soundTrack.time;
            set => Player.Instance.soundTrack.time = value;
        }

        public static float Pitch
        {
            get => Player.Instance.soundTrack.pitch;
            set => Player.Instance.soundTrack.pitch = value;
        }

        public static float Volume
        {
            get => Player.Instance.soundTrack.volume;
            set => Player.Instance.soundTrack.volume = value;
        }

        public static float Progress
        {
            get => Player.Instance.soundTrack.time / Player.Instance.soundTrack.clip.length;
        }

        public static void Stop()
        {
            Player.Instance.soundTrack.Stop();
        }

        public static void Play()
        {
            Player.Instance.soundTrack.Play();
        }

        public static Tween FadeOut(float volume, float duration)
        {
            return Player.Instance.soundTrack.DOFade(volume, duration).SetEase(Ease.Linear).OnComplete(new TweenCallback(Stop));
        }
    }
}