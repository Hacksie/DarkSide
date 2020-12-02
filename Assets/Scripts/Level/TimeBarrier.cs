using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class TimeBarrier : MonoBehaviour
    {
        private int addTime;

        public void SetTime(int time)
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Logger.Log(this, "Barrier triggered");
                Animate();
            }
        }

        private void Animate()
        {
            this.gameObject.SetActive(false);
        }

    }
}
