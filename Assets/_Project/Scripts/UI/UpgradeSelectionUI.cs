using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LastLineDefense.Data;
using LastLineDefense.Game;

namespace LastLineDefense.UI
{
    public class UpgradeSelectionUI : MonoBehaviour
    {
        public static UpgradeSelectionUI Instance { get; private set; }

        [Header("Choice Buttons")]
        [SerializeField] private Button[] choiceButtons;
        [SerializeField] private TMP_Text[] choiceTexts;

        private UpgradeData[] currentChoices;
        private WaveUpgradeManager upgradeManager;
        private System.Action onSelectionComplete;

        private void Awake()
        {
            Instance = this;

            if (choiceButtons == null || choiceButtons.Length == 0)
                choiceButtons = GetComponentsInChildren<Button>(true);

            upgradeManager = FindAnyObjectByType<WaveUpgradeManager>();

            gameObject.SetActive(false);
        }

        public void ShowChoices(System.Action onComplete)
        {
            onSelectionComplete = onComplete;

            if (upgradeManager == null)
                upgradeManager = FindAnyObjectByType<WaveUpgradeManager>();

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

            gameObject.SetActive(true);

            // Cache texts from buttons
            if (choiceTexts == null || choiceTexts.Length == 0)
            {
                choiceTexts = new TMP_Text[choiceButtons.Length];
                for (int i = 0; i < choiceButtons.Length; i++)
                    choiceTexts[i] = choiceButtons[i].GetComponentInChildren<TMP_Text>();
            }

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
            gameObject.SetActive(false);

            onSelectionComplete?.Invoke();
        }
    }
}
