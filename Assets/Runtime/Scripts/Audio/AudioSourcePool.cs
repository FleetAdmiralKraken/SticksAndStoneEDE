using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace com.cringejam.sticksandstones
{
    public class AudioSourcePool
    {
        private const int InsufficientAudioSourcesIncrement = 5;

        private readonly AudioSource audioSourcePrefab = default;

        private readonly HashSet<AudioSource> audioSourceSet = default;
        private readonly HashSet<AudioSource> occupiedAudioSourceSet = default;

        public AudioSourcePool(AudioSource audioSourcePrefab, int numAudioSources)
        {
            this.audioSourcePrefab = audioSourcePrefab;

            audioSourceSet = new HashSet<AudioSource>();
            occupiedAudioSourceSet = new HashSet<AudioSource>();

            AddAudioSources(numAudioSources);
        }

        public void ReleaseAudioSource(AudioSource audioSource)
        {
            if (audioSource == null || !occupiedAudioSourceSet.Contains(audioSource))
                return;

            occupiedAudioSourceSet.Remove(audioSource);
        }

        public AudioSource RequestAudioSource() => GetUnoccupiedAudioSource();

        private void AddAudioSources(int num)
        {
            for (int i = 0; i < num; ++i)
                audioSourceSet.Add(Object.Instantiate(audioSourcePrefab));
        }

        private AudioSource GetUnoccupiedAudioSource()
        {
            if (audioSourceSet.Count == occupiedAudioSourceSet.Count)
                AddAudioSources(InsufficientAudioSourcesIncrement);

            foreach (var AudioSource in audioSourceSet)
            {
                if (!AudioSource.isPlaying)
                {
                    Assert.IsTrue(!occupiedAudioSourceSet.Contains(AudioSource));

                    occupiedAudioSourceSet.Add(AudioSource);

                    return AudioSource;
                }
            }

            return default;
        }
    }
}
