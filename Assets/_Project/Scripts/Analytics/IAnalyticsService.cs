namespace LastLineDefense.Analytics
{
    public interface IAnalyticsService
    {
        void Initialize();
        void LogEvent(string eventName);
        void LogEvent(string eventName, string paramKey, string paramValue);
        void LogEvent(string eventName, string paramKey, int paramValue);
        void LogStageStart(int stageIndex);
        void LogStageClear(int stageIndex, int playTimeSec, int reward);
        void LogStageFail(int stageIndex, int waveIndex);
        void LogAdRewardComplete(string placement);
        void LogAdInterstitialShown();
        void LogTowerPlaced(string towerId, int slotIndex);
        void LogUpgradeSelected(string upgradeId, int waveIndex);
        void LogPermanentUpgrade(string upgradeId, int newLevel);
        void LogIAPPurchase(string productId);
    }
}
