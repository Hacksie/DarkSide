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
            entity = GetComponent<IEntity>();
        }

        void Update()
        {
            switch(entity.State)
            {

            }
        }


    }
}