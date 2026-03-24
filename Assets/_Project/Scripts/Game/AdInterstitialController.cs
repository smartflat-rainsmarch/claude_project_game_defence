using UnityEngine;
using LastLineDefense.Ads;

namespace LastLineDefense.Game
{
    public class AdInterstitialController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int stagesBetweenAds = 3;
        [SerializeField] private int graceStages = 3;
        [SerializeField] private float minIntervalSeconds = 120f;

        [Header("References")]
        [SerializeField] private MonoBehaviour adServiceBehaviour;

        private IAdService adService;
        private int stagesSinceLastAd;
        private float lastAdTime;

        private void Awake()
        {
            if (adServiceBehaviour != null)
                adService = adServiceBehaviour as IAdService;

            lastAdTime = -minIntervalSeconds;
        }

        public void OnStageCompleted(int stageIndex)
        {
            stagesSinceLastAd++;

            if (stageIndex < graceStages) return;

            if (stagesSinceLastAd < stagesBetweenAds) return;

            if (Time.realtimeSinceStartup - lastAdTime < minIntervalSeconds) return;

            ShowInterstitial();
        }

        private void ShowInterstitial()
        {
            if (adService == null || !adService.CanShowInterstitial()) return;

            adService.ShowInterstitial();
            stagesSinceLastAd = 0;
            lastAdTime = Time.realtimeSinceStartup;

            Analytics.AnalyticsEvents.LogAdInterstitialShow();
            Debug.Log("[AdInterstitial] Interstitial shown");
        }
    }
}
