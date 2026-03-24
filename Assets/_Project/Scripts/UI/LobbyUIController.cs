using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LastLineDefense.Core;
using LastLineDefense.Save;

namespace LastLineDefense.UI
{
    public class LobbyUIController : MonoBehaviour
    {
        [Header("Display")]
        [SerializeField] private TMP_Text currencyText;
        [SerializeField] private TMP_Text highestStageText;
        [SerializeField] private TMP_Text selectedStageText;

        [Header("Buttons")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button prevButton;
        [SerializeField] private Button nextButton;

        private int selectedStage;
        private int maxUnlockedStage;

        private void Start()
        {
            var saveManager = FindAnyObjectByType<SaveManager>();
            if (saveManager != null)
            {
                var data = saveManager.GetSaveData();
                maxUnlockedStage = data.highestClearedStage + 1;
                selectedStage = Mathf.Min(maxUnlockedStage, 10);

                if (currencyText != null)
                    currencyText.text = $"Coins: {data.totalCoins}";

                if (highestStageText != null)
                    highestStageText.text = $"Best: Stage {data.highestClearedStage}";
            }
            else
            {
                maxUnlockedStage = 1;
                selectedStage = 1;
            }

            UpdateSelectedStageDisplay();

            if (startButton != null) startButton.onClick.AddListener(OnStartClicked);
            if (prevButton != null) prevButton.onClick.AddListener(OnPrevClicked);
            if (nextButton != null) nextButton.onClick.AddListener(OnNextClicked);
        }

        private void OnStartClicked()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.LoadStage(selectedStage - 1);
        }

        private void OnPrevClicked()
        {
            selectedStage = Mathf.Max(1, selectedStage - 1);
            UpdateSelectedStageDisplay();
        }

        private void OnNextClicked()
        {
            selectedStage = Mathf.Min(maxUnlockedStage, selectedStage + 1);
            UpdateSelectedStageDisplay();
        }

        private void UpdateSelectedStageDisplay()
        {
            if (selectedStageText != null)
                selectedStageText.text = $"Stage {selectedStage}";
        }
    }
}
