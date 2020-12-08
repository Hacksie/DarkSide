using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class EnemyWeapon : MonoBehaviour
    {
        void Update()
        {
            TrackPlayer();
        }

        private void TrackPlayer()
        {
            this.transform.LookAt(GameManager.Instance.MainCamera.transform);
        }        
    }
}