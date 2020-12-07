using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class EntityPool : MonoBehaviour
    {
        [SerializeField] private Transform entityPool;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject energySplash;
        [SerializeField] private GameObject boltSplash;


        [SerializeField] private List<IEntity> pool = new List<IEntity>();

        public void Awake()
        {
            this.entityPool = entityPool ?? this.transform;
        }

        public void DestroyEntities()
        {
            for (int i = 0; i < entityPool.transform.childCount; i++)
            {
                GameObject.Destroy(entityPool.transform.GetChild(i).gameObject);
            }

            pool.Clear();

        }

        public void UpdateBehaviour()
        {
            foreach (var e in pool)
            {
                e.UpdateBehaviour();
            }
        }

        public void UpdateLateBehaviour()
        {
            foreach (var e in pool)
            {
                e.UpdateLateBehaviour();
            }
        }

        public Enemy SpawnRandomEnemy(Vector3 position, float scale)
        {
            var gameObject = GameObject.Instantiate(enemyPrefab, position, Quaternion.identity, entityPool);
            var e = gameObject.GetComponent<Enemy>();
            e.Initialize(scale);
            pool.Add(e);
            return e;
        }

        public GameObject SpawnEnergySplash(Vector3 position)
        {
            var gameObject = GameObject.Instantiate(energySplash, position, Quaternion.identity, entityPool);
            GameObject.Destroy(gameObject, GameManager.Instance.GameSettings.splashTTL);
            return gameObject;
        }

        public GameObject SpawnBoltSplash(Vector3 position)
        {
            var gameObject = GameObject.Instantiate(boltSplash, position, Quaternion.identity, entityPool);
            GameObject.Destroy(gameObject, GameManager.Instance.GameSettings.splashTTL);
            return gameObject;
        }


    }
}
