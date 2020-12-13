using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int boltDamage;
        [SerializeField] private int energyDamage;

        void OnCollisionEnter(Collision other)
        {
            
            if (other.gameObject.CompareTag("Player"))
            {
                Logger.Log(this, "Player Hit");
                GameManager.Instance.TakeDamage(Mathf.FloorToInt(boltDamage * GameManager.Instance.DifficultyAdjustment()), Mathf.FloorToInt(energyDamage * GameManager.Instance.DifficultyAdjustment()));
                Animate();
            }
        }

        private void Animate()
        {
            this.gameObject.SetActive(false);
        }
    }
}