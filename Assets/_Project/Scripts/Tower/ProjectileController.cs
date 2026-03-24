using UnityEngine;
using LastLineDefense.Enemy;

namespace LastLineDefense.Tower
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float maxLifetime = 5f;

        private Transform target;
        private int damage;
        private bool initialized;

        public void Initialize(Transform targetTransform, int damageAmount)
        {
            target = targetTransform;
            damage = damageAmount;
            initialized = true;
            Destroy(gameObject, maxLifetime);
        }

        private void Update()
        {
            if (!initialized) return;

            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                HitTarget();
            }
        }

        private void HitTarget()
        {
            if (target != null)
            {
                var health = target.GetComponent<EnemyHealth>();
                if (health != null)
                    health.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
