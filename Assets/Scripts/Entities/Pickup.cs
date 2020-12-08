using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private float shields;

        void OnTriggerEnter(Collider other)
        {
            Logger.Log(this, "Trigger");
            if (other.CompareTag("Player"))
            {
                Logger.Log(this, "Player");
                GameManager.Instance.ConsumeShields(-1 * shields);
            }
        }

        private void Animate()
        {
            this.gameObject.SetActive(false);
        }
    }
}