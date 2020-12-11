
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
        [SerializeField] public float trackDistance = 200.0f;
        [SerializeField] public float shieldvsenergyfactor = 2.0f;
        [SerializeField] public float shieldvsboltfactor = 0.5f;
        [SerializeField] public float bodyvsenergyfactor = 0.75f;
        [SerializeField] public float bodyvsboltfactor = 1.0f;
        [SerializeField] public float splashTTL = 0.1f;
        [SerializeField] public float attackTTL = 1.0f;
        [SerializeField] public float pickupTTL = 10.0f;
        [SerializeField] public Vector3 pickupOffset = new Vector3(0, 3, 0);
        [SerializeField] public int minPickups = 1;
        [SerializeField] public int maxPickups = 5;
        [SerializeField] public float pickupForce = 5;
        [SerializeField] public int maxScore = 1000000;
        [SerializeField] public int scorePerKill = 100;
        [Header("Cheats")]
        [SerializeField] public bool invulnerability = true;
        [SerializeField] public bool unlimitedBolts = false;
        [SerializeField] public bool unlimitedEnergy = false;
        [SerializeField] public bool spawn = true;
        [SerializeField] public bool infinity = false;
        [SerializeField] public bool skipDialog = true;
        [SerializeField] public bool enemiesDontMove = false;
        [SerializeField] public bool enemiesDontAttack = false;
    }
}