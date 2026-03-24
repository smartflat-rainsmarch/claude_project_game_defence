using UnityEngine;

namespace LastLineDefense.Game
{
    public class RemoteConfigLoader : MonoBehaviour
    {
        private void Start()
        {
            FetchRemoteConfig();
        }

        private void FetchRemoteConfig()
        {
            Debug.Log("[RemoteConfig] Fetching remote config (stub — replace with Firebase Remote Config SDK)");

            // When Firebase is integrated:
            // var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            // remoteConfig.FetchAsync(System.TimeSpan.FromSeconds(fetchTimeoutSeconds))
            //     .ContinueWithOnMainThread(task => {
            //         remoteConfig.ActivateAsync().ContinueWithOnMainThread(activateTask => {
            //             ApplyConfig(remoteConfig);
            //         });
            //     });

            ApplyDefaultConfig();
        }

        private void ApplyDefaultConfig()
        {
            var config = RemoteConfigManager.Instance;
            if (config == null) return;

            Debug.Log("[RemoteConfig] Applied default config values");
        }
    }
}
