using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class StatusPresenter : MonoBehaviour
    {
        [SerializeField] Slider health = null;
        [SerializeField] Slider shields = null;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            health.value = GameManager.Instance.Data.health / 100;
            shields.value = GameManager.Instance.Data.shields / 100;
        }
    }
}
