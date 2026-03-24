#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace LastLineDefense.Editor
{
    public static class BuildSettingsSetup
    {
        [MenuItem("Defense/Setup Build Scenes")]
        public static void SetupBuildScenes()
        {
            var scenes = new EditorBuildSettingsScene[]
            {
                new EditorBuildSettingsScene("Assets/_Project/Scenes/Boot.unity", true),
                new EditorBuildSettingsScene("Assets/_Project/Scenes/Lobby.unity", true),
                new EditorBuildSettingsScene("Assets/_Project/Scenes/Stage.unity", true)
            };

            EditorBuildSettings.scenes = scenes;
            Debug.Log("[BuildSettings] Scenes registered: Boot(0), Lobby(1), Stage(2)");
        }

        [MenuItem("Defense/Create Test Enemy Prefab")]
        public static void CreateTestEnemyPrefab()
        {
            var go = new GameObject("Enemy_Basic");
            var sr = go.AddComponent<SpriteRenderer>();
            sr.color = Color.red;

            go.AddComponent<BoxCollider2D>();
            go.AddComponent<Enemy.EnemyController>();
            go.AddComponent<Enemy.EnemyMover>();
            go.AddComponent<Enemy.EnemyHealth>();

            string path = "Assets/_Project/Prefabs/Enemies/Enemy_Basic.prefab";
            EnsureDirectory("Assets/_Project/Prefabs/Enemies");
            PrefabUtility.SaveAsPrefabAsset(go, path);
            Object.DestroyImmediate(go);

            Debug.Log($"[Setup] Enemy prefab created at {path}");
        }

        [MenuItem("Defense/Create Test Tower Prefab")]
        public static void CreateTestTowerPrefab()
        {
            var go = new GameObject("Tower_Basic");
            var sr = go.AddComponent<SpriteRenderer>();
            sr.color = Color.blue;

            go.AddComponent<Tower.BasicTower>();
            go.AddComponent<Tower.TowerTargeting>();

            var firePoint = new GameObject("FirePoint");
            firePoint.transform.SetParent(go.transform);
            firePoint.transform.localPosition = new Vector3(0, 0.5f, 0);

            string path = "Assets/_Project/Prefabs/Towers/Tower_Basic.prefab";
            EnsureDirectory("Assets/_Project/Prefabs/Towers");
            PrefabUtility.SaveAsPrefabAsset(go, path);
            Object.DestroyImmediate(go);

            Debug.Log($"[Setup] Tower prefab created at {path}");
        }

        [MenuItem("Defense/Create Test Projectile Prefab")]
        public static void CreateTestProjectilePrefab()
        {
            var go = new GameObject("Projectile_Basic");
            var sr = go.AddComponent<SpriteRenderer>();
            sr.color = Color.white;
            sr.transform.localScale = new Vector3(0.2f, 0.2f, 1f);

            go.AddComponent<Tower.ProjectileController>();

            string path = "Assets/_Project/Prefabs/Projectiles/Projectile_Basic.prefab";
            EnsureDirectory("Assets/_Project/Prefabs/Projectiles");
            PrefabUtility.SaveAsPrefabAsset(go, path);
            Object.DestroyImmediate(go);

            Debug.Log($"[Setup] Projectile prefab created at {path}");
        }

        [MenuItem("Defense/Create All Test Prefabs")]
        public static void CreateAllPrefabs()
        {
            CreateTestEnemyPrefab();
            CreateTestTowerPrefab();
            CreateTestProjectilePrefab();
            Debug.Log("[Setup] All test prefabs created!");
        }

        private static void EnsureDirectory(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                string[] parts = path.Split('/');
                string current = parts[0];
                for (int i = 1; i < parts.Length; i++)
                {
                    string next = current + "/" + parts[i];
                    if (!AssetDatabase.IsValidFolder(next))
                        AssetDatabase.CreateFolder(current, parts[i]);
                    current = next;
                }
            }
        }
    }
}
#endif
