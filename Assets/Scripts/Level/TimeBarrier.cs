using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class TimeBarrier : MonoBehaviour
    {
        private int addTime;
        private bool start;

        public void SetTime(int time, bool start)
        {
            this.addTime = time;
            this.start = start;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Logger.Log(this, "Barrier triggered");
                GameManager.Instance.AddTime(addTime);
                if(this.start)
                {
                    GameManager.Instance.StartRun();
                }
                Animate();
            }
        }

        private void Animate()
        {
            this.gameObject.SetActive(false);
        }

    }
}
