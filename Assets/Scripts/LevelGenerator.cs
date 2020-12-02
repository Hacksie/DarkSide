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

        public void Generate(int length)
        {
            Logger.Log(this, length.ToString());
            DestroyLevel();
            var entryPos = parent.transform.position;
            var remainingLength = length;
            var section = GenerateSection(startPrefabs, entryPos, remainingLength);

            remainingLength -= section.length;
            entryPos = section.exit.transform.position;


            Logger.Log(this, "remainingLength:", remainingLength.ToString());

            while(remainingLength > 1)
            {
                section = GenerateSection(sectionPrefabs, entryPos, remainingLength);

                remainingLength -= section.length;
                entryPos = section.exit.transform.position;


                Logger.Log(this, "remainingLength:", remainingLength.ToString());
            }
            
            //Logger.Log(this, "remainingLength:", remainingLength.ToString());

            section = GenerateSection(endPrefabs, entryPos, 1);
        }

        public void DestroyLevel()
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                GameObject.Destroy(parent.transform.GetChild(i).gameObject);
            }
        }

        public Section GenerateSection(List<Section> sectionList, Vector3 entry, int remainingLength)
        {
            var available = sectionList.Where(s => s.length <= remainingLength).ToList();

            int index = Random.Range(0, available.Count());

            var sectionObj = GameObject.Instantiate(available[index].gameObject, entry, Quaternion.identity, parent.transform);
            return sectionObj.GetComponent<Section>();
        }
    }
}
