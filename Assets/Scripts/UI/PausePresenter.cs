using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign.UI
{
    public class PausePresenter : AbstractPresenter
    {
        

        public override void Repaint()
        {
            
        }

        public void Resume()
        {
            GameManager.Instance.SetPlaying();
        }

        public void Quit()
        {
            GameManager.Instance.SetMainMenu();
        }
    }
}