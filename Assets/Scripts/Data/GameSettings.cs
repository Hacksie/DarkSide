
using UnityEngine;

namespace HackedDesign
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "DarkSide/Settings/Game")]
    public class GameSettings : ScriptableObject
    {
        
        [SerializeField] public bool skipTutorial = false;
        [SerializeField] public float easyAdj = 1.0f;
        [SerializeField] public float mediumAdj = 0.8f;
        [SerializeField] public float hardAdj = 0.6f;
        [SerializeField] public int initialAddTime = 8;
        [Header("Cheats")]
        [SerializeField] public bool invulnerability = true;
        [SerializeField] public bool spawn = true;
        [SerializeField] public bool skipDialog = true;
    }
}