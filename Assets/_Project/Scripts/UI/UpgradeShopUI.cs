using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LastLineDefense.Game;
using LastLineDefense.Save;

namespace LastLineDefense.UI
{
    public class UpgradeShopUI : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] private GameObject shopPanel;

        [Header("Upgrade Buttons")]
        [SerializeField] private Button[] upgradeButtons;
        [SerializeField] private TMP_Text[] upgradeLabelTexts;
        [SerializeField] private TMP_Text[] upgradeCostTexts;

        [Header("Currency Display")]
        [SerializeField] private TMP_Text coinText;

        [Header("Navigation")]
        [SerializeField] private Button closeButton;

        private PermanentUpgradeManager upgradeManager;
        private SaveManager saveManager;

        private void Awake()
        {
            upgradeManager = FindAnyObjectByType<PermanentUpgradeManager>();
            saveManager = FindAnyObjectByType<SaveManager>();
        }

        private void OnEnable()
        {
            RefreshUI();

            if (closeButton != null)
                closeButton.onClick.AddListener(Close);

            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                int index = i;
                upgradeButtons[i].onClick.RemoveAllListeners();
                upgradeButtons[i].onClick.AddListener(() => OnUpgradeClicked(index));
            }
        }

        public void Open()
        {
            if (shopPanel != null)
                shopPanel.SetActive(true);
            RefreshUI();
        }

        public void Close()
        {
            if (shopPanel != null)
                shopPanel.SetActive(false);
        }

        private void OnUpgradeClicked(int index)
        {
            if (upgradeManager == null) return;

            if (upgradeManager.TryUpgrade(index))
            {
                RefreshUI();
            }
        }

        private void RefreshUI()
        {
            if (saveManager != null && coinText != null)
                coinText.text = $"Coins: {saveManager.GetSaveData().totalCoins}";

            if (upgradeManager == null) return;

            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                bool canUpgrade = upgradeManager.CanUpgrade(i);
                upgradeButtons[i].interactable = canUpgrade;

                if (i < upgradeCostTexts.Length && upgradeCostTexts[i] != null)
                {
                    int cost = upgradeManager.GetCost(i);
                    int level = upgradeManager.GetLevel(i);
                    upgradeCostTexts[i].text = $"Lv.{level} → {cost}";
                }
            }
        }
    }
}
