using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class WeaponsPresenter : MonoBehaviour
    {
        [SerializeField] Text bullets = null;
        [SerializeField] Slider energy = null;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            energy.value = GameManager.Instance.Data.energy / GameManager.Instance.Data.maxEnergy;
            bullets.text = GameManager.Instance.Data.bullets.ToString();
        }
    }
}