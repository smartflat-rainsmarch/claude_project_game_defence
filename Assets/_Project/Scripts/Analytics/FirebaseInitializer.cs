using UnityEngine;

namespace LastLineDefense.Analytics
{
    /// <summary>
    /// Firebase initialization handler.
    /// Place on Boot scene Managers GO.
    ///
    /// Setup:
    /// 1. Download google-services.json from Firebase Console
    /// 2. Place in Assets/ folder
    /// 3. Install Firebase Unity SDK (Analytics + Crashlytics)
    /// 4. Add this component to Managers in Boot scene
    /// </summary>
    public class FirebaseInitializer : MonoBehaviour
    {
        private bool firebaseReady;

        public bool IsReady => firebaseReady;

        private void Start()
        {
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            Debug.Log("[Firebase] Initializing (stub — install Firebase SDK to activate)");

            // Uncomment when Firebase SDK is installed:
            // Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            // {
            //     if (task.Result == Firebase.DependencyStatus.Available)
            //     {
            //         firebaseReady = true;
            //         Debug.Log("[Firebase] Ready");
            //
            //         // Set Crashlytics custom keys
            //         Firebase.Crashlytics.Crashlytics.SetCustomKey("app_version", Application.version);
            //
            //         // Log app open
            //         Firebase.Analytics.FirebaseAnalytics.LogEvent("app_open");
            //     }
            //     else
            //     {
            //         Debug.LogError($"[Firebase] Failed: {task.Result}");
            //     }
            // });

            // Stub
            firebaseReady = true;
        }
    }
}
