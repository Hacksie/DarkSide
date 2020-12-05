using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign.UI
{
    public class LevelOverPresenter : AbstractPresenter
    {
        

        public override void Repaint()
        {
            
        }

        public void NextLevel()
        {
            GameManager.Instance.SetMainMenu();
        }
    }
}