using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using System.Linq;

namespace HackedDesign.UI
{

    public class MainMenuPresenter : AbstractPresenter
    {
        [Header("Main")]
        [SerializeField] private GameObject defaultPanel = null;
        [SerializeField] private GameObject playPanel = null;
        [SerializeField] private GameObject optionsPanel = null;
        [SerializeField] private GameObject creditsPanel = null;

        [Header("Play")]
        [SerializeField] private UnityEngine.UI.Button[] slotButtons = null;
        [SerializeField] private UnityEngine.UI.Text[] slotTexts = null;
        [SerializeField] private Color selectedColor = Color.red;
        [SerializeField] private Color unselectedColor = Color.black;

        [SerializeField] private UnityEngine.UI.Slider sensitivitySlider = null;
        [SerializeField] private UnityEngine.UI.Text sensitivityText = null;
        [SerializeField] private UnityEngine.UI.Slider gunSlider = null;
        [SerializeField] private UnityEngine.UI.Text gunText = null;

        private MainMenuState state = MainMenuState.Default;

        // void Start()
        // {
        //     PopulateValues();
        // }

        public override void Repaint()
        {
            switch (state)
            {
                case MainMenuState.Play:
                    defaultPanel.SetActive(false);
                    playPanel.SetActive(true);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(false);

                    for (int i = 0; i < slotTexts.Length; i++)
                    {
                        slotTexts[i].text = (GameManager.Instance.gameSlots[i] == null || GameManager.Instance.gameSlots[i].newGame) ? "empty" : GameManager.Instance.gameSlots[i].saveName;
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
                    optionsPanel.SetActive(true);
                    creditsPanel.SetActive(false);
                    break;
                case MainMenuState.Credits:
                    defaultPanel.SetActive(false);
                    playPanel.SetActive(false);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(true);
                    break;
                case MainMenuState.Default:
                default:
                    defaultPanel.SetActive(true);
                    playPanel.SetActive(false);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(false);
                    break;
            }
        }

        public void PlayEvent()
        {
            Logger.Log(this, "Play event");
            state = MainMenuState.Play;
            //StartEvent();
        }



        public void OptionsEvent()
        {
            state = MainMenuState.Options;
        }

        public void CreditsEvent()
        {
            state = MainMenuState.Credits;
        }

        public void QuitEvent()
        {
            Application.Quit();
        }

        public void StartEvent()
        {
            GameManager.Instance.SetRunStart();
            // if (GameManager.Instance.gameSlots[GameManager.Instance.currentSlot] == null || GameManager.Instance.gameSlots[GameManager.Instance.currentSlot].newGame)
            // {
            //     Logger.Log(this, "New game");
            //     GameManager.Instance.NewGame();
            // }

            // GameManager.Instance.RandomGame = false;
            // //GameManager.Instance.SetPrelude();
            // GameManager.Instance.SetMissionSelect();
        }

        public void RandomEvent()
        {

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


        public void PopulateValues()
        {
            sensitivitySlider.value = GameManager.Instance.PlayerPreferences.mouseSensitivity;

            RepaintSensitivityText();
            RepaintGunText();
        }

        public void SensitivityChangedEvent()
        {
            GameManager.Instance.PlayerPreferences.mouseSensitivity = sensitivitySlider.value;
            RepaintSensitivityText();
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
            Random,
            Options,
            Screen,
            Credits,
            Quit
        }
    }
}