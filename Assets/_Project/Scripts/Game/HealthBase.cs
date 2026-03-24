using UnityEngine;
using LastLineDefense.Core;

namespace LastLineDefense.Game
{
    public class HealthBase : MonoBehaviour
    {
        [SerializeField] private int maxHp = 20;

        private int currentHp;
        private bool isDead;

        public int CurrentHp => currentHp;
        public bool IsDead => isDead;

        public void InitializeHp(int hp)
        {
            maxHp = hp;
            currentHp = hp;
            isDead = false;
            GameEvents.BaseHpChanged(currentHp);
        }

        public void InitializeWithDefault()
        {
            InitializeHp(maxHp);
        }

        public void TakeDamage(int amount)
        {
            if (isDead || amount <= 0) return;

            currentHp = Mathf.Max(0, currentHp - amount);
            GameEvents.BaseHpChanged(currentHp);

            if (currentHp <= 0)
            {
                isDead = true;
                GameEvents.StageFailed();
            }
        }
    }
}
