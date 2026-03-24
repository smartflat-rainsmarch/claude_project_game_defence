#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using LastLineDefense.Tower;

namespace LastLineDefense.Editor
{
    public static class TowerPrefabCreator
    {
        [MenuItem("Defense/Create All Tower Prefabs (4 types)")]
        public static void CreateAllTowers()
        {
            CreateSplashTower();
            CreateSlowTower();
            CreateLaserTower();
            UpdateBasicTowerProjectile();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.DisplayDialog("Tower Prefabs",
                "4 Tower prefabs ready:\n" +
                "- Tower_Basic (blue)\n" +
                "- Tower_Splash (orange)\n" +
                "- Tower_Slow (cyan)\n" +
                "- Tower_Laser (purple)",
                "OK");
        }

        private static void CreateSplashTower()
        {
            var go = new GameObject("Tower_Splash");
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/_Project/Art/Sprites/BlueSquare.png");
            sr.color = new Color(1f, 0.5f, 0f);
            go.transform.localScale = new Vector3(0.7f, 0.7f, 1f);

            var tower = go.AddComponent<SplashTower>();
            var targeting = go.AddComponent<TowerTargeting>();

            SetField(targeting, "attackRange", 2.5f);

            var firePoint = new GameObject("FirePoint");
            firePoint.transform.SetParent(go.transform);
            firePoint.transform.localPosition = new Vector3(0, 0.5f, 0);

            string path = "Assets/_Project/Prefabs/Towers/Tower_Splash.prefab";
            PrefabUtility.SaveAsPrefabAsset(go, path);
            Object.DestroyImmediate(go);
        }

        private static void CreateSlowTower()
        {
            var go = new GameObject("Tower_Slow");
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/_Project/Art/Sprites/BlueSquare.png");
            sr.color = Color.cyan;
            go.transform.localScale = new Vector3(0.7f, 0.7f, 1f);

            var tower = go.AddComponent<SlowTower>();
            var targeting = go.AddComponent<TowerTargeting>();

            SetField(targeting, "attackRange", 3.5f);

            string path = "Assets/_Project/Prefabs/Towers/Tower_Slow.prefab";
            PrefabUtility.SaveAsPrefabAsset(go, path);
            Object.DestroyImmediate(go);
        }

        private static void CreateLaserTower()
        {
            var go = new GameObject("Tower_Laser");
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/_Project/Art/Sprites/BlueSquare.png");
            sr.color = new Color(0.7f, 0.2f, 0.9f);
            go.transform.localScale = new Vector3(0.7f, 0.7f, 1f);

            var tower = go.AddComponent<LaserTower>();
            var targeting = go.AddComponent<TowerTargeting>();

            SetField(targeting, "attackRange", 4f);

            var lr = go.AddComponent<LineRenderer>();
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = new Color(0.7f, 0.2f, 0.9f);
            lr.endColor = new Color(0.7f, 0.2f, 0.9f);
            lr.positionCount = 2;

            SetField(tower, "laserLine", lr);

            string path = "Assets/_Project/Prefabs/Towers/Tower_Laser.prefab";
            PrefabUtility.SaveAsPrefabAsset(go, path);
            Object.DestroyImmediate(go);
        }

        private static void UpdateBasicTowerProjectile()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Project/Prefabs/Towers/Tower_Basic.prefab");
            if (prefab == null) return;

            var instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            var tower = instance.GetComponent<BasicTower>();
            if (tower != null)
            {
                var projPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Project/Prefabs/Projectiles/Projectile_Basic.prefab");
                SetField(tower, "projectilePrefab", projPrefab);

                var firePoint = instance.transform.Find("FirePoint");
                if (firePoint != null)
                    SetField(tower, "firePoint", firePoint);
            }

            PrefabUtility.SaveAsPrefabAsset(instance, "Assets/_Project/Prefabs/Towers/Tower_Basic.prefab");
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
    }
}
#endif
