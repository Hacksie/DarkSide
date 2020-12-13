using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] private List<Transform> barrel;
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private float fireRate = 1.0f;
        [SerializeField] private float projectileSpeed = 1.0f;
        [SerializeField] private AudioSource attackFX;
        [SerializeField] private WeaponSettings settings;

        private int barrelIndex = 0;

        private float lastFire = 0;

        void Awake()
        {
            if (settings != null && settings.fireSound != null)
            {
                attackFX.clip = settings.fireSound;
                attackFX.loop = false;
            }
        }

        public void Fire()
        {
            if (Time.time - lastFire > (fireRate / GameManager.Instance.DifficultyAdjustment()))
            {

                lastFire = Time.time;
                switch (weaponType)
                {
                    case WeaponType.Melee:

                        var boltDamage = Random.Range(settings.damageRanges[0].minBoltDamage, settings.damageRanges[0].maxBoltDamage);
                        var energyDamage = Random.Range(settings.damageRanges[0].minEnergyDamage, settings.damageRanges[0].maxEnergyDamage);
                        GameManager.Instance.TakeDamage(boltDamage, energyDamage);
                        break;
                    case WeaponType.Bolt:
                        GameManager.Instance.EntityPool.SpawnBoltAttack(barrel[barrelIndex].position, barrel[barrelIndex].forward * projectileSpeed);
                        break;
                    case WeaponType.Energy:
                        GameManager.Instance.EntityPool.SpawnEnergyAttack(barrel[barrelIndex].position, barrel[barrelIndex].forward * projectileSpeed);
                        break;
                    case WeaponType.Rail:
                        GameManager.Instance.EntityPool.SpawnRailAttack(barrel[barrelIndex].position, barrel[barrelIndex].forward * projectileSpeed);
                        break;
                }

                PlayAttackSound();

                barrelIndex = ++barrelIndex >= barrel.Count ? 0 : barrelIndex;
            }
        }

        private void PlayAttackSound()
        {
            if (attackFX.clip != null)
            {
                attackFX.Play();
            }
        }
    }
}