using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class GamePresenter : MonoBehaviour
    {
        [SerializeField] Text timer = null;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            timer.text = GameManager.Instance.Data.timer.ToString("N0");
        }
    }
}