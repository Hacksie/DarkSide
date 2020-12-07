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
        [SerializeField] private List<GameObject> weaponsOptions;
        [SerializeField] private List<GameObject> shieldOptions;

        [Header("Configuration")]
        [SerializeField] private int movementIndex;
        [SerializeField] private int eyesIndex;
        [SerializeField] private int weaponsIndex;
        [SerializeField] private int shieldIndex;
        [Header("Settings")]
        [SerializeField] private float lookTurnSpeed = 360.0f;
        [SerializeField] private float moveTurnSpeed = 180.0f;
        [SerializeField] private float maxHealth = 200.0f;
        [SerializeField] private float maxShield = 50.0f;
        [SerializeField] private float meleeDistance = 10.0f;
        [Header("State")]
        [SerializeField] private float health = 100.0f;
        [SerializeField] private float shield = 50.0f;

        private float scale = 1.0f;

        public override void UpdateBehaviour()
        {
            float distance;

            switch (State)
            {
                case EntityState.Passive:
                    distance = Vector3.Distance(GameManager.Instance.Player.transform.position, this.transform.position);
                    if (distance <= (GameManager.Instance.GameSettings.trackDistance * this.transform.localScale.x))
                    {
                        State = EntityState.Tracking;
                    }
                    break;
                case EntityState.Tracking:
                    distance = Vector3.Distance(GameManager.Instance.Player.transform.position, this.transform.position);
                    TrackPlayer(distance);
                    break;
                case EntityState.Angry:
                    distance = Vector3.Distance(GameManager.Instance.Player.transform.position, this.transform.position);
                    if(distance <= meleeDistance)
                    {
                        agent.SetDestination(GameManager.Instance.Player.transform.position);
                        
                    }
                    State = EntityState.Tracking;
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

            // Subtract energy from shield if any exists
            // Consume all energy if so
            if (energyAmount > 0 && shield > 0)
            {
                if (shield >= (energyAmount * GameManager.Instance.GameSettings.shieldvsenergyfactor))
                {
                    shield -= energyAmount * GameManager.Instance.GameSettings.shieldvsenergyfactor;
                    energyAmount = 0;
                    UpdateShield();
                }
                else
                {
                    shield = 0;
                    energyAmount = 0;
                    UpdateShield();
                }
            }

            // Subtract bolt from shield if any exists
            // Consume the bolt if so
            if (boltAmount > 0 && shield > 0)
            {
                if (shield >= (boltAmount * GameManager.Instance.GameSettings.shieldvsboltfactor))
                {
                    shield -= boltAmount * GameManager.Instance.GameSettings.shieldvsboltfactor;
                    boltAmount = 0;
                    UpdateShield();
                }
                else
                {
                    shield = 0;
                    boltAmount = 0;
                    UpdateShield();
                }
            }

            Logger.Log(this, "Bolt amount:", boltAmount.ToString(), " Energy Amount:", energyAmount.ToString(), " Shield Amount:", shield.ToString());


            // if we have any left over damage, and the shield is down, apply it to the body
            if ((boltAmount > 0 || energyAmount > 0) && shield <= 0)
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

        private void Dead()
        {
            Logger.Log(this, "Dead");
            State = EntityState.Dead;
            agent.isStopped = true;
            body.transform.LookAt(body.transform.position + new Vector3(0,-100,0));
            //this.gameObject.SetActive(false);
        }

        private void TrackPlayer(float distance)
        {
            body.transform.LookAt(GameManager.Instance.MainCamera.transform);

            if (distance <= meleeDistance)
            {
                State = EntityState.Angry;
            }
        }

        private void HideAll()
        {
            for (int i = 0; i < movementOptions.Count; i++)
            {
                movementOptions[i].SetActive(false);
            }

            for (int i = 0; i < eyesOptions.Count; i++)
            {
                eyesOptions[i].SetActive(false);
            }

            for (int i = 0; i < weaponsOptions.Count; i++)
            {
                weaponsOptions[i].SetActive(false);
            }

            for (int i = 0; i < shieldOptions.Count; i++)
            {
                shieldOptions[i].SetActive(false);
            }
        }

        public void Initialize(float scale)
        {
            Logger.Log(this, "randomize");
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

            weaponsIndex = Random.Range(0, weaponsOptions.Count);

            Logger.Log(this, "movement: ", movementIndex.ToString());
            Logger.Log(this, "eyes:", eyesIndex.ToString());
            Logger.Log(this, "weapons:", weaponsIndex.ToString());
            Logger.Log(this, "shield:", shieldIndex.ToString());

            movementOptions[movementIndex].SetActive(true);
            eyesOptions[eyesIndex].SetActive(true);
            weaponsOptions[weaponsIndex].SetActive(true);
            shieldOptions[shieldIndex].SetActive(true);

            this.transform.localScale = new Vector3(scale, scale, scale);
            this.health = this.maxHealth * scale;
            this.shield = this.maxShield * shieldIndex * scale;
        }


    }
}