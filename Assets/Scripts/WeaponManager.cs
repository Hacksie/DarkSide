using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{
    public class WeaponManager : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] List<Weapon> weapons;
        [SerializeField] Weapon melee;

        [Header("Settings")]
        [Range(0, 2)]
        [SerializeField] int currentOffset = 0;
        [SerializeField] List<float> offset = new List<float>() { -0.3f, 0, 0.3f };
        [SerializeField] float verticalOffset = 0.0f;

        [Header("State")]
        [SerializeField] int currentWeapon = 0; //FIXME: pull this from game state

        void Start()
        {
            HideAll();

            weapons[currentWeapon].gameObject.SetActive(true);
            
        }

        void Update()
        {
            this.transform.localPosition = new Vector3(offset[GameManager.Instance.PlayerPreferences.gunPosition], verticalOffset, 0);
        }

        public Weapon GetCurrentWeapon()
        {
            return weapons[currentWeapon];
        }

        public Weapon GetMeleeWeapon()
        {   
            return melee;
        }

        public void WeaponScrollEvent(InputAction.CallbackContext context)
        {
            if (context.started)
            {  
                var direction = context.ReadValue<float>();
                if(direction <=0)
                {
                    PrevWeapon();
                    
                }
                else
                {
                    NextWeapon();
                }
            }
        }


        public void NextWeaponEvent(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                NextWeapon();
            }
        }

        public void PrevWeaponEvent(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                PrevWeapon();
            }
        }

        private void HideAll()
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].gameObject.SetActive(false);
            }
        }



        private void NextWeapon()
        {
            weapons[currentWeapon].gameObject.SetActive(false);
            currentWeapon++;
            if (currentWeapon >= weapons.Count)
            {
                currentWeapon = 0;
            }
            weapons[currentWeapon].gameObject.SetActive(true);
        }

        private void PrevWeapon()
        {
            weapons[currentWeapon].gameObject.SetActive(false);
            currentWeapon--;
            if (currentWeapon < 0)
            {
                currentWeapon = weapons.Count - 1;
            }
            weapons[currentWeapon].gameObject.SetActive(true);
        }



    }
}