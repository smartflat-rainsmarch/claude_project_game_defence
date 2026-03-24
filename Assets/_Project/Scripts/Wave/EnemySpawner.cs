using System;
using System.Collections;
using UnityEngine;
using LastLineDefense.Enemy;

namespace LastLineDefense.Wave
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int enemiesPerWave = 8;
        [SerializeField] private float spawnInterval = 1.0f;

        [Header("Route")]
        [SerializeField] private Transform routeRoot;

        [Header("Runtime Parent")]
        [SerializeField] private Transform enemyParent;

        private Transform[] waypoints;

        private void Awake()
        {
            if (routeRoot == null)
                routeRoot = GameObject.Find("RouteRoot")?.transform;

            CacheWaypoints();
        }

        private void CacheWaypoints()
        {
            if (routeRoot == null) return;

            waypoints = new Transform[routeRoot.childCount];
            for (int i = 0; i < routeRoot.childCount; i++)
            {
                waypoints[i] = routeRoot.GetChild(i);
            }
        }

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
            var go = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, parent);

            var controller = go.GetComponent<EnemyController>();
            if (controller != null && waypoints != null)
            {
                controller.Initialize(waypoints, 10, 1);
            }
        }
    }
}
