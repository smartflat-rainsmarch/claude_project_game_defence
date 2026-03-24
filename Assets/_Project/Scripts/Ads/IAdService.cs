using System;

namespace LastLineDefense.Ads
{
    public interface IAdService
    {
        void Initialize();
        bool CanShowRewarded();
        void ShowRewarded(Action onReward);
        bool CanShowInterstitial();
        void ShowInterstitial();
    }
}
