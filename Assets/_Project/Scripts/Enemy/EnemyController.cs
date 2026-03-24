using UnityEngine;
using LastLineDefense.Core;

namespace LastLineDefense.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Enemy Settings")]
        [SerializeField] private int rewardGold = 10;
        [SerializeField] private int baseDamage = 1;

        private EnemyMover mover;
        private EnemyHealth health;
        private bool isDead;

        private void Awake()
        {
            mover = GetComponent<EnemyMover>();
            health = GetComponent<EnemyHealth>();
        }

        private void OnEnable()
        {
            if (health != null)
                health.OnDeath += HandleDeath;

            if (mover != null)
                mover.OnReachedEnd += HandleReachedBase;
        }

        private void OnDisable()
        {
            if (health != null)
                health.OnDeath -= HandleDeath;

            if (mover != null)
                mover.OnReachedEnd -= HandleReachedBase;
        }

        public void Initialize(Transform[] waypoints, int goldReward, int damage)
        {
            rewardGold = goldReward;
            baseDamage = damage;
            isDead = false;

            if (mover != null)
                mover.SetWaypoints(waypoints);
        }

        private void HandleDeath()
        {
            if (isDead) return;
            isDead = true;

            GameEvents.EnemyKilled();

            var currencyManager = FindFirstObjectByType<Game.CurrencyManager>();
            if (currencyManager != null)
                currencyManager.AddGold(rewardGold);

            Destroy(gameObject);
        }

        private void HandleReachedBase()
        {
            if (isDead) return;
            isDead = true;

            GameEvents.EnemyReachedBase(baseDamage);

            var waveManager = FindFirstObjectByType<Wave.WaveManager>();
            if (waveManager != null)
                waveManager.NotifyEnemyReachedBase();

            Destroy(gameObject);
        }
    }
}
