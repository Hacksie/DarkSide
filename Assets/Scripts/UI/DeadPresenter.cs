using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign.UI
{
    public class DeadPresenter : AbstractPresenter
    {
        [SerializeField] private UnityEngine.UI.Text retryLabel = null;

        public override void Repaint()
        {
            if (GameManager.Instance.Data.permadeath)
            {
                retryLabel.text = "Quit";
            }
            else
            {
                retryLabel.text = "Restart";
            }

        }

        public void Quit()
        {
            if (GameManager.Instance.Data.permadeath)
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