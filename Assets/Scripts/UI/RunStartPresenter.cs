using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign.UI
{
    public class RunStartPresenter : AbstractPresenter
    {
        [SerializeField] UnityEngine.UI.Text seedText = null;
        [SerializeField] UnityEngine.UI.Text levelText = null;
        [SerializeField] UnityEngine.UI.Text lengthText = null;
        [SerializeField] UnityEngine.UI.Text difficultyText = null;
        [SerializeField] UnityEngine.UI.Text permaText = null;

        public override void Repaint()
        {
            seedText.text = GameManager.Instance.Data.seed.ToString();
            levelText.text = GameManager.Instance.Data.currentLevelIndex.ToString();
            lengthText.text = (8 + GameManager.Instance.Data.currentLevelIndex).ToString();
            difficultyText.text = GameManager.Instance.Data.difficulty;
            permaText.text = GameManager.Instance.Data.permadeath.ToString();
        }

       public void Run()
        {
            GameManager.Instance.SetPlaying();
        }

        public void Shop()
        {
            GameManager.Instance.CurrentState.Select();
            //GameManager.Instance.SetShop();
        }

        public void Leave()
        {
            GameManager.Instance.SetMainMenu();
        }
    }
}