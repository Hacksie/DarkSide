using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HackedDesign
{
    [RequireComponent(typeof(IEntity))]
    public class EyeFeedback : MonoBehaviour
    {
        [SerializeField] IEntity entity;
        [SerializeField] GameObject blackEyes;
        [SerializeField] GameObject blueEyes;
        [SerializeField] GameObject redEyes;


        void Awake()
        {
            entity = GetComponentInParent<IEntity>();
        }

        void Update()
        {
            switch (entity.State)
            {
                case EntityState.Passive:
                    SetEyes(true, false, false);
                    break;
                case EntityState.Tracking:
                    SetEyes(false, true, false);
                    break;
                case EntityState.Angry:
                    SetEyes(false, false, true);
                    break;
                case EntityState.Avoiding:
                    SetEyes(false, true, false);
                    break;
                default:
                    SetEyes(true, false, false);
                    break;
            }
        }

        private void SetEyes(bool black, bool blue, bool red)
        {
            blackEyes.SetActive(black);
            blueEyes.SetActive(blue);
            redEyes.SetActive(red);
        }


    }
}