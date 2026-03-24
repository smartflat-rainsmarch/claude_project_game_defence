using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LastLineDefense.Tower;
using LastLineDefense.Game;

namespace LastLineDefense.UI
{
    [System.Serializable]
    public class TowerOption
    {
        public string displayName;
        public int buildCost;
        public GameObject prefab;
    }

    public class TowerSelectionUI : MonoBehaviour
    {
        [Header("Tower Options")]
        [SerializeField] private TowerOption[] towerOptions;

        [Header("Buttons (auto-found if empty)")]
        [SerializeField] private Button[] towerButtons;

        [Header("Cancel")]
        [SerializeField] private Button cancelButton;

        private TowerSlot selectedSlot;

        private void Awake()
        {
            if (towerButtons == null || towerButtons.Length == 0)
                towerButtons = GetComponentsInChildren<Button>(true);

            if (cancelButton != null)
                cancelButton.onClick.AddListener(Cancel);

            gameObject.SetActive(false);
        }

        private void Start()
        {
            if (towerOptions == null || towerOptions.Length == 0)
                AutoDetectTowerPrefabs();
        }

        private void AutoDetectTowerPrefabs()
        {
            var basic = Resources.Load<GameObject>("Prefabs/Tower_Basic");
            var splash = Resources.Load<GameObject>("Prefabs/Tower_Splash");
            var slow = Resources.Load<GameObject>("Prefabs/Tower_Slow");
            var laser = Resources.Load<GameObject>("Prefabs/Tower_Laser");

            towerOptions = new TowerOption[]
            {
                new TowerOption { displayName = "Basic", buildCost = 50, prefab = basic },
                new TowerOption { displayName = "Splash", buildCost = 80, prefab = splash },
                new TowerOption { displayName = "Slow", buildCost = 60, prefab = slow },
                new TowerOption { displayName = "Laser", buildCost = 120, prefab = laser }
            };
        }

        public void OnSlotSelected(TowerSlot slot)
        {
            selectedSlot = slot;
            gameObject.SetActive(true);
            RefreshButtons();
        }

        private void RefreshButtons()
        {
            var currency = FindAnyObjectByType<CurrencyManager>();

            for (int i = 0; i < towerButtons.Length; i++)
            {
                if (towerOptions != null && i < towerOptions.Length && towerOptions[i] != null)
                {
                    towerButtons[i].gameObject.SetActive(true);

                    var text = towerButtons[i].GetComponentInChildren<TMP_Text>();
                    if (text != null)
                        text.text = $"{towerOptions[i].displayName}\n{towerOptions[i].buildCost}G";

                    bool canAfford = currency != null && currency.CanSpend(towerOptions[i].buildCost);
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
            if (selectedSlot == null || towerOptions == null || index >= towerOptions.Length) return;

            var option = towerOptions[index];
            if (option.prefab != null)
            {
                selectedSlot.SetTowerPrefab(option.prefab, option.buildCost);
                selectedSlot.TryBuild();
            }

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
