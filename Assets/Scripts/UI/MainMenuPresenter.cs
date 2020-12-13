using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using System.Linq;
using System;

namespace HackedDesign.UI
{

    public class MainMenuPresenter : AbstractPresenter
    {
        [SerializeField] private AudioMixer masterMixer = null;
        [Header("Main")]
        [SerializeField] private GameObject defaultPanel = null;
        [SerializeField] private GameObject playPanel = null;
        [SerializeField] private GameObject optionsPanel = null;
        [SerializeField] private GameObject controlsPanel = null;
        [SerializeField] private GameObject creditsPanel = null;
        [SerializeField] private GameObject newGamePanel = null;
        [SerializeField] private GameObject randomGamePanel = null;

        [Header("Play")]
        [SerializeField] private UnityEngine.UI.Button[] slotButtons = null;
        [SerializeField] private UnityEngine.UI.Text[] slotTexts = null;
        [SerializeField] private Color selectedColor = Color.red;
        [SerializeField] private Color unselectedColor = Color.black;
        [Header("New Game")]
        [SerializeField] private UnityEngine.UI.Toggle permadeathToggle = null;
        [Header("Random")]
        [SerializeField] private UnityEngine.UI.InputField seedInput = null;

        [Header("Options")]
        [SerializeField] private UnityEngine.UI.Slider masterSlider = null;
        [SerializeField] private UnityEngine.UI.Slider fxSlider = null;
        [SerializeField] private UnityEngine.UI.Slider musicSlider = null;
        [SerializeField] private UnityEngine.UI.Text masterVolumeText = null;
        [SerializeField] private UnityEngine.UI.Text fxVolumeText = null;
        [SerializeField] private UnityEngine.UI.Text musicVolumeText = null;
        [SerializeField] private UnityEngine.UI.Slider sensitivitySlider = null;
        [SerializeField] private UnityEngine.UI.Text sensitivityText = null;
        [SerializeField] private UnityEngine.UI.Toggle invertMouseToggle = null;
        [SerializeField] private UnityEngine.UI.Slider gunSlider = null;
        [SerializeField] private UnityEngine.UI.Text gunText = null;

        private MainMenuState state = MainMenuState.Default;

        // void Start()
        // {
        //     PopulateValues();
        // }

        public void Reset()
        {
            state = MainMenuState.Default;
        }

        public override void Repaint()
        {
            switch (state)
            {
                case MainMenuState.Play:
                    defaultPanel.SetActive(false);
                    playPanel.SetActive(true);
                    newGamePanel.SetActive(false);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(false);
                    randomGamePanel.SetActive(false);
                    controlsPanel.SetActive(false);

                    for (int i = 0; i < slotTexts.Length; i++)
                    {
                        slotTexts[i].text = (GameManager.Instance.gameSlots[i] == null) ? "empty" : GameManager.Instance.gameSlots[i].saveName + " Level:" + GameManager.Instance.gameSlots[i].currentLevelIndex + "\nDifficulty: " + GameManager.Instance.gameSlots[i].difficulty + (GameManager.Instance.gameSlots[i].permadeath ? " Permadeath!" : "");
                    }

                    for (int j = 0; j < slotButtons.Length; j++)
                    {
                        if (j == GameManager.Instance.currentSlot)
                        {
                            var block = slotButtons[j].colors;
                            block.normalColor = selectedColor;
                            slotButtons[j].colors = block;
                        }
                        else
                        {
                            var block = slotButtons[j].colors;
                            block.normalColor = unselectedColor;
                            slotButtons[j].colors = block;
                        }
                    }

                    break;
                case MainMenuState.Options:
                    defaultPanel.SetActive(false);
                    playPanel.SetActive(false);
                    newGamePanel.SetActive(false);
                    optionsPanel.SetActive(true);
                    creditsPanel.SetActive(false);
                    randomGamePanel.SetActive(false);
                    controlsPanel.SetActive(false);
                    break;
                case MainMenuState.Controls:
                    defaultPanel.SetActive(false);
                    playPanel.SetActive(false);
                    newGamePanel.SetActive(false);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(false);
                    randomGamePanel.SetActive(false);
                    controlsPanel.SetActive(true);
                    break;
                case MainMenuState.Credits:
                    defaultPanel.SetActive(false);
                    playPanel.SetActive(false);
                    newGamePanel.SetActive(false);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(true);
                    randomGamePanel.SetActive(false);
                    controlsPanel.SetActive(false);
                    break;
                case MainMenuState.NewGame:
                    defaultPanel.SetActive(false);
                    playPanel.SetActive(false);
                    newGamePanel.SetActive(true);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(false);
                    randomGamePanel.SetActive(false);
                    controlsPanel.SetActive(false);
                    break;
                case MainMenuState.Random:
                    defaultPanel.SetActive(false);
                    playPanel.SetActive(false);
                    newGamePanel.SetActive(false);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(false);
                    randomGamePanel.SetActive(true);
                    controlsPanel.SetActive(false);
                    break;
                case MainMenuState.Default:
                default:
                    defaultPanel.SetActive(true);
                    playPanel.SetActive(false);
                    newGamePanel.SetActive(false);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(false);
                    randomGamePanel.SetActive(false);
                    controlsPanel.SetActive(false);
                    break;
            }
        }

        public void PlayEvent()
        {
            GameManager.Instance.PlayerPreferences.Save();
            Logger.Log(this, "Play event");
            state = MainMenuState.Play;
            //StartEvent();
        }



        public void OptionsEvent()
        {
            GameManager.Instance.PlayerPreferences.Save();
            state = MainMenuState.Options;
        }

        public void CreditsEvent()
        {
            GameManager.Instance.PlayerPreferences.Save();
            state = MainMenuState.Credits;
        }

        public void QuitEvent()
        {
            GameManager.Instance.PlayerPreferences.Save();
            Application.Quit();
        }

        public void ControlsEvent()
        {
            state = MainMenuState.Controls;
            GameManager.Instance.PlayerPreferences.Save();
        }

        public void GraphicsEvent()
        {
            GameManager.Instance.PlayerPreferences.Save();
        }

        public void StartEvent()
        {
            if (GameManager.Instance.gameSlots[GameManager.Instance.currentSlot] == null)
            {
                Logger.Log(this, "New game");
                state = MainMenuState.NewGame;
            }
            else
            {
                GameManager.Instance.SetRunStart();
            }

            // GameManager.Instance.RandomGame = false;
            // //GameManager.Instance.SetPrelude();
            // GameManager.Instance.SetMissionSelect();
        }

        public void NewGameEvent()
        {
            //FIXME: Permadeath
            GameManager.Instance.SetRunStart();
        }

        public void RandomEvent()
        {
            state = MainMenuState.Random;
            seedInput.text = NewSeed().ToString();
        }

        public void Slot0Event()
        {
            GameManager.Instance.currentSlot = 0;
        }

        public void Slot1Event()
        {
            GameManager.Instance.currentSlot = 1;
        }

        public void Slot2Event()
        {
            GameManager.Instance.currentSlot = 2;
        }

        public void DeleteEvent()
        {
            GameManager.Instance.gameSlots[GameManager.Instance.currentSlot] = null;
        }


        public void NormalEvent()
        {
            GameManager.Instance.NewGame(NewSeed(), "Normal", this.permadeathToggle.isOn, false);
        }

        public void HardEvent()
        {
            GameManager.Instance.NewGame(NewSeed(), "Hard", this.permadeathToggle.isOn, false);
        }

        public void UltraEvent()
        {
            GameManager.Instance.NewGame(NewSeed(), "Ultra", this.permadeathToggle.isOn, false);
        }

        public void RandomNormalEvent()
        {
            GameManager.Instance.NewGame(Convert.ToInt32(seedInput.text), "Normal", true, true);
        }

        public void RandomHardEvent()
        {
            GameManager.Instance.NewGame(Convert.ToInt32(seedInput.text), "Hard", true, true);
        }

        public void RandomUltraEvent()
        {
            GameManager.Instance.NewGame(Convert.ToInt32(seedInput.text), "Ultra", true, true);
        }

        public void RefreshSeedEvent()
        {
            seedInput.text = NewSeed().ToString();
        }

        private int NewSeed()
        {
            return (int)System.DateTime.Now.Ticks;
        }



        public void PopulateValues()
        {
            sensitivitySlider.value = GameManager.Instance.PlayerPreferences.mouseSensitivity;
            invertMouseToggle.isOn = GameManager.Instance.PlayerPreferences.invertMouse;
            gunSlider.value = GameManager.Instance.PlayerPreferences.gunPosition;
            masterSlider.value = GameManager.Instance.PlayerPreferences.masterVolume;
            fxSlider.value = GameManager.Instance.PlayerPreferences.sfxVolume;
            musicSlider.value = GameManager.Instance.PlayerPreferences.musicVolume;

            RepaintSensitivityText();
            RepaintGunText();
            RepaintMasterText();
            RepaintFXText();
            RepaintMusicText();
        }

        public void MasterChangedEvent()
        {
            masterMixer.SetFloat("MasterVolume", masterSlider.value);
            GameManager.Instance.PlayerPreferences.masterVolume = masterSlider.value;
            RepaintMasterText();
            GameManager.Instance.PlayerPreferences.Save();
        }

        public void FXChangedEvent()
        {
            masterMixer.SetFloat("FXVolume", fxSlider.value);
            GameManager.Instance.PlayerPreferences.sfxVolume = fxSlider.value;
            RepaintFXText();
            GameManager.Instance.PlayerPreferences.Save();
        }

        public void MusicChangedEvent()
        {
            masterMixer.SetFloat("MusicVolume", musicSlider.value);
            GameManager.Instance.PlayerPreferences.musicVolume = musicSlider.value;
            RepaintMusicText();
            GameManager.Instance.PlayerPreferences.Save();
        }

        private void RepaintMasterText()
        {
            masterVolumeText.text = GameManager.Instance.PlayerPreferences.masterVolume.ToString("F0") + "db";
        }

        private void RepaintFXText()
        {
            fxVolumeText.text = GameManager.Instance.PlayerPreferences.sfxVolume.ToString("F0") + "db";
        }

        private void RepaintMusicText()
        {
            musicVolumeText.text = GameManager.Instance.PlayerPreferences.musicVolume.ToString("F0") + "db";
        }


        public void SensitivityChangedEvent()
        {
            GameManager.Instance.PlayerPreferences.mouseSensitivity = sensitivitySlider.value;
            RepaintSensitivityText();
            GameManager.Instance.PlayerPreferences.Save();
        }

        public void InvertMouseToggleEvent()
        {
            GameManager.Instance.PlayerPreferences.invertMouse = invertMouseToggle.isOn;
            GameManager.Instance.PlayerPreferences.Save();
        }

        public void GunChangedEvent()
        {
            GameManager.Instance.PlayerPreferences.gunPosition = Mathf.FloorToInt(gunSlider.value);
            RepaintGunText();
            GameManager.Instance.PlayerPreferences.Save();
        }


        private void RepaintSensitivityText() => sensitivityText.text = GameManager.Instance.PlayerPreferences.mouseSensitivity.ToString("F0");
        private void RepaintGunText()
        {
            switch (GameManager.Instance.PlayerPreferences.gunPosition)
            {
                case 0:
                    gunText.text = "left";
                    break;
                case 1:
                    gunText.text = "center";
                    break;
                case 2:
                    gunText.text = "right";
                    break;

            }
        }

        private enum MainMenuState
        {
            Default,
            Play,
            NewGame,
            Random,
            Options,
            Graphics,
            Controls,
            Screen,
            Credits,
            Quit
        }
    }
}