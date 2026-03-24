using UnityEngine;
using LastLineDefense.Game;
using LastLineDefense.UI;

namespace LastLineDefense.Tower
{
    public class TowerSlot : MonoBehaviour
    {
        [SerializeField] private GameObject towerPrefab;
        [SerializeField] private int buildCost = 50;
        [SerializeField] private SpriteRenderer slotIndicator;

        private GameObject builtTower;
        private bool isOccupied;

        public bool IsOccupied => isOccupied;

        private void OnMouseDown()
        {
            if (isOccupied) return;

            var towerUI = TowerSelectionUI.Instance;
            if (towerUI != null)
            {
                towerUI.OnSlotSelected(this);
            }
            else
            {
                TryBuild();
            }
        }

        public void TryBuild()
        {
            if (isOccupied || towerPrefab == null) return;

            var currency = FindAnyObjectByType<CurrencyManager>();
            if (currency == null || !currency.CanSpend(buildCost)) return;

            currency.SpendGold(buildCost);
            builtTower = Instantiate(towerPrefab, transform.position, Quaternion.identity, transform);
            isOccupied = true;

            if (slotIndicator != null)
                slotIndicator.enabled = false;
        }

        public void SetTowerPrefab(GameObject prefab, int cost)
        {
            towerPrefab = prefab;
            buildCost = cost;
        }
    }
}
