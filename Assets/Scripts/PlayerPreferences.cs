
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

namespace HackedDesign
{
    public class PlayerPreferences
    {
        public int resolutionWidth;
        public int resolutionHeight;
        public int resolutionRefresh;
        public int fullScreen;
        public float mouseSensitivity;
        public float mouseYSensitivity;
        public float masterVolume;
        public float sfxVolume;
        public float musicVolume;
        public bool invertMouse;
        public int gunPosition;

        public PlayerPreferences()
        {
        }

        public void Save()
        {
            PlayerPrefs.SetInt("ResolutionWidth", resolutionWidth);
            PlayerPrefs.SetInt("ResolutionHeight", resolutionHeight);
            PlayerPrefs.SetInt("ResolutionRefresh", resolutionRefresh);

            PlayerPrefs.SetInt("FullScreen", fullScreen);
            PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
            PlayerPrefs.SetFloat("FXVolume", sfxVolume);
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        }

        public void Load()
        {
            resolutionWidth = PlayerPrefs.GetInt("ResolutionWidth", Screen.currentResolution.width);
            resolutionHeight = PlayerPrefs.GetInt("ResolutionHeight", Screen.currentResolution.height);
            resolutionRefresh = PlayerPrefs.GetInt("ResolutionRefresh", Screen.currentResolution.refreshRate);
            fullScreen = PlayerPrefs.GetInt("FullScreen", (int)Screen.fullScreenMode);
            mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 100);
            masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0);
            sfxVolume = PlayerPrefs.GetFloat("FXVolume", 0);
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0);
            SetPreferences();
        }

        public void SetPreferences()
        {
            // this.mixer.SetFloat("MasterVolume", this.masterVolume);
            // this.mixer.SetFloat("FXVolume", this.sfxVolume);
            // this.mixer.SetFloat("MusicVolume", this.musicVolume);
            
            //Screen.SetResolution(resolutionWidth, resolutionHeight, (FullScreenMode) fullScreen);

            //Resolution scr = Screen.resolutions.FirstOrDefault(r => r.width == this.resolutionWidth && r.height == this.resolutionHeight && r.refreshRate == this.resolutionRefresh);

            //Logger.Log("Player Preferences", scr.width.ToString(), " x ", scr.height.ToString());
            //Screen.SetResolution(scr.width, scr.height, (FullScreenMode)this.fullScreen, scr.refreshRate);
        }
    }
}
