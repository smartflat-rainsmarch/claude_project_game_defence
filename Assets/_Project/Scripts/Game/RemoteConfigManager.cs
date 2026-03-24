using System.Collections.Generic;
using UnityEngine;

namespace LastLineDefense.Game
{
    public class RemoteConfigManager : MonoBehaviour
    {
        public static RemoteConfigManager Instance { get; private set; }

        private readonly Dictionary<string, string> defaults = new Dictionary<string, string>
        {
            { "ad_interstitial_interval_stages", "3" },
            { "ad_interstitial_min_interval_sec", "120" },
            { "ad_interstitial_grace_stages", "3" },
            { "ad_interstitial_grace_minutes", "10" },
            { "ad_interstitial_max_per_session", "5" },
            { "ad_rewarded_revive_enabled", "true" },
            { "ad_rewarded_revive_hp_percent", "50" },
            { "ad_rewarded_double_enabled", "true" },
            { "enemy_hp_multiplier", "1.0" },
            { "enemy_speed_multiplier", "1.0" },
            { "tower_damage_multiplier", "1.0" },
            { "starting_gold_bonus", "0" },
            { "clear_reward_multiplier", "1.0" },
            { "daily_reward_coins_base", "50" },
            { "new_user_buff_enabled", "true" },
            { "new_user_buff_stages", "5" }
        };

        private Dictionary<string, string> currentValues;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            currentValues = new Dictionary<string, string>(defaults);
        }

        public int GetInt(string key, int fallback = 0)
        {
            if (currentValues.TryGetValue(key, out string val) && int.TryParse(val, out int result))
                return result;
            return fallback;
        }

        public float GetFloat(string key, float fallback = 0f)
        {
            if (currentValues.TryGetValue(key, out string val) && float.TryParse(val, out float result))
                return result;
            return fallback;
        }

        public bool GetBool(string key, bool fallback = false)
        {
            if (currentValues.TryGetValue(key, out string val) && bool.TryParse(val, out bool result))
                return result;
            return fallback;
        }

        public void SetValue(string key, string value)
        {
            currentValues[key] = value;
        }
    }
}
