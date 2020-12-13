//#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class AudioManager : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] private AudioSource dashSource = null;
        [SerializeField] private AudioSource goSource = null;
        [SerializeField] private AudioSource weaponSource = null;
        [SerializeField] private AudioSource pickupSource = null;
        //[SerializeField] private AudioSource fxSource = null;
        [SerializeField] private AudioSource musicSource = null;
        [SerializeField] private AudioSource timeOverSource = null;
        [SerializeField] private AudioSource deadSource = null;
        [SerializeField] private AudioSource loserSource = null;

        [Header("Clips")]
        [SerializeField] private AudioClip dash = null;
        [SerializeField] private List<AudioClip> boltFire = null;
        [SerializeField] private List<AudioClip> energyFire = null;
        [SerializeField] private List<AudioClip> footsteps = null;
        [SerializeField] private List<AudioClip> playMusic = null;
        [SerializeField] private AudioClip pickup = null;
        [SerializeField] private AudioClip success = null;
        [SerializeField] private AudioClip deathMusic = null;
        [SerializeField] private AudioClip gameOver = null;
        [SerializeField] private AudioClip timeOver = null;
        [SerializeField] private AudioClip loser = null;
        [SerializeField] private AudioClip waiting = null;
        [SerializeField] private AudioClip go = null;


        public static AudioManager Instance { get; private set; }

        private AudioManager() => Instance = this;

        public void PlayWaitingMusic()
        {
            musicSource.clip = waiting;
            musicSource.Play();
        }

        public void PlaySuccessMusic()
        {
            musicSource.clip = success;
            musicSource.Play();
        }

        public void PlayPickup()
        {
            pickupSource.clip = pickup;
            pickupSource.Play();
        }

        public void PlayDeathMusic()
        {
            musicSource.clip = deathMusic;
            musicSource.Play();
        }

        public void PlayDeath()
        {
            deadSource.clip = gameOver;
            deadSource.Play();
        }

        public void PlayTimeOver()
        {
            timeOverSource.clip = timeOver;
            timeOverSource.Play();
        }

        public void PlayLoser()
        {
            loserSource.clip = loser;
            loserSource.Play();
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

        public void PlayDash()
        {
            dashSource.clip = dash;
            dashSource.Play();
        }

        public void PlayRandomGameMusic()
        {
            musicSource.clip = playMusic[Random.Range(0, playMusic.Count)];
            musicSource.loop = true;
            musicSource.Play();
        }

        public void PlayMusic()
        {
            musicSource.Play();
        }

        public void PauseMusic()
        {
            musicSource.Pause();
        }


        public void StopMusic()
        {
            musicSource.Stop();
        }
    }
}