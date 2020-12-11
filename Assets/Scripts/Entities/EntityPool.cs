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
        [SerializeField] private GameObject energyAttack;
        [SerializeField] private GameObject boltAttack;
        [SerializeField] private GameObject railAttack;
        [SerializeField] private List<GameObject> pickups;


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

        public GameObject SpawnEnergyAttack(Vector3 position, Vector3 direction)
        {
            var gameObject = GameObject.Instantiate(energyAttack, position, Quaternion.identity, entityPool);
            GameObject.Destroy(gameObject, GameManager.Instance.GameSettings.attackTTL);
            var rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(direction, ForceMode.Impulse);
            return gameObject;
        }
        public GameObject SpawnBoltAttack(Vector3 position, Vector3 direction)
        {
            var gameObject = GameObject.Instantiate(boltAttack, position, Quaternion.identity, entityPool);
            GameObject.Destroy(gameObject, GameManager.Instance.GameSettings.attackTTL);
            var rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(direction, ForceMode.Impulse);
            return gameObject;
        }

        public GameObject SpawnRailAttack(Vector3 position, Vector3 direction)
        {
            var gameObject = GameObject.Instantiate(railAttack, position, Quaternion.identity, entityPool);
            GameObject.Destroy(gameObject, GameManager.Instance.GameSettings.attackTTL);
            var rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(direction, ForceMode.Impulse);
            return gameObject;
        }

        public void SpawnRandomPickups(Vector3 position)
        {
            var count = Random.Range(GameManager.Instance.GameSettings.minPickups, GameManager.Instance.GameSettings.maxPickups + 1);
            for (int i = 0; i < count; i++)
            {
                var direction = Random.insideUnitCircle.normalized * 2;
                var spawnPosition = position + new Vector3(direction.x, 0, direction.y);
                var gameObject = GameObject.Instantiate(pickups[Random.Range(0, pickups.Count)], spawnPosition, Quaternion.identity, entityPool);
                GameObject.Destroy(gameObject, GameManager.Instance.GameSettings.pickupTTL);
                var rigidbody = gameObject.GetComponent<Rigidbody>();
                rigidbody.AddForce((spawnPosition - position), ForceMode.Impulse);
            }
        }

    }
}
