
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
        [SerializeField] public int largeSpawns = 2;
        [SerializeField] public int mediumSpawns = 5;
        [SerializeField] public int smallSpawns = 20;
        [SerializeField] public float energyBaseRegen = 1;
        [SerializeField] public float dashEnergy = 33;
        [SerializeField] public float footstepTime = 0.3f;
        [SerializeField] public float footstepSpeedSqr = 1.0f;
        [SerializeField] public float islandGap = 1.0f;
        [SerializeField] public float floorDistance = -5.0f;
        [Header("Cheats")]
        [SerializeField] public bool invulnerability = true;
        [SerializeField] public bool spawn = true;
        [SerializeField] public bool infinity = false;
        [SerializeField] public bool skipDialog = true;
    }
}