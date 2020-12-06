using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(IEntity))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private IEntity entity;

        [Header("GameObjects")]
        [SerializeField] private GameObject body;
        [SerializeField] private List<GameObject> movementOptions;
        [SerializeField] private List<GameObject> eyesOptions;
        [SerializeField] private List<GameObject> weaponsOptions;

        [Header("Configuration")]
        [SerializeField] private int movement;
        [SerializeField] private int eyes;
        [SerializeField] private int weapons;

        void Awake()
        {
            entity = GetComponent<IEntity>();
        }

        void Update()
        {
            body.transform.LookAt(GameManager.Instance.Player.transform);
            

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
        }

        public void Randomize()
        {
            Logger.Log(this, "randomize");
            HideAll();
            movement = Random.Range(0, movementOptions.Count);
            eyes = Random.Range(0, eyesOptions.Count);
            weapons = Random.Range(0, weaponsOptions.Count);
            Logger.Log(this, "movement: ", movement.ToString());
            Logger.Log(this, "eyes:", eyes.ToString());
            Logger.Log(this, "weapons:", weapons.ToString());

            movementOptions[movement].SetActive(true);
            eyesOptions[eyes].SetActive(true);
            weaponsOptions[weapons].SetActive(true);
        }


    }
}