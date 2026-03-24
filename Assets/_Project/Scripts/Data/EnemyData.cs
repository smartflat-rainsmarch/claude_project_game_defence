using UnityEngine;

namespace LastLineDefense.Data
{
    [CreateAssetMenu(fileName = "NewEnemyData", menuName = "Defense/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public string enemyId;
        public string displayName;
        public GameObject prefab;
        public int maxHp = 30;
        public float moveSpeed = 2f;
        public int rewardGold = 10;
        public int baseDamage = 1;
    }
}
