using System;
using System.Collections;
using UnityEngine;

namespace LastLineDefense.Wave
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int enemiesPerWave = 8;
        [SerializeField] private float spawnInterval = 1.0f;

        [Header("Runtime Parent")]
        [SerializeField] private Transform enemyParent;

        public void SpawnWave(int waveIndex, Action<int> onSpawnComplete)
        {
            int count = enemiesPerWave + (waveIndex * 2);
            float interval = Mathf.Max(0.3f, spawnInterval - (waveIndex * 0.1f));

            StartCoroutine(SpawnRoutine(count, interval, onSpawnComplete));
        }

        private IEnumerator SpawnRoutine(int count, float interval, Action<int> onSpawnComplete)
        {
            onSpawnComplete?.Invoke(count);

            for (int i = 0; i < count; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(interval);
            }
        }

        private void SpawnEnemy()
        {
            if (enemyPrefab == null || spawnPoint == null) return;

            Transform parent = enemyParent != null ? enemyParent : transform;
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, parent);
        }
    }
}
