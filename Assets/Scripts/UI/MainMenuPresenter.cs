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
        [SerializeField] private GameObject randomPanel = null;
        [SerializeField] private GameObject optionsPanel = null;
        [SerializeField] private GameObject creditsPanel = null;

        private MainMenuState state = MainMenuState.Default;

        public override void Repaint()
        {
            switch (state)
            {
                case MainMenuState.Random:
                    defaultPanel.SetActive(false);
                    randomPanel.SetActive(true);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(false);
                break;
                case MainMenuState.Options:
                    defaultPanel.SetActive(false);
                    randomPanel.SetActive(false);
                    optionsPanel.SetActive(true);
                    creditsPanel.SetActive(false);
                break;
                case MainMenuState.Credits:
                    defaultPanel.SetActive(false);
                    randomPanel.SetActive(false);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(true);
                break;
                case MainMenuState.Default:
                default:
                    defaultPanel.SetActive(true);
                    randomPanel.SetActive(false);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(false);
                break;
            }
        }

        public void PlayEvent()
        {
            Logger.Log(this,"Play event");
            StartEvent();
        }   

        public void RandomEvent()
        {
            state = MainMenuState.Random;
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
            GameManager.Instance.SetPlaying();
            // if (GameManager.Instance.gameSlots[GameManager.Instance.currentSlot] == null || GameManager.Instance.gameSlots[GameManager.Instance.currentSlot].newGame)
            // {
            //     Logger.Log(this, "New game");
            //     GameManager.Instance.NewGame();
            // }

            // GameManager.Instance.RandomGame = false;
            // //GameManager.Instance.SetPrelude();
            // GameManager.Instance.SetMissionSelect();
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