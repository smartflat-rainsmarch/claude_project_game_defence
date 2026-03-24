#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using LastLineDefense.Data;

namespace LastLineDefense.Editor
{
    public class BalanceTuner : EditorWindow
    {
        private float enemyHpMultiplier = 1f;
        private float enemySpeedMultiplier = 1f;
        private float towerDamageMultiplier = 1f;
        private float rewardMultiplier = 1f;
        private int goldBonus = 0;

        [MenuItem("Defense/Balance Tuner Window")]
        public static void ShowWindow()
        {
            GetWindow<BalanceTuner>("Balance Tuner");
        }

        private void OnGUI()
        {
            GUILayout.Label("Quick Balance Tuning", EditorStyles.boldLabel);
            GUILayout.Space(10);

            enemyHpMultiplier = EditorGUILayout.Slider("Enemy HP Multiplier", enemyHpMultiplier, 0.5f, 2f);
            enemySpeedMultiplier = EditorGUILayout.Slider("Enemy Speed Multiplier", enemySpeedMultiplier, 0.5f, 2f);
            towerDamageMultiplier = EditorGUILayout.Slider("Tower Damage Multiplier", towerDamageMultiplier, 0.5f, 3f);
            rewardMultiplier = EditorGUILayout.Slider("Reward Multiplier", rewardMultiplier, 0.5f, 3f);
            goldBonus = EditorGUILayout.IntSlider("Starting Gold Bonus", goldBonus, 0, 200);

            GUILayout.Space(20);

            if (GUILayout.Button("Apply to All Enemy Data"))
            {
                ApplyEnemyBalance();
            }

            if (GUILayout.Button("Apply to All Stage Rewards"))
            {
                ApplyRewardBalance();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Reset All to Default"))
            {
                enemyHpMultiplier = 1f;
                enemySpeedMultiplier = 1f;
                towerDamageMultiplier = 1f;
                rewardMultiplier = 1f;
                goldBonus = 0;
                Debug.Log("[BalanceTuner] Reset to defaults");
            }

            GUILayout.Space(20);
            GUILayout.Label("Stage Overview", EditorStyles.boldLabel);

            var stages = FindAllAssets<StageData>("Assets/_Project/ScriptableObjects/Balance");
            foreach (var stage in stages)
            {
                int waveCount = stage.waves != null ? stage.waves.Length : 0;
                EditorGUILayout.LabelField(
                    $"Stage {stage.stageIndex + 1}",
                    $"Gold:{stage.startingGold} HP:{stage.baseHp} Waves:{waveCount} Reward:{stage.clearReward}");
            }
        }

        private void ApplyEnemyBalance()
        {
            var enemies = FindAllAssets<EnemyData>("Assets/_Project/ScriptableObjects/Enemies");
            foreach (var enemy in enemies)
            {
                // Store base values in name for reference
                enemy.maxHp = Mathf.RoundToInt(enemy.maxHp * enemyHpMultiplier);
                enemy.moveSpeed *= enemySpeedMultiplier;
                EditorUtility.SetDirty(enemy);
            }
            AssetDatabase.SaveAssets();
            Debug.Log($"[BalanceTuner] Applied HP x{enemyHpMultiplier}, Speed x{enemySpeedMultiplier} to {enemies.Length} enemies");
        }

        private void ApplyRewardBalance()
        {
            var stages = FindAllAssets<StageData>("Assets/_Project/ScriptableObjects/Balance");
            foreach (var stage in stages)
            {
                stage.clearReward = Mathf.RoundToInt(stage.clearReward * rewardMultiplier);
                stage.failReward = Mathf.RoundToInt(stage.clearReward * 0.3f);
                stage.startingGold += goldBonus;
                EditorUtility.SetDirty(stage);
            }
            AssetDatabase.SaveAssets();
            Debug.Log($"[BalanceTuner] Applied Reward x{rewardMultiplier}, Gold +{goldBonus} to {stages.Length} stages");
        }

        private static T[] FindAllAssets<T>(string folder) where T : ScriptableObject
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { folder });
            var results = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                results[i] = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[i]));
            }
            return results;
        }
    }
}
#endif
