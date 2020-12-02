using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class EntityPool : MonoBehaviour
    {
        [SerializeField] private Transform entityPool;

        public void Awake()
        {
            this.entityPool = entityPool ?? this.transform;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
