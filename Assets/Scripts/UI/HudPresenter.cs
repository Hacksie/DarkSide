using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign.UI
{
    public class HudPresenter : AbstractPresenter
    {
        [SerializeField] Text timer = null;
        [SerializeField] Slider health = null;
        [SerializeField] Slider shields = null;
        [SerializeField] Text bullets = null;
        [SerializeField] Slider energy = null;

        public override void Repaint()
        {
            timer.text = GameManager.Instance.Data.timer.ToString("N0");
            health.value = GameManager.Instance.Data.health / 100;
            shields.value = GameManager.Instance.Data.shields / 100;
            energy.value = GameManager.Instance.Data.energy / GameManager.Instance.Data.maxEnergy;
            bullets.text = GameManager.Instance.Data.bullets.ToString();
        }
    }
}