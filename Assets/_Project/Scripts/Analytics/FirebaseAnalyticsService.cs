using UnityEngine;

namespace LastLineDefense.Analytics
{
    public class FirebaseAnalyticsService : MonoBehaviour, IAnalyticsService
    {
        public void Initialize()
        {
            Debug.Log("[FirebaseAnalytics] Initialized (stub — replace with real Firebase SDK)");
        }

        public void LogEvent(string eventName)
        {
            Debug.Log($"[FirebaseAnalytics] Event: {eventName}");
            // Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName);
        }

        public void LogEvent(string eventName, string paramKey, string paramValue)
        {
            Debug.Log($"[FirebaseAnalytics] Event: {eventName}, {paramKey}={paramValue}");
            // Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName, new Parameter(paramKey, paramValue));
        }

        public void LogEvent(string eventName, string paramKey, int paramValue)
        {
            Debug.Log($"[FirebaseAnalytics] Event: {eventName}, {paramKey}={paramValue}");
            // Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName, new Parameter(paramKey, paramValue));
        }

        public void LogStageStart(int stageIndex)
        {
            Debug.Log($"[FirebaseAnalytics] stage_start: stage={stageIndex}");
        }

        public void LogStageClear(int stageIndex, int playTimeSec, int reward)
        {
            Debug.Log($"[FirebaseAnalytics] stage_clear: stage={stageIndex}, time={playTimeSec}, reward={reward}");
        }

        public void LogStageFail(int stageIndex, int waveIndex)
        {
            Debug.Log($"[FirebaseAnalytics] stage_fail: stage={stageIndex}, wave={waveIndex}");
        }

        public void LogAdRewardComplete(string placement)
        {
            Debug.Log($"[FirebaseAnalytics] ad_reward_complete: placement={placement}");
        }

        public void LogAdInterstitialShown()
        {
            Debug.Log("[FirebaseAnalytics] ad_interstitial_show");
        }

        public void LogTowerPlaced(string towerId, int slotIndex)
        {
            Debug.Log($"[FirebaseAnalytics] tower_placed: tower={towerId}, slot={slotIndex}");
        }

        public void LogUpgradeSelected(string upgradeId, int waveIndex)
        {
            Debug.Log($"[FirebaseAnalytics] upgrade_selected: id={upgradeId}, wave={waveIndex}");
        }

        public void LogPermanentUpgrade(string upgradeId, int newLevel)
        {
            Debug.Log($"[FirebaseAnalytics] permanent_upgrade: id={upgradeId}, level={newLevel}");
        }

        public void LogIAPPurchase(string productId)
        {
            Debug.Log($"[FirebaseAnalytics] iap_success: product={productId}");
        }
    }
}
