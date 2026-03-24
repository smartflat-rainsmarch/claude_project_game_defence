using UnityEngine;

namespace LastLineDefense.Data
{
    public enum PermanentUpgradeType
    {
        BaseHp,
        StartingGold,
        BasicTowerDamage,
        SplashTowerDamage,
        KillReward,
        CriticalChance,
        UpgradeChoices,
        ClearReward
    }

    [CreateAssetMenu(fileName = "NewPermanentUpgrade", menuName = "Defense/Permanent Upgrade")]
    public class PermanentUpgradeData : ScriptableObject
    {
        public string upgradeId;
        public string displayName;
        public PermanentUpgradeType upgradeType;
        public float effectPerLevel;
        public int maxLevel = 10;
        public int[] costPerLevel = { 50, 80, 120, 170, 230, 300, 400, 520, 670, 850 };
    }
}
