using UnityEngine;
using LastLineDefense.Enemy;

namespace LastLineDefense.Tower
{
    public class BasicTower : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private int damage = 10;
        [SerializeField] private float attackInterval = 1.0f;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint;

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

            if (projectilePrefab != null && firePoint != null)
            {
                var proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                var controller = proj.GetComponent<ProjectileController>();
                if (controller != null)
                    controller.Initialize(target.transform, damage);
            }
            else
            {
                var health = target.GetComponent<EnemyHealth>();
                if (health != null)
                    health.TakeDamage(damage);
            }
        }
    }
}
