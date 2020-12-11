using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class LevelEnd : MonoBehaviour
    {

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Logger.Log(this, "End triggered");
                // Level Over
                GameManager.Instance.SetLevelOver();

                Animate();
            }
        }

        private void Animate()
        {
            //this.gameObject.SetActive(false);
        }

    }
}
