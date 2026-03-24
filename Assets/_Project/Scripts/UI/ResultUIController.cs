using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LastLineDefense.UI
{
    public class ResultUIController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject resultPanel;
        [SerializeField] private TMP_Text resultTitleText;
        [SerializeField] private TMP_Text rewardText;
        [SerializeField] private Button rewardAdButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button lobbyButton;

        private void Start()
        {
            if (resultPanel != null)
                resultPanel.SetActive(false);
        }

        public void ShowResult(bool isCleared, int reward)
        {
            if (resultPanel != null)
                resultPanel.SetActive(true);

            if (resultTitleText != null)
                resultTitleText.text = isCleared ? "STAGE CLEAR!" : "STAGE FAILED";

            if (rewardText != null)
                rewardText.text = $"Reward: {reward}";

            if (rewardAdButton != null)
                rewardAdButton.gameObject.SetActive(isCleared);

            if (nextButton != null)
                nextButton.gameObject.SetActive(isCleared);
        }

        public void UpdateRewardText(int newReward)
        {
            if (rewardText != null)
                rewardText.text = $"Reward: {newReward}";
        }

        public void DisableAdButton()
        {
            if (rewardAdButton != null)
                rewardAdButton.interactable = false;
        }
    }
}
