using UnityEngine;
using TMPro;
using LastLineDefense.Core;

namespace LastLineDefense.UI
{
    public class StageUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private TMP_Text waveText;
        [SerializeField] private TMP_Text baseHpText;

        private void OnEnable()
        {
            GameEvents.OnGoldChanged += UpdateGold;
            GameEvents.OnWaveChanged += UpdateWave;
            GameEvents.OnBaseHpChanged += UpdateBaseHp;
        }

        private void OnDisable()
        {
            GameEvents.OnGoldChanged -= UpdateGold;
            GameEvents.OnWaveChanged -= UpdateWave;
            GameEvents.OnBaseHpChanged -= UpdateBaseHp;
        }

        private void UpdateGold(int gold)
        {
            if (goldText != null)
                goldText.text = $"Gold: {gold}";
        }

        private void UpdateWave(int current, int total)
        {
            if (waveText != null)
                waveText.text = $"Wave: {current}/{total}";
        }

        private void UpdateBaseHp(int hp)
        {
            if (baseHpText != null)
                baseHpText.text = $"HP: {hp}";
        }
    }
}
