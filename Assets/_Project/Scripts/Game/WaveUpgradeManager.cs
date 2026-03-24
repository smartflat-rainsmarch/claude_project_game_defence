using System.Collections.Generic;
using UnityEngine;
using LastLineDefense.Data;

namespace LastLineDefense.Game
{
    public class WaveUpgradeManager : MonoBehaviour
    {
        [Header("Upgrade Pool")]
        [SerializeField] private UpgradeData[] allUpgrades;

        [Header("Settings")]
        [SerializeField] private int choicesPerWave = 3;

        private readonly List<UpgradeData> activeUpgrades = new List<UpgradeData>();

        public List<UpgradeData> ActiveUpgrades => activeUpgrades;

        private void Start()
        {
            if (allUpgrades == null || allUpgrades.Length == 0)
                CreateDefaultUpgrades();
        }

        private void CreateDefaultUpgrades()
        {
            allUpgrades = new UpgradeData[10];
            string[] names = { "ATK +15%", "Splash Range +20%", "Slow Duration +1s", "Laser DPS +20%",
                "Start Gold +30", "Kill Gold +10%", "Base HP +1", "Crit +5%", "ATK Speed +10%", "Enemy Slow -5%" };
            string[] descs = { "Basic tower damage up", "Splash area wider", "Slow lasts longer", "Laser hits harder",
                "More starting gold", "More gold per kill", "Recover 1 base HP", "Chance for 2x damage", "All towers attack faster", "All enemies move slower" };
            UpgradeEffectType[] types = {
                UpgradeEffectType.BasicTowerDamage, UpgradeEffectType.SplashRange, UpgradeEffectType.SlowDuration,
                UpgradeEffectType.LaserDps, UpgradeEffectType.StartingGold, UpgradeEffectType.KillGold,
                UpgradeEffectType.BaseHpRecover, UpgradeEffectType.CriticalChance, UpgradeEffectType.AttackSpeed,
                UpgradeEffectType.EnemySlowAll };
            float[] values = { 15f, 20f, 1f, 20f, 30f, 10f, 1f, 5f, 10f, 5f };

            for (int i = 0; i < 10; i++)
            {
                allUpgrades[i] = ScriptableObject.CreateInstance<UpgradeData>();
                allUpgrades[i].upgradeId = $"upgrade_{i}";
                allUpgrades[i].displayName = names[i];
                allUpgrades[i].description = descs[i];
                allUpgrades[i].effectType = types[i];
                allUpgrades[i].effectValue = values[i];
            }

            Debug.Log("[WaveUpgrade] Created 10 default upgrades");
        }

        public UpgradeData[] GetRandomChoices()
        {
            if (allUpgrades == null || allUpgrades.Length == 0)
                return new UpgradeData[0];

            var pool = new List<UpgradeData>(allUpgrades);
            var choices = new List<UpgradeData>();
            int count = Mathf.Min(choicesPerWave, pool.Count);

            for (int i = 0; i < count; i++)
            {
                int index = Random.Range(0, pool.Count);
                choices.Add(pool[index]);
                pool.RemoveAt(index);
            }

            return choices.ToArray();
        }

        public void ApplyUpgrade(UpgradeData upgrade)
        {
            if (upgrade == null) return;
            activeUpgrades.Add(upgrade);
            Debug.Log($"[WaveUpgrade] Applied: {upgrade.displayName} ({upgrade.effectType}: {upgrade.effectValue})");
        }

        public float GetTotalEffect(UpgradeEffectType type)
        {
            float total = 0f;
            foreach (var upgrade in activeUpgrades)
            {
                if (upgrade.effectType == type)
                    total += upgrade.effectValue;
            }
            return total;
        }

        public void ClearUpgrades()
        {
            activeUpgrades.Clear();
        }
    }
}
