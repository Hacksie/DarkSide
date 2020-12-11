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

        //[Header("State")]
        //[SerializeField] int currentWeapon = 0; //FIXME: pull this from game state

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

        private void NextWeapon()
        {
            weapons[GameManager.Instance.Data.currentWeapon].gameObject.SetActive(false);
            GameManager.Instance.Data.currentWeapon++;
            var max = GameManager.Instance.Random ? weapons.Count - 1 : Mathf.Min(GameManager.Instance.Data.currentLevelIndex, weapons.Count - 1);

            if (GameManager.Instance.Data.currentWeapon >= max)
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
                //var max = Mathf.Min(GameManager.Instance.Data.currentLevelIndex, weapons.Count - 1);
                var max = GameManager.Instance.Random ? weapons.Count -1 : Mathf.Min(GameManager.Instance.Data.currentLevelIndex, weapons.Count - 1);
                GameManager.Instance.Data.currentWeapon = max;
            }
            weapons[GameManager.Instance.Data.currentWeapon].gameObject.SetActive(true);
        }



    }
}