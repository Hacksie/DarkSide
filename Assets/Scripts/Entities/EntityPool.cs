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
        [SerializeField] private GameObject enemyPrefab;

        [SerializeField] private List<IEntity> pool = new List<IEntity>();

        public void Awake()
        {
            this.entityPool = entityPool ?? this.transform;
        }

        public void DestroyEntities()
        {

        }

        public Enemy SpawnRandomEnemy(Vector3 position, float scale)
        {
            var gameObject = GameObject.Instantiate(enemyPrefab, position, Quaternion.identity, entityPool);
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
            var e = gameObject.GetComponent<Enemy>();

            e.Randomize();
            

            return e;
        }

        public Enemy SpawnRandomLargeEnemy(Vector3 position)
        {
            var gameObject = GameObject.Instantiate(largeEnemyPrefab, position, Quaternion.identity, entityPool);
            var e = gameObject.GetComponent<Enemy>();

            e.Randomize();
            

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
