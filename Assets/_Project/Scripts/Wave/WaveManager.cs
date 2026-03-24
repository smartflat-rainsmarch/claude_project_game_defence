using UnityEngine;
using LastLineDefense.Core;

namespace LastLineDefense.Wave
{
    public class WaveManager : MonoBehaviour
    {
        [Header("Wave Settings")]
        [SerializeField] private float timeBetweenWaves = 3f;

        private int totalWaves;
        private int currentWaveIndex;
        private int enemiesAliveInWave;
        private bool waveInProgress;
        private bool allWavesComplete;

        private EnemySpawner enemySpawner;
        private Game.StageManager stageManager;

        public int CurrentWave => currentWaveIndex + 1;
        public int TotalWaves => totalWaves;
        public bool AllWavesComplete => allWavesComplete;

        private void Awake()
        {
            enemySpawner = GetComponentInChildren<EnemySpawner>();
            if (enemySpawner == null)
                enemySpawner = FindFirstObjectByType<EnemySpawner>();

            stageManager = GetComponentInChildren<Game.StageManager>();
            if (stageManager == null)
                stageManager = FindFirstObjectByType<Game.StageManager>();
        }

        private void OnEnable()
        {
            GameEvents.OnEnemyKilled += HandleEnemyKilled;
        }

        private void OnDisable()
        {
            GameEvents.OnEnemyKilled -= HandleEnemyKilled;
        }

        public void InitializeWaves(int waveCount)
        {
            totalWaves = waveCount;
            currentWaveIndex = 0;
            allWavesComplete = false;
            waveInProgress = false;
            GameEvents.WaveChanged(1, totalWaves);
        }

        public void StartWaveFlow()
        {
            if (totalWaves <= 0) return;
            StartNextWave();
        }

        private void StartNextWave()
        {
            if (currentWaveIndex >= totalWaves)
            {
                allWavesComplete = true;
                if (stageManager != null)
                    stageManager.HandleAllWavesCleared();
                return;
            }

            waveInProgress = true;
            GameEvents.WaveChanged(currentWaveIndex + 1, totalWaves);

            if (enemySpawner != null)
            {
                enemySpawner.SpawnWave(currentWaveIndex, (spawnedCount) =>
                {
                    enemiesAliveInWave = spawnedCount;
                });
            }

            Debug.Log($"[WaveManager] Wave {currentWaveIndex + 1}/{totalWaves} started");
        }

        private void HandleEnemyKilled()
        {
            if (!waveInProgress) return;

            enemiesAliveInWave--;

            if (enemiesAliveInWave <= 0)
            {
                waveInProgress = false;
                currentWaveIndex++;
                Debug.Log($"[WaveManager] Wave cleared. Next: {currentWaveIndex + 1}/{totalWaves}");
                Invoke(nameof(StartNextWave), timeBetweenWaves);
            }
        }

        public void NotifyEnemyReachedBase()
        {
            if (!waveInProgress) return;
            enemiesAliveInWave--;

            if (enemiesAliveInWave <= 0)
            {
                waveInProgress = false;
                currentWaveIndex++;
                Invoke(nameof(StartNextWave), timeBetweenWaves);
            }
        }
    }
}
