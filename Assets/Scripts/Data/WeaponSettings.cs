
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace HackedDesign
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "DarkSide/Settings/Weapon")]
    public class WeaponSettings : ScriptableObject
    {
        [SerializeField] public WeaponType weaponType;
        [SerializeField] public float fireRate;
        [SerializeField] public int boltCost;
        [SerializeField] public int energyCost;
        [SerializeField] public int fragments = 1;
        [SerializeField] public List<WeaponDamageRange> damageRanges;
        [SerializeField] public float spread;
        [SerializeField] public bool heavy = false;
        [SerializeField] public bool automatic = false;
        [SerializeField] public Sprite sprite;
        [SerializeField] public AudioClip fireSound;
        [SerializeField] public int scoreNeeded;
    }

    public enum WeaponType {
        Bolt,
        Energy,
        Melee
    }

    [System.Serializable]
    public class WeaponDamageRange {
        public float minDistance;
        public float maxDistance;
        public int minBoltDamage;
        public int maxBoltDamage;
        public int minEnergyDamage;
        public int maxEnergyDamage;
    }
} 