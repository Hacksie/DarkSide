using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace HackedDesign
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] GameObject parent;

        [Header("Prefabs")]
        [SerializeField] List<Section> startPrefabs;
        [SerializeField] List<Section> endPrefabs;
        [SerializeField] List<Section> sectionPrefabs;
        [SerializeField] GameObject startTimeBarrier;
        [SerializeField] GameObject sectionTimeBarrier;
        [SerializeField] GameObject endTimeBarrier;

        public void Generate(int length)
        {
            Logger.Log(this, "Generating level");
            Logger.Log(this, "Length: ", length.ToString());
            DestroyLevel();
            var remainingLength = length;
            var section = SpawnSection(startPrefabs, parent.transform.position, remainingLength);

            remainingLength -= section.length;

            SpawnBarrier(startTimeBarrier, GameManager.Instance.GameSettings.initialAddTime, true, section.exit.transform.position);

            Logger.Log(this, "Remaining Length:", remainingLength.ToString());

            while (remainingLength > 1)
            {
                section = SpawnSection(sectionPrefabs, section.exit.transform.position, remainingLength);

                remainingLength -= section.length;

                SpawnBarrier(sectionTimeBarrier, section.length, false, section.exit.transform.position);

                Logger.Log(this, "Remaining Length:", remainingLength.ToString());
            }

            //Logger.Log(this, "remainingLength:", remainingLength.ToString());

            section = SpawnSection(endPrefabs, section.exit.transform.position, 1);


            var spawnLocations = GetSpawnLocations();

            SpawnLargeEnemies(spawnLocations, 2);
        }

        public void DestroyLevel()
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                GameObject.Destroy(parent.transform.GetChild(i).gameObject);
            }
        }

        public Section SpawnSection(List<Section> sectionPrefabList, Vector3 position, int remainingLength)
        {
            var available = sectionPrefabList.Where(s => s.length <= remainingLength).ToList();

            int index = Random.Range(0, available.Count());

            var sectionObj = GameObject.Instantiate(available[index].gameObject, position, Quaternion.identity, parent.transform);
            return sectionObj.GetComponent<Section>();
        }

        public TimeBarrier SpawnBarrier(GameObject barrierPrefab, int time, bool start, Vector3 position)
        {
            var barrierObj = GameObject.Instantiate(barrierPrefab, position, Quaternion.identity, parent.transform);
            var barrier = barrierObj.GetComponent<TimeBarrier>();

            barrier.SetTime(time, start);

            return barrier;
        }

        public void SpawnLargeEnemies(List<GameObject> spawnLocations, int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject loc = spawnLocations.FirstOrDefault(l => l.CompareTag("LargeSpawn"));

                if (loc != null)
                {
                    Logger.Log(this, loc.transform.position.ToString());

                    var enemy = GameObject.CreatePrimitive(PrimitiveType.Capsule);

                    enemy.transform.position = loc.transform.position + new Vector3(0, 1, 0);
                    spawnLocations.Remove(loc);
                } 
                else 
                {
                    Logger.Log(this, "No large spawn location found");
                }

            }


        }

        public List<GameObject> GetSpawnLocations()
        {
            List<GameObject> spawnLocations = new List<GameObject>();
            spawnLocations.AddRange(GameObject.FindGameObjectsWithTag("SmallSpawn"));
            spawnLocations.AddRange(GameObject.FindGameObjectsWithTag("MediumSpawn"));
            spawnLocations.AddRange(GameObject.FindGameObjectsWithTag("LargeSpawn"));

            return spawnLocations;
        }
    }
}
