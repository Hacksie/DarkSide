using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

namespace HackedDesign
{
    public class WeaponManager : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] List<Weapon> weapons;
        [SerializeField] Weapon melee;
        [SerializeField] SpriteRenderer crosshair;

        [Header("Settings")]
        [Range(0, 2)]
        [SerializeField] int currentOffset = 0;
        [SerializeField] List<float> offset = new List<float>() { -0.3f, 0, 0.3f };
        [SerializeField] float verticalOffset = 0.0f;


        void Start()
        {
            HideAll();

            //FIXME: set up the main weapon, so we don't have to do everything in update
            //weapons[currentWeapon].gameObject.SetActive(true);

        }

        void Update()
        {
            if (GameManager.Instance.CurrentState.PlayerActionAllowed)
            {
                this.transform.localPosition = new Vector3(offset[GameManager.Instance.PlayerPreferences.gunPosition], verticalOffset, 0);
                if (offset[GameManager.Instance.PlayerPreferences.gunPosition] != 0)
                {
                    melee.transform.localPosition = new Vector3(-2 * offset[GameManager.Instance.PlayerPreferences.gunPosition], 0, 0);
                }
                crosshair.sprite = weapons[GameManager.Instance.Data.currentWeapon].settings.crosshair; // FIXME: Don't do this every frame, stupid
            }
        }


        public void WeaponScrollEvent(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                var direction = context.ReadValue<float>();
                if (direction <= 0)
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

        public void Weapon1Event(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                SelectWeapon(0);
            }
        }

        public void Weapon2Event(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                SelectWeapon(1);
            }
        }

        public void Weapon3Event(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                SelectWeapon(2);
            }
        }

        public void Weapon4Event(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                SelectWeapon(3);
            }
        }

        public void Weapon5Event(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                SelectWeapon(4);
            }
        }

        public void Weapon6Event(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                SelectWeapon(5);
            }
        }

        public void Weapon7Event(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                SelectWeapon(6);
            }
        }

        public void Weapon8Event(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                SelectWeapon(7);
            }
        }
        
        public void ShowCurrentWeapon()
        {
            HideAll();
            weapons[GameManager.Instance.Data.currentWeapon].gameObject.SetActive(true);
        }

        public Weapon GetCurrentWeapon()
        {
            return weapons[GameManager.Instance.Data.currentWeapon];
        }

        public Weapon GetMeleeWeapon()
        {
            return melee;
        }

        public Weapon GetWeapon(int index)
        {
            return weapons[index];
        }

        public int GetMaxWeapon()
        {
            return GameManager.Instance.Random ? weapons.Count - 1 : Mathf.Min(GameManager.Instance.Data.currentLevelIndex, weapons.Count - 1);
        }        

        public int Count()
        {
            return weapons.Count();
        }

        private void HideAll()
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].gameObject.SetActive(false);
            }
        }

        private void SelectWeapon(int weapon)
        {
            if(weapon <= GetMaxWeapon())
            {
                weapons[GameManager.Instance.Data.currentWeapon].gameObject.SetActive(false);    
                GameManager.Instance.Data.currentWeapon = weapon;
                weapons[GameManager.Instance.Data.currentWeapon].gameObject.SetActive(true);    
            }
        }

        private void NextWeapon()
        {
            weapons[GameManager.Instance.Data.currentWeapon].gameObject.SetActive(false);
            GameManager.Instance.Data.currentWeapon++;

            if (GameManager.Instance.Data.currentWeapon >= GetMaxWeapon())
            {
                GameManager.Instance.Data.currentWeapon = 0;
            }
            weapons[GameManager.Instance.Data.currentWeapon].gameObject.SetActive(true);
        }

        private void PrevWeapon()
        {
            weapons[GameManager.Instance.Data.currentWeapon].gameObject.SetActive(false);
            GameManager.Instance.Data.currentWeapon--;
            if (GameManager.Instance.Data.currentWeapon < 0)
            {
                GameManager.Instance.Data.currentWeapon = GetMaxWeapon();
            }
            weapons[GameManager.Instance.Data.currentWeapon].gameObject.SetActive(true);
        }



    }
}