using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign.UI
{
    public class HudPresenter : AbstractPresenter
    {
        [SerializeField] Text timer = null;
        [SerializeField] Text cash = null;
        [SerializeField] Slider health = null;
        [SerializeField] Slider shields = null;
        [SerializeField] Text bullets = null;
        [SerializeField] Slider energy = null;
        [SerializeField] UnityEngine.UI.Image gunSprite = null;

        public override void Repaint()
        {
            timer.text = GameManager.Instance.Data.timer.ToString("N0");
            cash.text = GameManager.Instance.Data.score.ToString();
            health.value = (float)GameManager.Instance.Data.health / GameManager.Instance.Data.maxHealth;
            shields.value = (float)GameManager.Instance.Data.shields / GameManager.Instance.Data.maxShields;
            energy.value = (float)GameManager.Instance.Data.energy / GameManager.Instance.Data.maxEnergy;
            bullets.text = GameManager.Instance.Data.bolts.ToString();
            gunSprite.sprite = GameManager.Instance.WeaponManager.GetCurrentWeapon().settings.sprite;
            
        }
    }
}