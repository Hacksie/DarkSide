using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


namespace HackedDesign
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] GameObject parent;
        [SerializeField] GameObject floor;
        [SerializeField] NavMeshSurface navMeshSurface;

        [Header("Prefabs")]
        [SerializeField] List<Section> startPrefabs;
        [SerializeField] List<Section> endPrefabs;
        [SerializeField] List<Section> sectionPrefabs;
        [SerializeField] GameObject startTimeBarrier;
        [SerializeField] GameObject sectionTimeBarrier;
        [SerializeField] GameObject endTimeBarrier;

        public void GenerateLevelSelect()
        {
            DestroyLevel();
            var section = SpawnSection(startPrefabs, parent.transform.position, 1);
        }

        public void Generate(int length)
        {
            Logger.Log(this, "Generating level");
            Logger.Log(this, "Length: ", length.ToString());
            DestroyLevel();
            var remainingLength = length;
            Bounds bounds = new Bounds();
            var section = SpawnSection(startPrefabs, parent.transform.position, remainingLength);

            bounds.Encapsulate(section.gameObject.GetComponent<Renderer>().bounds);
            

            remainingLength -= section.length;

            SpawnBarrier(startTimeBarrier, 0, true, section.exit.transform.position);

            Logger.Log(this, "Remaining Length:", remainingLength.ToString());

            while (remainingLength > 1)
            {
                var start = section.exit.transform.position + (section.exit.transform.forward * GameManager.Instance.GameSettings.islandGap);
                section = SpawnSection(sectionPrefabs, start, remainingLength);
                bounds.Encapsulate(section.gameObject.GetComponent<Renderer>().bounds);

                remainingLength -= section.length;

                SpawnBarrier(sectionTimeBarrier, section.length, false, section.exit.transform.position);

                Logger.Log(this, "Remaining Length:", remainingLength.ToString());
            }

            var endpos = section.exit.transform.position + (section.exit.transform.forward * GameManager.Instance.GameSettings.islandGap);

            section = SpawnSection(endPrefabs, endpos, 1);
            bounds.Encapsulate(section.gameObject.GetComponent<Renderer>().bounds);

            //Logger.Log(this, "Bounds: ", bounds.min.y.ToString());
            var floorPos = floor.transform.position;
            floorPos.y = bounds.min.y + GameManager.Instance.GameSettings.floorDistance;
            floor.transform.position = floorPos;

            navMeshSurface.BuildNavMesh();

            var spawnLocations = GetSpawnLocations().OrderBy(x => System.Guid.NewGuid()).ToList();

            // FIXME: small can spawn at medium etc
            SpawnEnemies(spawnLocations, "LargeSpawn", 1, GameManager.Instance.GameSettings.largeSpawns);
            SpawnEnemies(spawnLocations, "MediumSpawn", 0.75f, GameManager.Instance.GameSettings.mediumSpawns);
            SpawnEnemies(spawnLocations, "SmallSpawn", 0.50f, GameManager.Instance.GameSettings.smallSpawns);

            
            //SpawnMediumEnemies(spawnLocations, GameManager.Instance.GameSettings.mediumSpawns);
            //SpawnSmallEnemies(spawnLocations, GameManager.Instance.GameSettings.smallSpawns);
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

        public void SpawnEnemies(List<GameObject> spawnLocations, string spawnLocationTag, float size, int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject spawnLocation = spawnLocations.FirstOrDefault(l => l.CompareTag(spawnLocationTag));

                if (spawnLocation != null)
                {
                    Logger.Log(this, spawnLocation.transform.position.ToString());

                    //var enemy = GameManager.Instance.EntityPool.SpawnRandomLargeEnemy(spawnLocation.transform.position);
                    var enemy = GameManager.Instance.EntityPool.SpawnRandomEnemy(spawnLocation.transform.position, size);
                    //enemy.Randomize();
                    enemy.gameObject.transform.Rotate(0,Random.Range(135.0f, 225.0f), 0);
                    spawnLocations.Remove(spawnLocation);
                }
                else
                {
                    Logger.Log(this, "No spawn location found: ", spawnLocationTag);
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
