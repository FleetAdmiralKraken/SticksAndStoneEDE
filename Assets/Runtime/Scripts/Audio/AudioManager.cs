using UnityEngine;

namespace com.cringejam.sticksandstones
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private AudioSource audioSourcePrefab = default;
        [Header("Data")]
        [SerializeField] private int initialNumAudioSources = default;

        private AudioSourcePool audioSourcePool = default;

        private void Awake()
        {
            audioSourcePool = new AudioSourcePool(audioSourcePrefab, initialNumAudioSources);
        }

        public AudioSource RequestAudioSource() => audioSourcePool.RequestAudioSource();
        public void ReleaseAudioSource(AudioSource audioSource) => audioSourcePool.ReleaseAudioSource(audioSource);
    }
}
