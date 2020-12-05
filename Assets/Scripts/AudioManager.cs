//#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class AudioManager : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] private AudioSource footstepsSource = null;
        [SerializeField] private AudioSource fxSource = null;
        [SerializeField] private AudioSource musicSource = null;

        [Header("Clips")]
        [SerializeField] private AudioClip dash = null;
        [SerializeField] private List<AudioClip> boltFire = null;
        [SerializeField] private List<AudioClip> energyFire = null;
        [SerializeField] private List<AudioClip> footsteps = null;
        [SerializeField] private List<AudioClip> playMusic = null;

        private float footstepTimer = 0;


        public static AudioManager Instance { get; private set; }

        private AudioManager() => Instance = this;

        public void PlayBoltFire()
        {
            fxSource.clip = boltFire[Random.Range(0, boltFire.Count)];
            fxSource.Play();
        }

        public void PlayEnergyFire()
        {
            fxSource.clip = energyFire[Random.Range(0, energyFire.Count)];
            fxSource.Play();
        }

        public void PlayFootsteps()
        {
            if ((Time.time - GameManager.Instance.GameSettings.footstepTime) >= footstepTimer)
            {
                footstepTimer = Time.time;
                footstepsSource.clip = footsteps[Random.Range(0, footsteps.Count)];
                footstepsSource.Play();
            }
        }

        public void PlayDash()
        {
            footstepTimer = Time.time;
            footstepsSource.clip = dash;
            footstepsSource.Play();
        }
    }
}