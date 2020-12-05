#nullable enable
using System.Collections;
using System.Collections.Generic;
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

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public bool CanFire()
        {
            return settings != null ? (Time.time - lastFired) > settings.fireRate : false;
        }

        public void Fire()
        {
            if (settings == null)
            {
                Logger.LogError(this, "Weapon settings are null");
                return;
            }

            if (CanFire() && GameManager.Instance.Data.bullets >= settings.boltCost && GameManager.Instance.Data.energy >= settings.energyCost)
            {
                lastFired = Time.time;
                isFiring = true;
                Logger.Log(this, "Fired");
                GameManager.Instance.ConsumeEnergy(settings.energyCost);
                GameManager.Instance.ConsumeBolts(settings.boltCost);
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

                if (Physics.Raycast(barrel.transform.position, CalcSpread(barrel.transform.forward, settings.spread), out hit, 2000, hitMask))
                {
                    Debug.DrawLine(barrel.transform.position, hit.point, Color.red, 1);

                    if (settings.weaponType == WeaponType.Bolt)
                    {
                        AudioManager.Instance.PlayBoltFire();
                    }
                    else
                    {
                        AudioManager.Instance.PlayEnergyFire();
                    }

                    Logger.Log(this, "Hit ", hit.transform.tag, " ", hit.transform.name);

                    if (hit.transform.CompareTag("Entity"))
                    {

                        var distance = hit.distance;

                        Logger.Log(this, "Hit distance", distance.ToString());

                        //var damage = 

                        var entity = hit.transform.gameObject.GetComponentInParent<IEntity>();

                        if (entity != null)
                        {
                            entity.Hit(1);
                        }
                    }
                    //Logger.Log(this, "Hit!" + hit.transform.name);
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
        }

        private void Animate()
        {
            if (animator != null)
            {
                animator.SetBool("Heavy", settings.heavy);
                animator.SetBool("Fire", isFiring);
            }
        }
    }
}