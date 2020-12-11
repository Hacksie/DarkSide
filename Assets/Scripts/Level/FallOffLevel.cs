using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class FallOffLevel : MonoBehaviour
    {

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Logger.Log(this, "Fell off level triggered");
                // Level Over
                GameManager.Instance.SetDead();

                Animate();
            }
        }

        private void Animate()
        {
            //this.gameObject.SetActive(false);
        }

    }
}
