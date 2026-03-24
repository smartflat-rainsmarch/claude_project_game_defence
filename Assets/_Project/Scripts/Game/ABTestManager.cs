using UnityEngine;

namespace LastLineDefense.Game
{
    public class ABTestManager : MonoBehaviour
    {
        public static ABTestManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public string GetVariant(string experimentKey, string defaultVariant = "control")
        {
            var config = RemoteConfigManager.Instance;
            if (config == null) return defaultVariant;

            string value = config.GetInt(experimentKey, 0).ToString();
            if (string.IsNullOrEmpty(value)) return defaultVariant;

            return value;
        }

        public int GetInterstitialInterval()
        {
            var config = RemoteConfigManager.Instance;
            return config != null ? config.GetInt("ad_interstitial_interval_stages", 3) : 3;
        }

        public int GetReviveHpPercent()
        {
            var config = RemoteConfigManager.Instance;
            return config != null ? config.GetInt("ad_rewarded_revive_hp_percent", 50) : 50;
        }

        public float GetEnemyHpMultiplier()
        {
            var config = RemoteConfigManager.Instance;
            return config != null ? config.GetFloat("enemy_hp_multiplier", 1f) : 1f;
        }

        public float GetClearRewardMultiplier()
        {
            var config = RemoteConfigManager.Instance;
            return config != null ? config.GetFloat("clear_reward_multiplier", 1f) : 1f;
        }
    }
}
