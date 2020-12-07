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