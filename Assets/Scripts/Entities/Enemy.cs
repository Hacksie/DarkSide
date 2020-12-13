using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Enemy : Entity
    {
        [Header("GameObjects")]
        [SerializeField] private GameObject body;
        [SerializeField] private List<GameObject> movementOptions;
        [SerializeField] private List<GameObject> eyesOptions;
        [SerializeField] private List<EnemyWeapon> weaponsOptions;
        [SerializeField] private List<GameObject> shieldOptions;
        [SerializeField] private EnemyWeapon meleeOption;

        [SerializeField] private AudioSource deathFX;


        [Header("Configuration")]
        [SerializeField] private int movementIndex;
        [SerializeField] private int eyesIndex;
        [SerializeField] private int weaponsIndex;
        [SerializeField] private int shieldIndex;
        [Header("Settings")]
        [SerializeField] private float maxHealth = 200.0f;
        [SerializeField] private float maxShield = 50.0f;
        [SerializeField] private float shootDistance = 50.0f;
        [SerializeField] private float attackDistance = 10.0f;
        [SerializeField] private float meleeDistance = 2.0f;
        [SerializeField] private float bootTimeOut = 2.0f;
        [Header("State")]
        [SerializeField] private float health = 100.0f;
        [SerializeField] private float shield = 50.0f;

        private float scale = 1.0f;
        private float bootTime = int.MaxValue;



        public override void UpdateBehaviour()
        {
            float distance;

            if (!GameManager.Instance.RunStarted)
            {
                return;
            }

            switch (State)
            {
                case EntityState.Passive:
                    distance = Vector3.Distance(GameManager.Instance.Player.transform.position, this.transform.position);
                    if (distance <= (GameManager.Instance.GameSettings.trackDistance * this.transform.localScale.x))
                    {
                        State = EntityState.Tracking;
                        bootTime = Time.time;
                    }
                    break;
                case EntityState.Tracking:
                    distance = Vector3.Distance(GameManager.Instance.Player.transform.position, this.transform.position);
                    if (distance <= (GameManager.Instance.GameSettings.trackDistance * this.transform.localScale.x))
                    {
                        if ((Time.time - bootTimeOut) > bootTime)
                        {
                            if (distance <= attackDistance)
                            {
                                State = EntityState.Angry;
                            }
                            TrackPlayer();
                            if (distance <= shootDistance)
                            {
                                weaponsOptions[weaponsIndex].Fire();
                            }
                        }
                    }
                    else
                    {
                        State = EntityState.Passive;
                    }
                    break;
                case EntityState.Angry:
                    distance = Vector3.Distance(GameManager.Instance.Player.transform.position, this.transform.position);
                    if (distance <= attackDistance)
                    {
                        if (!GameManager.Instance.GameSettings.enemiesDontMove)
                        {
                            agent.SetDestination(GameManager.Instance.Player.transform.position);
                            agent.isStopped = false;
                        }
                        if (distance <= meleeDistance)
                        {
                            Logger.Log(this, "Enemy Melee!");
                            meleeOption.Fire();
                        }
                        TrackPlayer();
                    }
                    else
                    {
                        State = EntityState.Tracking;
                        agent.isStopped = true;
                    }
                    break;
                case EntityState.Dead:
                    break;
                default:
                    break;
            }
        }

        public override void Hit(int boltAmount, int energyAmount)
        {
            Logger.Log(this, "Hit! Bolt:", boltAmount.ToString(), " Energy:", energyAmount.ToString());

            if (health <= 0)
            {
                // We're already dead Jim;
                return;
            }

            if (energyAmount > 0 && shield > 0)
            {
                if ((energyAmount * GameManager.Instance.GameSettings.shieldvsenergyfactor) >= shield)
                {
                    var consumed = Mathf.CeilToInt((float)shield / GameManager.Instance.GameSettings.shieldvsenergyfactor);
                    shield = 0;
                    energyAmount -= consumed;
                }
                else
                {
                    shield -= (energyAmount * GameManager.Instance.GameSettings.shieldvsenergyfactor);
                    energyAmount = 0;
                }

                UpdateShield();
            }

            if (boltAmount > 0 && shield > 0)
            {
                if ((boltAmount * GameManager.Instance.GameSettings.shieldvsboltfactor) >= shield)
                {
                    var consumed = Mathf.CeilToInt((float)shield / GameManager.Instance.GameSettings.shieldvsboltfactor);
                    shield = 0;
                    boltAmount -= consumed;
                }
                else
                {
                    shield -= (boltAmount * GameManager.Instance.GameSettings.shieldvsboltfactor);
                    boltAmount = 0;
                }


                UpdateShield();
            }


            Logger.Log(this, "Bolt amount:", boltAmount.ToString(), " Energy Amount:", energyAmount.ToString(), " Shield Amount:", shield.ToString());


            // if we have any left over damage, and the shield is down, apply it to the body
            if ((boltAmount > 0 || energyAmount > 0))
            {
                health -= ((boltAmount * GameManager.Instance.GameSettings.bodyvsboltfactor) + (energyAmount * GameManager.Instance.GameSettings.bodyvsenergyfactor));
            }

            if (health <= 0)
            {
                Dead();
            }

            Logger.Log(this, health.ToString(), " ", shield.ToString());
        }

        private void UpdateShield()
        {
            if (shield <= 0)
            {
                Logger.Log(this, "Shield destroyed");
                for (int i = 0; i < shieldOptions.Count; i++)
                {
                    shieldOptions[i].SetActive(false);
                }
                return;
            }

            if (shield <= (maxShield * this.scale * (shieldIndex - 1)))
            {
                Logger.Log(this, "Shield lowered");
                shieldOptions[shieldIndex--].SetActive(false);
                shieldOptions[shieldIndex].SetActive(true);
            }

            // FIXME: lower the shield version if we kill it below half
            // if(shield <= (maxShield * this.scale))
            // {
            //     Logger.Log(this, "Shield lowered");   
            //     shieldOptions[shieldIndex].SetActive(false);
            //     shieldOptions[--shieldIndex].SetActive(true);
            // }
        }

        private void PlayDeathSound()
        {
            deathFX.Play();
        }

        private void Dead()
        {
            Logger.Log(this, "Dead");
            State = EntityState.Dead;
            agent.isStopped = true;
            body.transform.LookAt(body.transform.position + new Vector3(0, -100, 0));
            PlayDeathSound();
            GameManager.Instance.AddScore(Mathf.FloorToInt(GameManager.Instance.GameSettings.scorePerKill * scale));

            GameManager.Instance.EntityPool.SpawnRandomPickups(this.transform.position + GameManager.Instance.GameSettings.pickupOffset);
        }

        private void TrackPlayer()
        {
            body.transform.LookAt(GameManager.Instance.MainCamera.transform);
        }

        private void HideAll()
        {
            movementOptions.ForEach(m => m.SetActive(false));
            eyesOptions.ForEach(m => m.SetActive(false));
            weaponsOptions.ForEach(m => m.gameObject.SetActive(false));
            shieldOptions.ForEach(m => m.SetActive(false));
        }

        public void Initialize(float scale)
        {
            HideAll();
            this.scale = scale;

            if (Random.value < 0.5)
            {
                movementIndex = Random.Range(0, movementOptions.Count);
            }
            else
            {
                movementIndex = 0;
            }

            if (Random.value < 0.5)
            {
                eyesIndex = Random.Range(0, eyesOptions.Count);
            }
            else
            {
                eyesIndex = 0;
            }

            if (Random.value < 0.5)
            {
                shieldIndex = Random.Range(0, shieldOptions.Count);
            }
            else
            {
                shieldIndex = 0;
            }

            weaponsIndex = Mathf.CeilToInt(Random.Range(0, weaponsOptions.Count) * scale);

            movementOptions[movementIndex].SetActive(true);
            eyesOptions[eyesIndex].SetActive(true);
            weaponsOptions[weaponsIndex].gameObject.SetActive(true);
            shieldOptions[shieldIndex].SetActive(true);

            this.transform.localScale = new Vector3(scale, scale, scale);
            this.health = this.maxHealth * GameManager.Instance.DifficultyAdjustment() * scale;
            this.shield = this.maxShield * shieldIndex * scale;
        }
    }
}