using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign.UI
{
    public class TimeOverPresenter : AbstractPresenter
    {
        

        public override void Repaint()
        {
            
        }

        public void GameOver()
        {
            GameManager.Instance.SetMainMenu();
        }
    }
}