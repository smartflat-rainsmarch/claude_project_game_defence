#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using LastLineDefense.Enemy;

namespace LastLineDefense.Editor
{
    public static class EnemyPrefabCreator
    {
        [MenuItem("Defense/Create All Enemy Prefabs (5 types)")]
        public static void CreateAllEnemies()
        {
            CreateEnemy("Enemy_Fast", new Color(1f, 0.5f, 0f), 0.4f, 15, 3.5f, 12, 1);
            CreateEnemy("Enemy_Tank", new Color(0.5f, 0.5f, 0.5f), 0.8f, 80, 1.2f, 25, 2);
            CreateEnemy("Enemy_Splitter", new Color(0.8f, 0.2f, 0.8f), 0.5f, 40, 2f, 15, 1);
            CreateEnemy("Enemy_Boss", new Color(0.8f, 0.1f, 0.1f), 1.2f, 200, 1f, 80, 5);

            // Update basic enemy stats
            UpdateBasicEnemy();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.DisplayDialog("Enemy Prefabs",
                "5 Enemy prefabs ready:\n" +
                "- Enemy_Basic (red, HP:30)\n" +
                "- Enemy_Fast (orange, HP:15, fast)\n" +
                "- Enemy_Tank (gray, HP:80, slow)\n" +
                "- Enemy_Splitter (purple, HP:40)\n" +
                "- Enemy_Boss (dark red, HP:200, big)",
                "OK");
        }

        private static void CreateEnemy(string name, Color color, float scale, int hp, float speed, int gold, int damage)
        {
            var go = new GameObject(name);
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/_Project/Art/Sprites/RedSquare.png");
            sr.color = color;
            go.transform.localScale = new Vector3(scale, scale, 1f);

            go.AddComponent<BoxCollider2D>();

            var controller = go.AddComponent<EnemyController>();
            SetField(controller, "rewardGold", gold);
            SetField(controller, "baseDamage", damage);

            var mover = go.AddComponent<EnemyMover>();
            SetField(mover, "moveSpeed", speed);

            var health = go.AddComponent<EnemyHealth>();
            SetField(health, "maxHp", hp);

            string path = $"Assets/_Project/Prefabs/Enemies/{name}.prefab";
            EnsureDirectory("Assets/_Project/Prefabs/Enemies");
            PrefabUtility.SaveAsPrefabAsset(go, path);
            Object.DestroyImmediate(go);
        }

        private static void UpdateBasicEnemy()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Project/Prefabs/Enemies/Enemy_Basic.prefab");
            if (prefab == null) return;

            var instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            var health = instance.GetComponent<EnemyHealth>();
            if (health != null) SetField(health, "maxHp", 30);
            var mover = instance.GetComponent<EnemyMover>();
            if (mover != null) SetField(mover, "moveSpeed", 2f);
            var controller = instance.GetComponent<EnemyController>();
            if (controller != null)
            {
                SetField(controller, "rewardGold", 10);
                SetField(controller, "baseDamage", 1);
            }

            PrefabUtility.SaveAsPrefabAsset(instance, "Assets/_Project/Prefabs/Enemies/Enemy_Basic.prefab");
            Object.DestroyImmediate(instance);
        }

        private static void SetField(object target, string fieldName, object value)
        {
            var type = target.GetType();
            while (type != null)
            {
                var field = type.GetField(fieldName,
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance);
                if (field != null) { field.SetValue(target, value); return; }
                type = type.BaseType;
            }
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
