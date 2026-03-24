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
