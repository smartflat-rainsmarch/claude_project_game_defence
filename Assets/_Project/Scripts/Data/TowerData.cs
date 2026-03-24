using UnityEngine;

namespace LastLineDefense.Data
{
    [CreateAssetMenu(fileName = "NewTowerData", menuName = "Defense/Tower Data")]
    public class TowerData : ScriptableObject
    {
        public string towerId;
        public string displayName;
        public GameObject prefab;
        public int buildCost = 50;
        public int damage = 10;
        public float attackRange = 3f;
        public float attackInterval = 1f;
        public GameObject projectilePrefab;
    }
}
