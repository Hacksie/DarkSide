using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class EntityPool : MonoBehaviour
    {
        [SerializeField] private Transform entityPool;
        [SerializeField] private GameObject largeEnemyPrefab;
        [SerializeField] private GameObject mediumEnemyPrefab;
        [SerializeField] private GameObject smallEnemyPrefab;

        [SerializeField] private List<IEntity> pool = new List<IEntity>();

        public void Awake()
        {
            this.entityPool = entityPool ?? this.transform;
        }

        public IEntity SpawnLargeEnemy(Vector3 position)
        {
            var gameObject = GameObject.Instantiate(largeEnemyPrefab, position, Quaternion.identity, entityPool);
            IEntity e = gameObject.GetComponent<IEntity>();
            return e;
        }

        public IEntity SpawnMediumEnemy(Vector3 position)
        {
            var gameObject = GameObject.Instantiate(mediumEnemyPrefab, position, Quaternion.identity, entityPool);
            IEntity e = gameObject.GetComponent<IEntity>();
            return e;
        }

        public IEntity SpawnSmallEnemy(Vector3 position)
        {
            var gameObject = GameObject.Instantiate(smallEnemyPrefab, position, Quaternion.identity, entityPool);
            IEntity e = gameObject.GetComponent<IEntity>();
            return e;
        }                
    }
}
