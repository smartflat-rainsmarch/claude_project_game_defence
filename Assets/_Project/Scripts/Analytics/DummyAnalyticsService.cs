using UnityEngine;

namespace LastLineDefense.Analytics
{
    public class DummyAnalyticsService : MonoBehaviour, IAnalyticsService
    {
        public void Initialize() { }
        public void LogEvent(string eventName) { }
        public void LogEvent(string eventName, string paramKey, string paramValue) { }
        public void LogEvent(string eventName, string paramKey, int paramValue) { }
        public void LogStageStart(int stageIndex) { }
        public void LogStageClear(int stageIndex, int playTimeSec, int reward) { }
        public void LogStageFail(int stageIndex, int waveIndex) { }
        public void LogAdRewardComplete(string placement) { }
        public void LogAdInterstitialShown() { }
        public void LogTowerPlaced(string towerId, int slotIndex) { }
        public void LogUpgradeSelected(string upgradeId, int waveIndex) { }
        public void LogPermanentUpgrade(string upgradeId, int newLevel) { }
        public void LogIAPPurchase(string productId) { }
    }
}
