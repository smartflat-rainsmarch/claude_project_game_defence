using System;
using UnityEngine;

namespace LastLineDefense.Ads
{
    /// <summary>
    /// Real ad service for LevelPlay (ironSource) + AdMob.
    /// Replace DummyAdService with this when SDK is installed.
    ///
    /// Setup:
    /// 1. Install LevelPlay SDK via Unity Package Manager
    /// 2. Install Google Mobile Ads SDK
    /// 3. Replace DummyAdService on Managers GO with this
    /// 4. Set appKey in Inspector
    /// </summary>
    public class LevelPlayAdService : MonoBehaviour, IAdService
    {
        [Header("LevelPlay Settings")]
        [SerializeField] private string appKey = "YOUR_APP_KEY";
        [SerializeField] private bool testMode = true;

        private bool rewardedReady;
        private bool interstitialReady;
        private Action pendingRewardCallback;

        public void Initialize()
        {
            Debug.Log($"[LevelPlayAds] Initialize (testMode={testMode})");

            // Uncomment when LevelPlay SDK is installed:
            // IronSource.Agent.init(appKey);
            // IronSource.Agent.validateIntegration();
            //
            // IronSourceRewardedVideoEvents.onAdRewardedEvent += OnRewardedAdCompleted;
            // IronSourceRewardedVideoEvents.onAdAvailableEvent += (info) => rewardedReady = true;
            // IronSourceRewardedVideoEvents.onAdUnavailableEvent += () => rewardedReady = false;
            //
            // IronSourceInterstitialEvents.onAdReadyEvent += (info) => interstitialReady = true;
            // IronSourceInterstitialEvents.onAdShowSucceededEvent += (info) => { };
            //
            // IronSource.Agent.loadInterstitial();

            // Stub: simulate ready state
            rewardedReady = true;
            interstitialReady = true;
        }

        public bool CanShowRewarded()
        {
            // return IronSource.Agent.isRewardedVideoAvailable();
            return rewardedReady;
        }

        public void ShowRewarded(Action onReward)
        {
            pendingRewardCallback = onReward;

            if (!CanShowRewarded())
            {
                Debug.LogWarning("[LevelPlayAds] Rewarded ad not ready");
                return;
            }

            Debug.Log("[LevelPlayAds] Showing rewarded ad");
            // IronSource.Agent.showRewardedVideo();

            // Stub: simulate immediate reward
            OnRewardedAdCompleted();
        }

        private void OnRewardedAdCompleted()
        {
            Debug.Log("[LevelPlayAds] Rewarded ad completed");
            pendingRewardCallback?.Invoke();
            pendingRewardCallback = null;

            Analytics.AnalyticsEvents.LogAdRewardComplete("rewarded");
        }

        public bool CanShowInterstitial()
        {
            // return IronSource.Agent.isInterstitialReady();
            return interstitialReady;
        }

        public void ShowInterstitial()
        {
            if (!CanShowInterstitial())
            {
                Debug.LogWarning("[LevelPlayAds] Interstitial not ready");
                return;
            }

            Debug.Log("[LevelPlayAds] Showing interstitial");
            // IronSource.Agent.showInterstitial();

            Analytics.AnalyticsEvents.LogAdInterstitialShow();

            // Reload next interstitial
            // IronSource.Agent.loadInterstitial();
        }

        private void OnApplicationPause(bool isPaused)
        {
            // IronSource.Agent.onApplicationPause(isPaused);
        }
    }
}
