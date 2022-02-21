using UnityEngine;
using UnityEngine.Audio;

namespace com.cringejam.sticksandstones
{
    public class BGSoundAlarm : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Transform audioListenerRoot = default;
        [SerializeField] private AudioManager audioManager = default;
        [SerializeField] private AudioMixer audioMixer = default;
        [SerializeField] private AudioClip alarmAudioClip = default;
        [Header("Data")]
        [SerializeField] private float minimumPitch = default;
        [SerializeField] private float maximumPitch = default;
        [SerializeField] private float minimumVolume = default;
        [SerializeField] private float maximumVolume = default;
        [SerializeField] private float alarmPeriod = default;

        private AudioSource alarmAudioSource = default;
        private float alarmLerpValue = default;

        private float periodTimer = default;

        public float AlarmLerpValue { get => alarmLerpValue; set => alarmLerpValue = Mathf.Clamp01(value); }

        private void Start()
        {
            alarmAudioSource = audioManager.RequestAudioSource();

            alarmAudioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BG Alarm Group")[0];
            alarmAudioSource.clip = alarmAudioClip;

            periodTimer = alarmPeriod;
        }

        private void Update()
        {
            HandleAlarmAudio();
        }

        private void HandleAlarmAudio()
        {
            if (!alarmAudioSource.isPlaying)
            {
                if (periodTimer > alarmPeriod / Mathf.Lerp(minimumPitch, maximumPitch, AlarmLerpValue))
                {
                    periodTimer = 0f;

                    AlarmLerpValue += 0.05f;

                    alarmAudioSource.pitch = Mathf.Lerp(minimumPitch, maximumPitch, alarmLerpValue);
                    alarmAudioSource.volume = Mathf.Lerp(minimumVolume, maximumVolume, alarmLerpValue);
                    alarmAudioSource.transform.position = audioListenerRoot.position;
                                        
                    alarmAudioSource.Play();
                }
                else
                    periodTimer += Time.deltaTime;
            }
        }
    }
}
