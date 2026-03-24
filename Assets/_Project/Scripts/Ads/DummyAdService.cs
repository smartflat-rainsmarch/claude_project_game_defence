using System;
using System.Collections;
using UnityEngine;

namespace LastLineDefense.Ads
{
    public class DummyAdService : MonoBehaviour, IAdService
    {
        [SerializeField] private float simulatedDelay = 0.5f;

        public void Initialize()
        {
            Debug.Log("[DummyAdService] Initialized (test mode)");
        }

        public bool CanShowRewarded()
        {
            return true;
        }

        public void ShowRewarded(Action onReward)
        {
            Debug.Log("[DummyAdService] Showing rewarded ad (simulated)");
            StartCoroutine(SimulateAd(onReward));
        }

        public bool CanShowInterstitial()
        {
            return true;
        }

        public void ShowInterstitial()
        {
            Debug.Log("[DummyAdService] Showing interstitial ad (simulated)");
        }

        private IEnumerator SimulateAd(Action onComplete)
        {
            yield return new WaitForSeconds(simulatedDelay);
            Debug.Log("[DummyAdService] Rewarded ad completed");
            onComplete?.Invoke();
        }
    }
}
