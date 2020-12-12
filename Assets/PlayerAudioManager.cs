﻿#if UNITY_STANDALONE
using Gameplay.VR.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.VR.Feedbacks
{
    public class PlayerAudioManager : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("General")] AudioSource playerAudioSource;
        [SerializeField] [FoldoutGroup("General")] float volume = 0.5f;

        [SerializeField] [FoldoutGroup("Shooting")] AudioClip gunshotClip;
        [SerializeField] [FoldoutGroup("Shooting")] AudioClip[] ricochetClips;
        AudioClip lastRicochetClip; // to avoid repeating the sound
        int attempts = 3; // 1-out-of-3 chance to replay the same sound 
        [SerializeField] [FoldoutGroup("Shooting")] AudioClip[] hitEnvironmentClips;
        AudioClip lastEnvironmentClip;

        [SerializeField] [FoldoutGroup("Teleportation")] AudioClip teleportationSFX;
        [SerializeField] [FoldoutGroup("Teleportation")] AudioClip[] teleportationWooshSFX;
        AudioClip lastWooshSFX;

        [SerializeField] [FoldoutGroup("GameOver")] AudioClip gameOverAlarm;

        private void Awake()
        {
            playerAudioSource = FindObjectOfType<VR_TeleportBehaviour>().gameObject.GetComponent<AudioSource>();
        }

        public void GE_GunshotSFX()
        {
            playerAudioSource.PlayOneShot(gunshotClip, volume);
        }

        public void GE_GunshotRicochetSFX()
        {
            playerAudioSource.PlayOneShot(lastRicochetClip = RandomRicochet(ricochetClips, lastRicochetClip), volume);
        }

        public void GE_HitEnvironementSFX()
        {
            playerAudioSource.PlayOneShot(lastRicochetClip = RandomRicochet(ricochetClips, lastRicochetClip), volume);
            playerAudioSource.PlayOneShot(lastEnvironmentClip = RandomRicochet(hitEnvironmentClips, lastEnvironmentClip), volume);
        }

        public void GE_TeleportationSFX()
        {
            playerAudioSource.PlayOneShot(teleportationSFX);
            playerAudioSource.PlayOneShot(lastWooshSFX = RandomRicochet(teleportationWooshSFX, lastWooshSFX), volume);
        }

        public void GE_GameOverAlarmSFX()
        {
            playerAudioSource.PlayOneShot(gameOverAlarm, volume);
        }

        AudioClip RandomRicochet(AudioClip[] clipArray, AudioClip previousClip)
        {
            AudioClip ricochetClip = clipArray[Random.Range(0, clipArray.Length)];

            while (ricochetClip == previousClip && attempts > 0)
            {
                ricochetClip = clipArray[Random.Range(0, clipArray.Length)];
                attempts--;
            }
            return ricochetClip;
        }
    }
} 
#endif