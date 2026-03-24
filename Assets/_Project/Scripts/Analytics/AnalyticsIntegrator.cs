using UnityEngine;
using LastLineDefense.Core;

namespace LastLineDefense.Analytics
{
    public class AnalyticsIntegrator : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour analyticsServiceBehaviour;

        private IAnalyticsService analyticsService;
        private float sessionStartTime;

        private void Awake()
        {
            if (analyticsServiceBehaviour != null)
                analyticsService = analyticsServiceBehaviour as IAnalyticsService;

            sessionStartTime = Time.realtimeSinceStartup;
        }

        private void OnEnable()
        {
            GameEvents.OnStageCleared += HandleStageCleared;
            GameEvents.OnStageFailed += HandleStageFailed;
            GameEvents.OnEnemyKilled += HandleEnemyKilled;
        }

        private void OnDisable()
        {
            GameEvents.OnStageCleared -= HandleStageCleared;
            GameEvents.OnStageFailed -= HandleStageFailed;
            GameEvents.OnEnemyKilled -= HandleEnemyKilled;
        }

        public void LogStageStart(int stageIndex)
        {
            sessionStartTime = Time.realtimeSinceStartup;
            analyticsService?.LogStageStart(stageIndex);
        }

        private void HandleStageCleared()
        {
            int playTime = Mathf.RoundToInt(Time.realtimeSinceStartup - sessionStartTime);
            int stageIndex = GameManager.Instance != null ? GameManager.Instance.SelectedStageIndex : 0;
            analyticsService?.LogStageClear(stageIndex, playTime, 0);
        }

        private void HandleStageFailed()
        {
            int stageIndex = GameManager.Instance != null ? GameManager.Instance.SelectedStageIndex : 0;
            analyticsService?.LogStageFail(stageIndex, 0);
        }

        private void HandleEnemyKilled()
        {
            // Batch enemy kill events to avoid spam
        }

        public void LogAdReward(string placement)
        {
            analyticsService?.LogAdRewardComplete(placement);
        }

        public void LogTowerPlaced(string towerId, int slot)
        {
            analyticsService?.LogTowerPlaced(towerId, slot);
        }
    }
}
