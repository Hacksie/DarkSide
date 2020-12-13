using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign.UI
{
    public class LevelOverPresenter : AbstractPresenter
    {
        [SerializeField] UnityEngine.UI.Image upgradeSprite = null;
        [SerializeField] UnityEngine.UI.Text scoreText = null;
        [SerializeField] UnityEngine.UI.Text upgradeText = null;
        [SerializeField] UnityEngine.UI.Text upgradeTitleText = null;
        [SerializeField] UnityEngine.UI.Text upgradeDescriptionText = null;

        public override void Repaint()
        {
            scoreText.text = GameManager.Instance.Data.currentLevelScore.ToString();
            if(!GameManager.Instance.Random && GameManager.Instance.Data.currentLevelIndex < (GameManager.Instance.WeaponManager.Count() - 1))
            {
                var weapon = GameManager.Instance.WeaponManager.GetWeapon(GameManager.Instance.Data.currentLevelIndex + 1);
                upgradeSprite.sprite = weapon.settings.sprite;
                upgradeTitleText.text = weapon.settings.title;
                upgradeDescriptionText.text = weapon.settings.description;
                upgradeSprite.gameObject.SetActive(true);
                upgradeText.gameObject.SetActive(true);
                upgradeTitleText.gameObject.SetActive(true);
                upgradeDescriptionText.gameObject.SetActive(true);
            }
            else 
            {
                upgradeSprite.gameObject.SetActive(false);
                upgradeText.gameObject.SetActive(false);
                upgradeTitleText.gameObject.SetActive(false);
                upgradeDescriptionText.gameObject.SetActive(false);
            }
        }

        public void NextLevel()
        {
            GameManager.Instance.SetRunStart();
        }
    }
}