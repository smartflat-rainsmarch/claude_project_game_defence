using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LastLineDefense.Game;

namespace LastLineDefense.UI
{
    public class DailyRewardUI : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject rewardPanel;
        [SerializeField] private Button claimButton;
        [SerializeField] private Button claimWithAdButton;
        [SerializeField] private TMP_Text rewardAmountText;
        [SerializeField] private TMP_Text statusText;

        private DailyRewardManager rewardManager;

        private void Start()
        {
            rewardManager = FindFirstObjectByType<DailyRewardManager>();
            RefreshUI();

            if (claimButton != null)
                claimButton.onClick.AddListener(OnClaim);

            if (claimWithAdButton != null)
                claimWithAdButton.onClick.AddListener(OnClaimWithAd);
        }

        public void RefreshUI()
        {
            if (rewardManager == null) return;

            bool canClaim = rewardManager.CanClaimDaily();
            int amount = rewardManager.GetRewardAmount();

            if (claimButton != null)
                claimButton.interactable = canClaim;

            if (claimWithAdButton != null)
                claimWithAdButton.interactable = canClaim;

            if (rewardAmountText != null)
                rewardAmountText.text = $"+{amount}";

            if (statusText != null)
                statusText.text = canClaim ? "Claim your daily reward!" : "Come back tomorrow!";
        }

        private void OnClaim()
        {
            if (rewardManager == null) return;
            int reward = rewardManager.ClaimDailyReward();
            if (reward > 0)
            {
                Debug.Log($"[DailyRewardUI] Claimed {reward} coins");
                RefreshUI();
            }
        }

        private void OnClaimWithAd()
        {
            if (rewardManager == null) return;

            var adServiceObj = FindFirstObjectByType<MonoBehaviour>();
            Ads.IAdService adService = null;
            foreach (var mb in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None))
            {
                if (mb is Ads.IAdService svc) { adService = svc; break; }
            }
            if (adService != null && adService.CanShowRewarded())
            {
                adService.ShowRewarded(() =>
                {
                    int reward = rewardManager.ClaimDailyReward();
                    if (reward > 0)
                    {
                        var saveManager = FindFirstObjectByType<Save.SaveManager>();
                        if (saveManager != null)
                            saveManager.AddCoins(reward);
                        Debug.Log($"[DailyRewardUI] Claimed {reward * 2} coins (with ad bonus)");
                    }
                    RefreshUI();
                });
            }
            else
            {
                OnClaim();
            }
        }
    }
}
