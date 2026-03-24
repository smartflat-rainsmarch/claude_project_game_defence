using UnityEngine;
using LastLineDefense.Enemy;

namespace LastLineDefense.Tower
{
    public class LaserTower : MonoBehaviour
    {
        [Header("Laser Settings")]
        [SerializeField] private int damagePerTick = 5;
        [SerializeField] private float tickInterval = 0.5f;
        [SerializeField] private LineRenderer laserLine;

        private TowerTargeting targeting;
        private float tickTimer;
        private EnemyController currentTarget;

        private void Awake()
        {
            targeting = GetComponent<TowerTargeting>();
            if (targeting == null)
                targeting = gameObject.AddComponent<TowerTargeting>();

            if (laserLine != null)
                laserLine.enabled = false;
        }

        private void Update()
        {
            currentTarget = targeting.FindNearestEnemy();

            if (currentTarget == null)
            {
                if (laserLine != null)
                    laserLine.enabled = false;
                return;
            }

            UpdateLaserVisual();

            tickTimer += Time.deltaTime;
            if (tickTimer >= tickInterval)
            {
                DealDamage();
                tickTimer = 0f;
            }
        }

        private void DealDamage()
        {
            if (currentTarget == null) return;
            var health = currentTarget.GetComponent<EnemyHealth>();
            if (health != null)
                health.TakeDamage(damagePerTick);
        }

        private void UpdateLaserVisual()
        {
            if (laserLine == null || currentTarget == null) return;

            laserLine.enabled = true;
            laserLine.SetPosition(0, transform.position);
            laserLine.SetPosition(1, currentTarget.transform.position);
        }
    }
}
