using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ProjectBootstrap
{
    private static readonly string[] SceneNames = { "Boot", "Lobby", "Stage" };
    private const string ScenesFolder = "Assets/_Project/Scenes";

    public static void Setup()
    {
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "_Project"));
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "_Project", "Scenes"));

        EditorSettings.defaultBehaviorMode = EditorBehaviorMode.Mode2D;

        var sceneAssets = new EditorBuildSettingsScene[SceneNames.Length];

        for (var i = 0; i < SceneNames.Length; i++)
        {
            var sceneName = SceneNames[i];
            var scenePath = $"{ScenesFolder}/{sceneName}.unity";
            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            scene.name = sceneName;

            var mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.orthographic = true;
                mainCamera.transform.position = new Vector3(0f, 0f, -10f);
            }

            var root = new GameObject($"{sceneName}Root");
            SceneManager.MoveGameObjectToScene(root, scene);

            EditorSceneManager.SaveScene(scene, scenePath);
            sceneAssets[i] = new EditorBuildSettingsScene(scenePath, true);
        }

        EditorBuildSettings.scenes = sceneAssets;
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorApplication.Exit(0);
    }
}
