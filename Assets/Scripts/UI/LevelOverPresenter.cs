using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign.UI
{
    public class LevelOverPresenter : AbstractPresenter
    {
        [SerializeField] UnityEngine.UI.Image upgradeSprite = null;
        [SerializeField] UnityEngine.UI.Text upgradeText = null;
        [SerializeField] UnityEngine.UI.Text upgradeDescriptionText = null;

        public override void Repaint()
        {
            if(GameManager.Instance.Data.currentLevelIndex < (GameManager.Instance.WeaponManager.Count() - 1))
            {
                upgradeSprite.sprite = GameManager.Instance.WeaponManager.GetWeapon(GameManager.Instance.Data.currentLevelIndex + 1).settings.sprite;
                upgradeSprite.gameObject.SetActive(true);
                upgradeText.gameObject.SetActive(true);
                upgradeDescriptionText.gameObject.SetActive(true);
            }
            else 
            {
                upgradeSprite.gameObject.SetActive(false);
                upgradeText.gameObject.SetActive(false);
                upgradeDescriptionText.gameObject.SetActive(false);
            }
            
        }

        public void NextLevel()
        {
            GameManager.Instance.SetRunStart();
        }
    }
}