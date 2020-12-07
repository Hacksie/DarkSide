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
        [SerializeField] private AudioSource dashSource = null;
        [SerializeField] private AudioSource goSource = null;
        [SerializeField] private AudioSource weaponSource = null;
        //[SerializeField] private AudioSource fxSource = null;
        [SerializeField] private AudioSource musicSource = null;

        [Header("Clips")]
        [SerializeField] private AudioClip dash = null;
        [SerializeField] private List<AudioClip> boltFire = null;
        [SerializeField] private List<AudioClip> energyFire = null;
        [SerializeField] private List<AudioClip> footsteps = null;
        [SerializeField] private List<AudioClip> playMusic = null;
        [SerializeField] private AudioClip waiting = null;
        [SerializeField] private AudioClip go = null;

        private float footstepTimer = 0;


        public static AudioManager Instance { get; private set; }

        private AudioManager() => Instance = this;

        public void PlayWaitingMusic()
        {
            musicSource.clip = waiting;
            musicSource.Play();
        }

        public void PlayGo()
        {
            goSource.clip = go;
            goSource.Play();
        }        

        public void PlayBoltFire()
        {
            weaponSource.clip = boltFire[Random.Range(0, boltFire.Count)];
            weaponSource.Play();
        }

        public void PlayEnergyFire()
        {
            weaponSource.clip = energyFire[Random.Range(0, energyFire.Count)];
            weaponSource.Play();
        }

        public void PlayFire(AudioClip clip)
        {
            weaponSource.clip = clip;
            weaponSource.Play();
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

        public void StopMusic()
        {
            musicSource.Stop();
        }
    }
}