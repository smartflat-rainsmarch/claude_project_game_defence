#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace LastLineDefense.Editor
{
    public static class AndroidBuildSetup
    {
        [MenuItem("Defense/Setup Android Build Settings")]
        public static void SetupAndroid()
        {
            // Switch to Android
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

            // Package name
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.lastlinedefense.game");

            // Version
            PlayerSettings.bundleVersion = "0.1.0";
            PlayerSettings.Android.bundleVersionCode = 1;

            // Minimum API level
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;

            // ARM64
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;

            // Build format: AAB for Play Store
            EditorUserBuildSettings.buildAppBundle = true;

            // Orientation
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;

            // Product name
            PlayerSettings.productName = "Last Line Defense";
            PlayerSettings.companyName = "YourCompany";

            Debug.Log("[AndroidBuild] Settings configured");

            EditorUtility.DisplayDialog("Android Build Setup",
                "Android build settings configured:\n\n" +
                "- Package: com.lastlinedefense.game\n" +
                "- Version: 0.1.0 (1)\n" +
                "- Min SDK: API 24\n" +
                "- IL2CPP + ARM64\n" +
                "- AAB format\n" +
                "- Portrait orientation\n\n" +
                "Next: Build > Build And Run",
                "OK");
        }

        [MenuItem("Defense/Build Android APK (Debug)")]
        public static void BuildDebugAPK()
        {
            EditorUserBuildSettings.buildAppBundle = false;

            string path = EditorUtility.SaveFilePanel("Save APK", "", "LastLineDefense_debug", "apk");
            if (string.IsNullOrEmpty(path)) return;

            var options = new BuildPlayerOptions
            {
                scenes = new string[]
                {
                    "Assets/_Project/Scenes/Boot.unity",
                    "Assets/_Project/Scenes/Lobby.unity",
                    "Assets/_Project/Scenes/Stage.unity"
                },
                locationPathName = path,
                target = BuildTarget.Android,
                options = BuildOptions.Development
            };

            var report = BuildPipeline.BuildPlayer(options);
            if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                EditorUtility.DisplayDialog("Build Success",
                    $"APK built: {path}\nSize: {report.summary.totalSize / 1024 / 1024}MB",
                    "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Build Failed",
                    $"Errors: {report.summary.totalErrors}",
                    "OK");
            }
        }
    }
}
#endif
