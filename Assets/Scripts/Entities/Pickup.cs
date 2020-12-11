using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private int ammo;
        [SerializeField] private int health;
        [SerializeField] private float energy;
        [SerializeField] private float shields;

        void OnTriggerEnter(Collider other)
        {
            Logger.Log(this, "Trigger");
            if (other.CompareTag("Player"))
            {
                Logger.Log(this, "Player");
                GameManager.Instance.ConsumeShields(-1 * shields);
                GameManager.Instance.ConsumeHealth(-1 * health);
                GameManager.Instance.ConsumeEnergy(-1 * energy);
                GameManager.Instance.ConsumeBolts(-1 * ammo);

                Animate();
                AudioManager.Instance.PlayPickup();
                // FIXME: Play a pickup noise
            }

        }

        private void Animate()
        {
            this.gameObject.SetActive(false);
        }
    }
}