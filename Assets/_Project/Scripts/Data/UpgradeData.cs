using UnityEngine;

namespace LastLineDefense.Data
{
    public enum UpgradeEffectType
    {
        BasicTowerDamage,
        SplashRange,
        SlowDuration,
        LaserDps,
        StartingGold,
        KillGold,
        BaseHpRecover,
        CriticalChance,
        AttackSpeed,
        EnemySlowAll
    }

    [CreateAssetMenu(fileName = "NewUpgradeData", menuName = "Defense/Upgrade Data")]
    public class UpgradeData : ScriptableObject
    {
        public string upgradeId;
        public string displayName;
        public string description;
        public UpgradeEffectType effectType;
        public float effectValue;
        public Sprite icon;
    }
}
