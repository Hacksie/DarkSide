using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign.UI
{
    public class DeadPresenter : AbstractPresenter
    {
        

        public override void Repaint()
        {
            
        }

        public void Quit()
        {
            if(GameManager.Instance.Data.permadeath)
            {
                GameManager.Instance.SetMainMenu();
            }
            else 
            {
                //FIXME: reset health etc
                GameManager.Instance.SetRunStart();
            }
            
        }
    }
}