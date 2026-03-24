using UnityEngine;
using LastLineDefense.Enemy;

namespace LastLineDefense.Tower
{
    public class SlowTower : MonoBehaviour
    {
        [Header("Slow Settings")]
        [SerializeField] private float slowPercent = 0.3f;
        [SerializeField] private float slowDuration = 2f;
        [SerializeField] private float attackInterval = 2f;

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
                TryApplySlow();
                attackTimer = 0f;
            }
        }

        private void TryApplySlow()
        {
            EnemyController target = targeting.FindNearestEnemy();
            if (target == null) return;

            var mover = target.GetComponent<EnemyMover>();
            if (mover != null)
            {
                StartCoroutine(ApplySlowEffect(mover));
            }
        }

        private System.Collections.IEnumerator ApplySlowEffect(EnemyMover mover)
        {
            if (mover == null) yield break;

            float originalSpeed = 2f;
            mover.SetSpeed(originalSpeed * (1f - slowPercent));

            yield return new WaitForSeconds(slowDuration);

            if (mover != null)
                mover.SetSpeed(originalSpeed);
        }
    }
}
