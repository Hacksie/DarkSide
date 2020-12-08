#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{
    public class Weapon : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] private Animator? animator;
        [SerializeField] private Transform? barrel;

        [Header("Settings")]
        [SerializeField] public WeaponSettings? settings;
        [SerializeField] public LayerMask hitMask;
        public float lastFired = 0;

        private bool isFiring = false;
        private bool isMeleeing = false;

        public bool IsAutomatic => settings != null ? settings.automatic : false;
        public bool CanFire => settings != null ? (Time.time - lastFired) > settings.fireRate : false;

        void Awake()
        {
            animator = GetComponent<Animator>();
            barrel = GameManager.Instance.MainCamera.transform;
        }


        public void Fire()
        {
            if (settings == null)
            {
                Logger.LogError(this, "Weapon settings are null");
                return;
            }

            if (CanFire && GameManager.Instance.Data.bolts >= settings.boltCost && GameManager.Instance.Data.energy >= settings.energyCost)
            {
                lastFired = Time.time;
                isFiring = true;
                Logger.Log(this, "Fired");
                GameManager.Instance.ConsumeEnergy(settings.energyCost);
                GameManager.Instance.ConsumeBolts(settings.boltCost);

                if (settings.fireSound != null)
                {
                    AudioManager.Instance.PlayFire(settings.fireSound);
                }
                else
                {

                    if (settings.weaponType == WeaponType.Bolt)
                    {
                        AudioManager.Instance.PlayBoltFire();
                    }
                    else
                    {
                        AudioManager.Instance.PlayEnergyFire();
                    }
                }

                CalcShots();
            }
        }

        public void Melee()
        {
            if (settings == null)
            {
                Logger.LogError(this, "Weapon settings are null");
                return;
            }

            Logger.Log(this, "Melee");

            if (CanFire)
            {
                lastFired = Time.time;
                isMeleeing = true;
                Logger.Log(this, "Can Melee Fire");
                
                // if (settings.fireSound != null)
                // {
                //     AudioManager.Instance.PlayFire(settings.fireSound);
                // }
                // else
                // {

                //     if (settings.weaponType == WeaponType.Bolt)
                //     {
                //         AudioManager.Instance.PlayBoltFire();
                //     }
                //     else
                //     {
                //         AudioManager.Instance.PlayEnergyFire();
                //     }
                // }

                CalcShots();
            }            


        }


        public void CalcShots()
        {
            if (settings == null)
            {
                Logger.LogError(this, "Weapon settings are null");
                return;
            }
            RaycastHit hit;
            for (int i = 0; i < settings.fragments; i++)
            {
                if (barrel != null && Physics.Raycast(barrel.transform.position, CalcSpread(barrel.transform.forward, settings.spread), out hit, 1000, hitMask))
                {
                    Debug.DrawLine(barrel.transform.position, hit.point, Color.red, 1);

                    if (GameManager.Instance.EntityPool != null)
                    {
                        if (settings.weaponType == WeaponType.Energy)
                        {
                            GameManager.Instance.EntityPool.SpawnEnergySplash(hit.point);
                        }
                        else if (settings.weaponType == WeaponType.Bolt)
                        {
                            GameManager.Instance.EntityPool.SpawnBoltSplash(hit.point);
                        }
                    }

                    if (hit.transform.CompareTag("Entity"))
                    {

                        var distance = hit.distance;

                        Logger.Log(this, "Hit, Distance:", distance.ToString());

                        var damage = settings.damageRanges.FirstOrDefault(d => distance >= d.minDistance && distance < d.maxDistance);

                        if (damage != null)
                        {
                            var entity = hit.transform.gameObject.GetComponentInParent<IEntity>();

                            if (entity != null)
                            {
                                entity.Hit(Random.Range(damage.minBoltDamage, damage.maxBoltDamage + 1), Random.Range(damage.minEnergyDamage, damage.maxEnergyDamage + 1));
                            }
                        }
                    }

                }
            }
        }

        public Vector3 CalcSpread(Vector3 forward, float spread)
        {
            var spreadRandomX = Random.Range(-1 * spread, spread);
            var spreadRandomY = Random.Range(-1 * spread, spread);

            return Quaternion.Euler(spreadRandomX, spreadRandomY, 0) * forward;

        }

        void LateUpdate()
        {
            Animate();
            isFiring = false;
            isMeleeing = false;
        }

        private void Animate()
        {
            if (animator != null)
            {
                if(isMeleeing)
                {
                    Logger.Log(this, isMeleeing.ToString());
                }
                animator.SetBool("Heavy", (settings != null && settings.heavy) || false);
                animator.SetBool("Fire", isFiring);
                animator.SetBool("Melee", isMeleeing);
            }
        }
    }
}
