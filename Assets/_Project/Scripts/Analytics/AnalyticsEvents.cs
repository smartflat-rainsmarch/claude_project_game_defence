using UnityEngine;

namespace LastLineDefense.Analytics
{
    public static class AnalyticsEvents
    {
        public const string StageStart = "stage_start";
        public const string StageClear = "stage_clear";
        public const string StageFail = "stage_fail";
        public const string AdRewardShow = "ad_reward_show";
        public const string AdRewardComplete = "ad_reward_complete";
        public const string AdInterstitialShow = "ad_interstitial_show";
        public const string TutorialStart = "tutorial_start";
        public const string TutorialComplete = "tutorial_complete";
        public const string UpgradeSelected = "upgrade_selected";
        public const string TowerPlaced = "tower_placed";
        public const string PermanentUpgrade = "permanent_upgrade";

        public static void LogStageStart(int stage)
        {
            Debug.Log($"[Analytics] {StageStart}: stage={stage}");
        }

        public static void LogStageClear(int stage, int time, int reward)
        {
            Debug.Log($"[Analytics] {StageClear}: stage={stage}, time={time}, reward={reward}");
        }

        public static void LogStageFail(int stage, int wave)
        {
            Debug.Log($"[Analytics] {StageFail}: stage={stage}, wave={wave}");
        }

        public static void LogAdRewardComplete(string placement)
        {
            Debug.Log($"[Analytics] {AdRewardComplete}: placement={placement}");
        }

        public static void LogAdInterstitialShow()
        {
            Debug.Log($"[Analytics] {AdInterstitialShow}");
        }

        public static void LogTowerPlaced(string towerId, int slotIndex)
        {
            Debug.Log($"[Analytics] {TowerPlaced}: tower={towerId}, slot={slotIndex}");
        }
    }
}
