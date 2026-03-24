#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace LastLineDefense.Editor
{
    public static class SpriteGenerator
    {
        [MenuItem("Defense/Generate Sprites and Fix Visuals", priority = 1)]
        public static void GenerateAndAssign()
        {
            EnsureDirectory("Assets/_Project/Art/Sprites");

            // Create basic sprites
            var whiteSquare = CreateSquareSprite("WhiteSquare", Color.white, 32);
            var redSquare = CreateSquareSprite("RedSquare", Color.red, 32);
            var blueSquare = CreateSquareSprite("BlueSquare", Color.blue, 32);
            var greenSquare = CreateSquareSprite("GreenSquare", new Color(0.3f, 0.8f, 0.3f), 32);
            var yellowCircle = CreateCircleSprite("YellowCircle", Color.yellow, 16);
            var darkSquare = CreateSquareSprite("DarkSquare", new Color(0.2f, 0.2f, 0.3f), 32);

            // Fix Enemy prefab
            var enemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Project/Prefabs/Enemies/Enemy_Basic.prefab");
            if (enemyPrefab != null)
            {
                var instance = PrefabUtility.InstantiatePrefab(enemyPrefab) as GameObject;
                var sr = instance.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sprite = redSquare;
                    sr.color = Color.red;
                    instance.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                }
                PrefabUtility.SaveAsPrefabAsset(instance, "Assets/_Project/Prefabs/Enemies/Enemy_Basic.prefab");
                Object.DestroyImmediate(instance);
                Debug.Log("[SpriteGen] Enemy prefab updated with red square sprite");
            }

            // Fix Tower prefab
            var towerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Project/Prefabs/Towers/Tower_Basic.prefab");
            if (towerPrefab != null)
            {
                var instance = PrefabUtility.InstantiatePrefab(towerPrefab) as GameObject;
                var sr = instance.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sprite = blueSquare;
                    sr.color = Color.blue;
                    instance.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
                }
                PrefabUtility.SaveAsPrefabAsset(instance, "Assets/_Project/Prefabs/Towers/Tower_Basic.prefab");
                Object.DestroyImmediate(instance);
                Debug.Log("[SpriteGen] Tower prefab updated with blue square sprite");
            }

            // Fix Projectile prefab
            var projPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Project/Prefabs/Projectiles/Projectile_Basic.prefab");
            if (projPrefab != null)
            {
                var instance = PrefabUtility.InstantiatePrefab(projPrefab) as GameObject;
                var sr = instance.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sprite = yellowCircle;
                    sr.color = Color.yellow;
                    instance.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
                }
                PrefabUtility.SaveAsPrefabAsset(instance, "Assets/_Project/Prefabs/Projectiles/Projectile_Basic.prefab");
                Object.DestroyImmediate(instance);
                Debug.Log("[SpriteGen] Projectile prefab updated with yellow circle sprite");
            }

            // Fix TowerSlots in Stage scene
            FixStageSceneSlots(greenSquare);

            // Add waypoint visuals
            AddWaypointVisuals(darkSquare);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("=== SPRITES GENERATED AND ASSIGNED ===");
            EditorUtility.DisplayDialog("Sprites Fixed",
                "All sprites generated and assigned:\n\n" +
                "- Enemy: Red square\n" +
                "- Tower: Blue square\n" +
                "- Projectile: Yellow circle\n" +
                "- Tower Slots: Green square\n" +
                "- Waypoints: Dark markers\n\n" +
                "Open Boot scene and press Play!",
                "OK");
        }

        private static void FixStageSceneSlots(Sprite greenSprite)
        {
            var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(
                "Assets/_Project/Scenes/Stage.unity");

            var slots = Object.FindObjectsByType<Tower.TowerSlot>();
            foreach (var slot in slots)
            {
                var sr = slot.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sprite = greenSprite;
                    sr.color = new Color(0.3f, 0.8f, 0.3f, 0.7f);
                    slot.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
                }
            }

            // Add visual to SpawnPoint
            var spawnPoint = GameObject.Find("SpawnPoint");
            if (spawnPoint != null)
            {
                var sr = spawnPoint.GetComponent<SpriteRenderer>();
                if (sr == null) sr = spawnPoint.AddComponent<SpriteRenderer>();
                sr.sprite = greenSprite;
                sr.color = new Color(1f, 0.5f, 0f, 0.5f);
                spawnPoint.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
            }

            // Add visual to BasePoint
            var basePoint = GameObject.Find("BasePoint");
            if (basePoint != null)
            {
                var sr = basePoint.GetComponent<SpriteRenderer>();
                if (sr == null) sr = basePoint.AddComponent<SpriteRenderer>();
                sr.sprite = greenSprite;
                sr.color = new Color(1f, 0f, 0f, 0.5f);
                basePoint.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
            }

            UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene);
        }

        private static void AddWaypointVisuals(Sprite markerSprite)
        {
            var routeRoot = GameObject.Find("RouteRoot");
            if (routeRoot == null) return;

            foreach (Transform wp in routeRoot.transform)
            {
                var sr = wp.GetComponent<SpriteRenderer>();
                if (sr == null) sr = wp.gameObject.AddComponent<SpriteRenderer>();
                sr.sprite = markerSprite;
                sr.color = new Color(0.5f, 0.5f, 0.5f, 0.4f);
                wp.localScale = new Vector3(0.3f, 0.3f, 1f);
            }

            // Draw path line using LineRenderer
            var lr = routeRoot.GetComponent<LineRenderer>();
            if (lr == null) lr = routeRoot.AddComponent<LineRenderer>();
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = new Color(0.4f, 0.4f, 0.4f, 0.6f);
            lr.endColor = new Color(0.4f, 0.4f, 0.4f, 0.6f);
            lr.positionCount = routeRoot.transform.childCount;
            for (int i = 0; i < routeRoot.transform.childCount; i++)
            {
                lr.SetPosition(i, routeRoot.transform.GetChild(i).position);
            }

            UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
        }

        private static Sprite CreateSquareSprite(string name, Color color, int size)
        {
            var tex = new Texture2D(size, size);
            var pixels = new Color[size * size];
            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = color;
            tex.SetPixels(pixels);
            tex.Apply();

            string path = $"Assets/_Project/Art/Sprites/{name}.png";
            System.IO.File.WriteAllBytes(path, tex.EncodeToPNG());
            AssetDatabase.ImportAsset(path);

            var importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer != null)
            {
                importer.textureType = TextureImporterType.Sprite;
                importer.spritePixelsPerUnit = 32;
                importer.filterMode = FilterMode.Point;
                importer.SaveAndReimport();
            }

            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }

        private static Sprite CreateCircleSprite(string name, Color color, int radius)
        {
            int size = radius * 2;
            var tex = new Texture2D(size, size);
            var pixels = new Color[size * size];

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float dx = x - radius + 0.5f;
                    float dy = y - radius + 0.5f;
                    if (dx * dx + dy * dy <= radius * radius)
                        pixels[y * size + x] = color;
                    else
                        pixels[y * size + x] = Color.clear;
                }
            }
            tex.SetPixels(pixels);
            tex.Apply();

            string path = $"Assets/_Project/Art/Sprites/{name}.png";
            System.IO.File.WriteAllBytes(path, tex.EncodeToPNG());
            AssetDatabase.ImportAsset(path);

            var importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer != null)
            {
                importer.textureType = TextureImporterType.Sprite;
                importer.spritePixelsPerUnit = 32;
                importer.filterMode = FilterMode.Point;
                importer.SaveAndReimport();
            }

            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
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
