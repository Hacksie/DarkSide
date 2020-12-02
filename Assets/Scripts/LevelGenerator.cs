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

            SpawnBarrier(startTimeBarrier, entryPos);


            Logger.Log(this, "Remaining Length:", remainingLength.ToString());

            while(remainingLength > 1)
            {
                section = SpawnSection(sectionPrefabs, section.exit.transform.position, remainingLength);
                
                remainingLength -= section.length;

                SpawnBarrier(sectionTimeBarrier, section.exit.transform.position);


                Logger.Log(this, "Remaining Length:", remainingLength.ToString());
            }
            
            //Logger.Log(this, "remainingLength:", remainingLength.ToString());

            section = SpawnSection(endPrefabs, section.exit.transform.position, 1);
        }

        public void DestroyLevel()
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                GameObject.Destroy(parent.transform.GetChild(i).gameObject);
            }
        }

        public Section SpawnSection(List<Section> sectionList, Vector3 position, int remainingLength)
        {
            var available = sectionList.Where(s => s.length <= remainingLength).ToList();

            int index = Random.Range(0, available.Count());

            var sectionObj = GameObject.Instantiate(available[index].gameObject, position, Quaternion.identity, parent.transform);
            return sectionObj.GetComponent<Section>();
        }

        public TimeBarrier SpawnBarrier(GameObject barrier, Vector3 position)
        {
            var barrierObj = GameObject.Instantiate(barrier, position, Quaternion.identity, parent.transform);
            return barrierObj.GetComponent<TimeBarrier>();
        }
    }
}
