#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using LastLineDefense.Data;

namespace LastLineDefense.Editor
{
    public static class StageDataCreator
    {
        [MenuItem("Defense/Create 10 Stage Data Assets")]
        public static void CreateAllStageData()
        {
            // Create Enemy Data assets first
            var enemyNormal = CreateEnemyData("EnemyData_Normal", "Normal", 30, 2f, 10, 1);
            var enemyFast = CreateEnemyData("EnemyData_Fast", "Fast", 15, 3.5f, 12, 1);
            var enemyTank = CreateEnemyData("EnemyData_Tank", "Tank", 80, 1.2f, 25, 2);
            var enemySplitter = CreateEnemyData("EnemyData_Splitter", "Splitter", 40, 2f, 15, 1);
            var enemyBoss = CreateEnemyData("EnemyData_Boss", "Boss", 200, 1f, 80, 5);

            // Assign prefabs to enemy data
            AssignPrefab(enemyNormal, "Assets/_Project/Prefabs/Enemies/Enemy_Basic.prefab");
            AssignPrefab(enemyFast, "Assets/_Project/Prefabs/Enemies/Enemy_Fast.prefab");
            AssignPrefab(enemyTank, "Assets/_Project/Prefabs/Enemies/Enemy_Tank.prefab");
            AssignPrefab(enemySplitter, "Assets/_Project/Prefabs/Enemies/Enemy_Splitter.prefab");
            AssignPrefab(enemyBoss, "Assets/_Project/Prefabs/Enemies/Enemy_Boss.prefab");

            // Create Tower Data assets
            CreateTowerData("TowerData_Basic", "Basic Tower", 50, 10, 3f, 1f, "Tower_Basic");
            CreateTowerData("TowerData_Splash", "Splash Tower", 80, 8, 2.5f, 1.5f, "Tower_Splash");
            CreateTowerData("TowerData_Slow", "Slow Tower", 60, 0, 3.5f, 2f, "Tower_Slow");
            CreateTowerData("TowerData_Laser", "Laser Tower", 120, 25, 4f, 2.5f, "Tower_Laser");

            // Stage 1: Tutorial
            CreateStage(1, 120, 20, 60, 18, new WaveEntry[]
            {
                new WaveEntry(enemyNormal, 5, 1.5f),
                new WaveEntry(enemyNormal, 7, 1.2f),
                new WaveEntry(enemyNormal, 8, 1.0f),
            });

            // Stage 2: Easy
            CreateStage(2, 120, 20, 75, 23, new WaveEntry[]
            {
                new WaveEntry(enemyNormal, 6, 1.2f),
                new WaveEntry(enemyNormal, 8, 1.0f),
                new WaveEntry(enemyNormal, 6, 1.0f, enemyFast, 3, 0.8f),
            });

            // Stage 3: Splash unlock
            CreateStage(3, 130, 18, 90, 27, new WaveEntry[]
            {
                new WaveEntry(enemyNormal, 8, 1.0f),
                new WaveEntry(enemyFast, 6, 0.8f),
                new WaveEntry(enemyNormal, 6, 1.0f, enemyFast, 4, 0.8f),
                new WaveEntry(enemyTank, 3, 2.0f),
            });

            // Stage 4
            CreateStage(4, 140, 18, 110, 33, new WaveEntry[]
            {
                new WaveEntry(enemyNormal, 6, 1.0f, enemyFast, 4, 0.8f),
                new WaveEntry(enemyTank, 2, 1.5f, enemyNormal, 6, 1.0f),
                new WaveEntry(enemyFast, 10, 0.7f),
                new WaveEntry(enemyTank, 3, 1.2f, enemyFast, 5, 0.8f),
            });

            // Stage 5: Slow unlock + first boss
            CreateStage(5, 150, 16, 130, 39, new WaveEntry[]
            {
                new WaveEntry(enemyNormal, 10, 0.8f),
                new WaveEntry(enemySplitter, 4, 1.5f),
                new WaveEntry(enemyFast, 6, 0.8f, enemyTank, 3, 1.2f),
                new WaveEntry(enemyBoss, 1, 1f),
            });

            // Stage 6
            CreateStage(6, 160, 16, 150, 45, new WaveEntry[]
            {
                new WaveEntry(enemyNormal, 8, 0.8f, enemyFast, 5, 0.7f),
                new WaveEntry(enemySplitter, 3, 1.2f, enemyNormal, 6, 0.8f),
                new WaveEntry(enemyTank, 5, 1.2f),
                new WaveEntry(enemyFast, 12, 0.6f),
                new WaveEntry(enemySplitter, 4, 1.0f, enemyTank, 3, 1.0f),
            });

            // Stage 7
            CreateStage(7, 170, 15, 175, 53, new WaveEntry[]
            {
                new WaveEntry(enemyNormal, 8, 0.8f, enemySplitter, 3, 1.0f),
                new WaveEntry(enemyFast, 8, 0.7f, enemyTank, 4, 1.0f),
                new WaveEntry(enemySplitter, 6, 0.8f),
                new WaveEntry(enemyTank, 5, 0.8f, enemyNormal, 8, 0.7f),
                new WaveEntry(enemyBoss, 1, 1f, enemyFast, 6, 0.8f),
            });

            // Stage 8: Laser unlock
            CreateStage(8, 180, 14, 200, 60, new WaveEntry[]
            {
                new WaveEntry(enemyFast, 15, 0.5f),
                new WaveEntry(enemyTank, 5, 1.0f, enemySplitter, 4, 0.8f),
                new WaveEntry(enemyNormal, 6, 0.7f, enemyFast, 6, 0.6f, enemyTank, 3, 1.0f),
                new WaveEntry(enemySplitter, 8, 0.7f),
                new WaveEntry(enemyBoss, 1, 1f, enemyTank, 4, 1.0f),
            });

            // Stage 9
            CreateStage(9, 190, 13, 235, 71, new WaveEntry[]
            {
                new WaveEntry(enemyNormal, 10, 0.5f, enemyFast, 8, 0.5f),
                new WaveEntry(enemyTank, 6, 0.8f, enemySplitter, 5, 0.8f),
                new WaveEntry(enemyFast, 20, 0.4f),
                new WaveEntry(enemyBoss, 2, 3.0f),
                new WaveEntry(enemySplitter, 5, 0.6f, enemyTank, 4, 0.6f, enemyFast, 8, 0.5f),
                new WaveEntry(enemyTank, 5, 1.0f, enemyBoss, 1, 1f),
            });

            // Stage 10: Final boss
            CreateStage(10, 210, 12, 280, 84, new WaveEntry[]
            {
                new WaveEntry(enemyNormal, 8, 0.5f, enemyFast, 6, 0.5f, enemySplitter, 4, 0.5f),
                new WaveEntry(enemyTank, 6, 0.7f, enemySplitter, 6, 0.7f),
                new WaveEntry(enemyFast, 25, 0.3f),
                new WaveEntry(enemyBoss, 2, 2.0f, enemyTank, 5, 1.0f),
                new WaveEntry(enemySplitter, 6, 0.5f, enemyFast, 10, 0.4f, enemyTank, 4, 0.5f),
                new WaveEntry(enemyBoss, 1, 1f), // Final boss (double HP applied via stage modifier)
            });

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.DisplayDialog("Stage Data Created",
                "Created:\n" +
                "- 5 Enemy Data assets\n" +
                "- 4 Tower Data assets\n" +
                "- 10 Stage Data assets (with wave configs)\n\n" +
                "All in Assets/_Project/ScriptableObjects/",
                "OK");
        }

        private struct WaveEntry
        {
            public EnemyData enemy1; public int count1; public float interval1;
            public EnemyData enemy2; public int count2; public float interval2;
            public EnemyData enemy3; public int count3; public float interval3;

            public WaveEntry(EnemyData e1, int c1, float i1)
            {
                enemy1 = e1; count1 = c1; interval1 = i1;
                enemy2 = null; count2 = 0; interval2 = 0;
                enemy3 = null; count3 = 0; interval3 = 0;
            }

            public WaveEntry(EnemyData e1, int c1, float i1, EnemyData e2, int c2, float i2)
            {
                enemy1 = e1; count1 = c1; interval1 = i1;
                enemy2 = e2; count2 = c2; interval2 = i2;
                enemy3 = null; count3 = 0; interval3 = 0;
            }

            public WaveEntry(EnemyData e1, int c1, float i1, EnemyData e2, int c2, float i2, EnemyData e3, int c3, float i3)
            {
                enemy1 = e1; count1 = c1; interval1 = i1;
                enemy2 = e2; count2 = c2; interval2 = i2;
                enemy3 = e3; count3 = c3; interval3 = i3;
            }
        }

        private static EnemyData CreateEnemyData(string name, string displayName, int hp, float speed, int gold, int damage)
        {
            EnsureDir("Assets/_Project/ScriptableObjects/Enemies");
            string path = $"Assets/_Project/ScriptableObjects/Enemies/{name}.asset";

            var existing = AssetDatabase.LoadAssetAtPath<EnemyData>(path);
            if (existing != null) return existing;

            var data = ScriptableObject.CreateInstance<EnemyData>();
            data.enemyId = name;
            data.displayName = displayName;
            data.maxHp = hp;
            data.moveSpeed = speed;
            data.rewardGold = gold;
            data.baseDamage = damage;

            AssetDatabase.CreateAsset(data, path);
            return data;
        }

        private static void AssignPrefab(EnemyData data, string prefabPath)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab != null)
                data.prefab = prefab;
            EditorUtility.SetDirty(data);
        }

        private static void CreateTowerData(string name, string displayName, int cost, int damage, float range, float interval, string prefabName)
        {
            EnsureDir("Assets/_Project/ScriptableObjects/Towers");
            string path = $"Assets/_Project/ScriptableObjects/Towers/{name}.asset";
            if (AssetDatabase.LoadAssetAtPath<TowerData>(path) != null) return;

            var data = ScriptableObject.CreateInstance<TowerData>();
            data.towerId = name;
            data.displayName = displayName;
            data.buildCost = cost;
            data.damage = damage;
            data.attackRange = range;
            data.attackInterval = interval;
            data.prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/_Project/Prefabs/Towers/{prefabName}.prefab");
            data.projectilePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Project/Prefabs/Projectiles/Projectile_Basic.prefab");

            AssetDatabase.CreateAsset(data, path);
        }

        private static void CreateStage(int stageNum, int startGold, int baseHp, int clearReward, int failReward, WaveEntry[] waves)
        {
            EnsureDir("Assets/_Project/ScriptableObjects/Waves");
            EnsureDir("Assets/_Project/ScriptableObjects/Balance");
            string path = $"Assets/_Project/ScriptableObjects/Balance/StageData_{stageNum:00}.asset";
            if (AssetDatabase.LoadAssetAtPath<StageData>(path) != null) return;

            var stageData = ScriptableObject.CreateInstance<StageData>();
            stageData.stageIndex = stageNum - 1;
            stageData.startingGold = startGold;
            stageData.baseHp = baseHp;
            stageData.clearReward = clearReward;
            stageData.failReward = failReward;

            stageData.waves = new WaveData[waves.Length];
            for (int i = 0; i < waves.Length; i++)
            {
                var waveData = ScriptableObject.CreateInstance<WaveData>();
                waveData.preWaveDelay = 3f;

                var groups = new System.Collections.Generic.List<EnemyGroup>();
                if (waves[i].enemy1 != null)
                    groups.Add(new EnemyGroup { enemyData = waves[i].enemy1, count = waves[i].count1, spawnInterval = waves[i].interval1 });
                if (waves[i].enemy2 != null)
                    groups.Add(new EnemyGroup { enemyData = waves[i].enemy2, count = waves[i].count2, spawnInterval = waves[i].interval2 });
                if (waves[i].enemy3 != null)
                    groups.Add(new EnemyGroup { enemyData = waves[i].enemy3, count = waves[i].count3, spawnInterval = waves[i].interval3 });

                waveData.enemyGroups = groups.ToArray();

                string wavePath = $"Assets/_Project/ScriptableObjects/Waves/Stage{stageNum:00}_Wave{i + 1:00}.asset";
                AssetDatabase.CreateAsset(waveData, wavePath);
                stageData.waves[i] = waveData;
            }

            AssetDatabase.CreateAsset(stageData, path);
        }

        private static void EnsureDir(string path)
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
