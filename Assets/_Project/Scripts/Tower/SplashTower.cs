using UnityEngine;
using LastLineDefense.Enemy;

namespace LastLineDefense.Tower
{
    public class SplashTower : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private int damage = 8;
        [SerializeField] private float attackInterval = 1.5f;
        [SerializeField] private float splashRadius = 1.5f;

        private TowerTargeting targeting;
        private float attackTimer;

        private void Awake()
        {
            targeting = GetComponent<TowerTargeting>();
            if (targeting == null)
                targeting = gameObject.AddComponent<TowerTargeting>();
        }

        private void Update()
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackInterval)
            {
                TryAttack();
                attackTimer = 0f;
            }
        }

        private void TryAttack()
        {
            EnemyController target = targeting.FindNearestEnemy();
            if (target == null) return;

            var enemies = FindObjectsByType<EnemyController>();
            foreach (var enemy in enemies)
            {
                if (enemy == null) continue;
                if (Vector3.Distance(target.transform.position, enemy.transform.position) <= splashRadius)
                {
                    var health = enemy.GetComponent<EnemyHealth>();
                    if (health != null)
                        health.TakeDamage(damage);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, splashRadius);
        }
    }
}
