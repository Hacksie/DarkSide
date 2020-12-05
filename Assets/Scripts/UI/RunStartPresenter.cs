using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign.UI
{
    public class RunStartPresenter : AbstractPresenter
    {
        

        public override void Repaint()
        {
            
        }

        public void Run()
        {
            GameManager.Instance.SetPlaying();
        }

        public void Leave()
        {
            GameManager.Instance.SetMainMenu();
        }
    }
}