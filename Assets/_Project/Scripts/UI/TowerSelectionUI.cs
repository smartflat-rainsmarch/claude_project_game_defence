using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LastLineDefense.Data;
using LastLineDefense.Tower;

namespace LastLineDefense.UI
{
    public class TowerSelectionUI : MonoBehaviour
    {
        [Header("Tower Buttons")]
        [SerializeField] private Button[] towerButtons;
        [SerializeField] private TMP_Text[] towerCostTexts;
        [SerializeField] private TowerData[] towerDataList;

        private TowerSlot selectedSlot;

        public void OnSlotSelected(TowerSlot slot)
        {
            selectedSlot = slot;
            gameObject.SetActive(true);
            RefreshButtons();
        }

        private void RefreshButtons()
        {
            var currency = FindAnyObjectByType<Game.CurrencyManager>();

            for (int i = 0; i < towerButtons.Length; i++)
            {
                if (i < towerDataList.Length && towerDataList[i] != null)
                {
                    towerButtons[i].gameObject.SetActive(true);

                    if (i < towerCostTexts.Length && towerCostTexts[i] != null)
                        towerCostTexts[i].text = $"{towerDataList[i].displayName}\n{towerDataList[i].buildCost}G";

                    bool canAfford = currency != null && currency.CanSpend(towerDataList[i].buildCost);
                    towerButtons[i].interactable = canAfford;

                    int index = i;
                    towerButtons[i].onClick.RemoveAllListeners();
                    towerButtons[i].onClick.AddListener(() => SelectTower(index));
                }
                else
                {
                    towerButtons[i].gameObject.SetActive(false);
                }
            }
        }

        private void SelectTower(int index)
        {
            if (selectedSlot == null || index >= towerDataList.Length) return;

            var data = towerDataList[index];
            selectedSlot.SetTowerPrefab(data.prefab, data.buildCost);
            selectedSlot.TryBuild();

            gameObject.SetActive(false);
            selectedSlot = null;
        }

        public void Cancel()
        {
            gameObject.SetActive(false);
            selectedSlot = null;
        }
    }
}
