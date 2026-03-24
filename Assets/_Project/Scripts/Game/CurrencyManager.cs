using UnityEngine;
using LastLineDefense.Core;

namespace LastLineDefense.Game
{
    public class CurrencyManager : MonoBehaviour
    {
        [SerializeField] private int startingGold = 120;

        private int currentGold;

        public int CurrentGold => currentGold;

        public void InitializeCurrency(int amount)
        {
            currentGold = amount;
            GameEvents.GoldChanged(currentGold);
        }

        public void InitializeWithDefault()
        {
            InitializeCurrency(startingGold);
        }

        public void AddGold(int amount)
        {
            if (amount <= 0) return;
            currentGold += amount;
            GameEvents.GoldChanged(currentGold);
        }

        public bool CanSpend(int amount)
        {
            return currentGold >= amount;
        }

        public bool SpendGold(int amount)
        {
            if (!CanSpend(amount)) return false;
            currentGold -= amount;
            GameEvents.GoldChanged(currentGold);
            return true;
        }
    }
}
