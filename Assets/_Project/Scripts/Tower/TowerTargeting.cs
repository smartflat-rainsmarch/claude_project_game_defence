using UnityEngine;
using LastLineDefense.Enemy;

namespace LastLineDefense.Tower
{
    public class TowerTargeting : MonoBehaviour
    {
        [SerializeField] private float attackRange = 3f;
        [SerializeField] private string enemyTag = "Enemy";

        public float AttackRange => attackRange;

        public EnemyController FindNearestEnemy()
        {
            var enemies = FindObjectsByType<EnemyController>();
            EnemyController nearest = null;
            float nearestDist = float.MaxValue;

            foreach (var enemy in enemies)
            {
                if (enemy == null) continue;

                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist <= attackRange && dist < nearestDist)
                {
                    nearestDist = dist;
                    nearest = enemy;
                }
            }

            return nearest;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
