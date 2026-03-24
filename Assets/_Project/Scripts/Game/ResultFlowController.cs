using UnityEngine;
using LastLineDefense.Core;
using LastLineDefense.Ads;
using LastLineDefense.Save;
using LastLineDefense.UI;

namespace LastLineDefense.Game
{
    public class ResultFlowController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ResultUIController resultUI;
        [SerializeField] private MonoBehaviour adServiceBehaviour;

        private IAdService adService;
        private int baseReward;
        private bool adRewardClaimed;
        private bool isCleared;

        private void Awake()
        {
            if (adServiceBehaviour != null)
                adService = adServiceBehaviour as IAdService;
        }

        private void OnEnable()
        {
            GameEvents.OnStageCleared += HandleCleared;
            GameEvents.OnStageFailed += HandleFailed;
        }

        private void OnDisable()
        {
            GameEvents.OnStageCleared -= HandleCleared;
            GameEvents.OnStageFailed -= HandleFailed;
        }

        private void HandleCleared()
        {
            isCleared = true;
            var stageManager = FindFirstObjectByType<StageManager>();
            baseReward = stageManager != null ? stageManager.ClearReward : 60;
            adRewardClaimed = false;

            if (resultUI != null)
                resultUI.ShowResult(true, baseReward);

            var saveManager = FindFirstObjectByType<SaveManager>();
            if (saveManager != null)
            {
                saveManager.AddCoins(baseReward);
                int stageIndex = GameManager.Instance != null ? GameManager.Instance.SelectedStageIndex : 0;
                saveManager.SetHighestStage(stageIndex + 1);
            }
        }

        private void HandleFailed()
        {
            isCleared = false;
            var stageManager = FindFirstObjectByType<StageManager>();
            int clearRewardBase = stageManager != null ? stageManager.ClearReward : 60;
            int failReward = Mathf.RoundToInt(clearRewardBase * 0.3f);
            baseReward = failReward;

            if (resultUI != null)
                resultUI.ShowResult(false, failReward);

            var saveManager = FindFirstObjectByType<SaveManager>();
            if (saveManager != null && failReward > 0)
                saveManager.AddCoins(failReward);
        }

        public void OnClickRewardAd()
        {
            if (adRewardClaimed || adService == null) return;

            if (adService.CanShowRewarded())
            {
                adService.ShowRewarded(() =>
                {
                    adRewardClaimed = true;
                    int bonusReward = baseReward;

                    var saveManager = FindFirstObjectByType<SaveManager>();
                    if (saveManager != null)
                        saveManager.AddCoins(bonusReward);

                    if (resultUI != null)
                    {
                        resultUI.UpdateRewardText(baseReward * 2);
                        resultUI.DisableAdButton();
                    }

                    Debug.Log($"[ResultFlow] Ad reward claimed: +{bonusReward}");
                });
            }
        }

        public void OnClickNextStage()
        {
            if (GameManager.Instance != null)
            {
                int next = GameManager.Instance.SelectedStageIndex + 1;
                GameManager.Instance.LoadStage(next);
            }
        }

        public void OnClickRetry()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.LoadStage();
        }

        public void OnClickLobby()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.LoadLobby();
        }
    }
}
