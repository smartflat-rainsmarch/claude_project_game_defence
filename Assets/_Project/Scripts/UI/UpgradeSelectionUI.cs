using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LastLineDefense.Data;
using LastLineDefense.Game;

namespace LastLineDefense.UI
{
    public class UpgradeSelectionUI : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] private GameObject selectionPanel;

        [Header("Choice Buttons")]
        [SerializeField] private Button[] choiceButtons;
        [SerializeField] private TMP_Text[] choiceTexts;

        private UpgradeData[] currentChoices;
        private WaveUpgradeManager upgradeManager;
        private System.Action onSelectionComplete;

        private void Awake()
        {
            upgradeManager = FindFirstObjectByType<WaveUpgradeManager>();

            if (selectionPanel != null)
                selectionPanel.SetActive(false);
        }

        public void ShowChoices(System.Action onComplete)
        {
            onSelectionComplete = onComplete;

            if (upgradeManager == null)
            {
                onSelectionComplete?.Invoke();
                return;
            }

            currentChoices = upgradeManager.GetRandomChoices();

            if (currentChoices.Length == 0)
            {
                onSelectionComplete?.Invoke();
                return;
            }

            if (selectionPanel != null)
                selectionPanel.SetActive(true);

            for (int i = 0; i < choiceButtons.Length; i++)
            {
                if (i < currentChoices.Length)
                {
                    choiceButtons[i].gameObject.SetActive(true);
                    if (i < choiceTexts.Length && choiceTexts[i] != null)
                        choiceTexts[i].text = $"{currentChoices[i].displayName}\n{currentChoices[i].description}";

                    int index = i;
                    choiceButtons[i].onClick.RemoveAllListeners();
                    choiceButtons[i].onClick.AddListener(() => SelectUpgrade(index));
                }
                else
                {
                    choiceButtons[i].gameObject.SetActive(false);
                }
            }

            Time.timeScale = 0f;
        }

        private void SelectUpgrade(int index)
        {
            if (index < 0 || index >= currentChoices.Length) return;

            upgradeManager.ApplyUpgrade(currentChoices[index]);

            Time.timeScale = 1f;

            if (selectionPanel != null)
                selectionPanel.SetActive(false);

            onSelectionComplete?.Invoke();
        }
    }
}
