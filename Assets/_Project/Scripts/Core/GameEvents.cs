using System;

namespace LastLineDefense.Core
{
    public static class GameEvents
    {
        public static event Action<int> OnGoldChanged;
        public static event Action<int, int> OnWaveChanged;
        public static event Action<int> OnBaseHpChanged;
        public static event Action OnStageCleared;
        public static event Action OnStageFailed;
        public static event Action OnEnemyKilled;
        public static event Action<int> OnEnemyReachedBase;

        public static void GoldChanged(int currentGold)
        {
            OnGoldChanged?.Invoke(currentGold);
        }

        public static void WaveChanged(int currentWave, int totalWaves)
        {
            OnWaveChanged?.Invoke(currentWave, totalWaves);
        }

        public static void BaseHpChanged(int currentHp)
        {
            OnBaseHpChanged?.Invoke(currentHp);
        }

        public static void StageCleared()
        {
            OnStageCleared?.Invoke();
        }

        public static void StageFailed()
        {
            OnStageFailed?.Invoke();
        }

        public static void EnemyKilled()
        {
            OnEnemyKilled?.Invoke();
        }

        public static void EnemyReachedBase(int damage)
        {
            OnEnemyReachedBase?.Invoke(damage);
        }

        public static void ClearAll()
        {
            OnGoldChanged = null;
            OnWaveChanged = null;
            OnBaseHpChanged = null;
            OnStageCleared = null;
            OnStageFailed = null;
            OnEnemyKilled = null;
            OnEnemyReachedBase = null;
        }
    }
}
