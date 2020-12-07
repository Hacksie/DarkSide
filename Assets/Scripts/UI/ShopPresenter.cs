using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign.UI
{
    public class ShopPresenter : AbstractPresenter
    {
        

        public override void Repaint()
        {
            
        }

        public void Leave()
        {
            GameManager.Instance.CurrentState.Start();
        }
    }
}