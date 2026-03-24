using UnityEngine;

namespace LastLineDefense.Analytics
{
    public class CrashlyticsHelper : MonoBehaviour
    {
        public static CrashlyticsHelper Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void SetCustomKey(string key, string value)
        {
            Debug.Log($"[Crashlytics] SetKey: {key}={value}");
            // Firebase.Crashlytics.Crashlytics.SetCustomKey(key, value);
        }

        public void SetCustomKey(string key, int value)
        {
            Debug.Log($"[Crashlytics] SetKey: {key}={value}");
            // Firebase.Crashlytics.Crashlytics.SetCustomKey(key, value);
        }

        public void UpdateStageContext(int stageIndex, int waveIndex, int activeEnemies, int activeTowers)
        {
            SetCustomKey("current_stage", stageIndex);
            SetCustomKey("current_wave", waveIndex);
            SetCustomKey("active_enemies", activeEnemies);
            SetCustomKey("active_towers", activeTowers);
        }

        public void LogException(System.Exception exception)
        {
            Debug.LogError($"[Crashlytics] Exception: {exception.Message}");
            // Firebase.Crashlytics.Crashlytics.LogException(exception);
        }

        public void Log(string message)
        {
            Debug.Log($"[Crashlytics] Log: {message}");
            // Firebase.Crashlytics.Crashlytics.Log(message);
        }
    }
}
