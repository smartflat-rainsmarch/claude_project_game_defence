using System;
using UnityEngine;

namespace LastLineDefense.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int maxHp = 30;

        private int currentHp;
        private bool isDead;

        public event Action OnDeath;
        public int CurrentHp => currentHp;
        public bool IsDead => isDead;

        private void Awake()
        {
            currentHp = maxHp;
            isDead = false;
        }

        public void Initialize(int hp)
        {
            maxHp = hp;
            currentHp = hp;
            isDead = false;
        }

        public void TakeDamage(int damage)
        {
            if (isDead || damage <= 0) return;

            currentHp = Mathf.Max(0, currentHp - damage);

            if (currentHp <= 0)
            {
                isDead = true;
                OnDeath?.Invoke();
            }
        }
    }
}
