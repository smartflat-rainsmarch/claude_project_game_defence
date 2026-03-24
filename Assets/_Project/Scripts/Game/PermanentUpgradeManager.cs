using UnityEngine;
using LastLineDefense.Data;
using LastLineDefense.Save;

namespace LastLineDefense.Game
{
    public class PermanentUpgradeManager : MonoBehaviour
    {
        [SerializeField] private PermanentUpgradeData[] allUpgrades;

        private SaveManager saveManager;

        private void Awake()
        {
            saveManager = FindAnyObjectByType<SaveManager>();
        }

        public int GetLevel(int upgradeIndex)
        {
            if (saveManager == null || upgradeIndex < 0) return 0;
            var data = saveManager.GetSaveData();
            if (data?.upgradeLevels == null || upgradeIndex >= data.upgradeLevels.Length) return 0;
            return data.upgradeLevels[upgradeIndex];
        }

        public bool CanUpgrade(int upgradeIndex)
        {
            if (allUpgrades == null || upgradeIndex < 0 || upgradeIndex >= allUpgrades.Length) return false;

            var upgrade = allUpgrades[upgradeIndex];
            int currentLevel = GetLevel(upgradeIndex);
            if (currentLevel >= upgrade.maxLevel) return false;

            int cost = GetCost(upgradeIndex);
            if (saveManager == null) return false;

            return saveManager.GetSaveData().totalCoins >= cost;
        }

        public int GetCost(int upgradeIndex)
        {
            if (allUpgrades == null || upgradeIndex >= allUpgrades.Length) return 0;

            var upgrade = allUpgrades[upgradeIndex];
            int level = GetLevel(upgradeIndex);
            if (level >= upgrade.costPerLevel.Length) return 99999;
            return upgrade.costPerLevel[level];
        }

        public bool TryUpgrade(int upgradeIndex)
        {
            if (!CanUpgrade(upgradeIndex)) return false;

            int cost = GetCost(upgradeIndex);
            var data = saveManager.GetSaveData();
            data.totalCoins -= cost;
            data.upgradeLevels[upgradeIndex]++;
            saveManager.Save();

            Debug.Log($"[PermanentUpgrade] {allUpgrades[upgradeIndex].displayName} upgraded to Lv.{data.upgradeLevels[upgradeIndex]}");
            return true;
        }

        public float GetTotalEffect(PermanentUpgradeType type)
        {
            float total = 0f;
            if (allUpgrades == null) return total;

            for (int i = 0; i < allUpgrades.Length; i++)
            {
                if (allUpgrades[i].upgradeType == type)
                    total += allUpgrades[i].effectPerLevel * GetLevel(i);
            }
            return total;
        }
    }
}
