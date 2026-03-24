using UnityEngine;
using LastLineDefense.Core;
using LastLineDefense.Wave;

namespace LastLineDefense.Game
{
    public class StageManager : MonoBehaviour
    {
        [Header("Stage Settings")]
        [SerializeField] private int startingGold = 120;
        [SerializeField] private int startingBaseHp = 20;
        [SerializeField] private int totalWaves = 3;
        [SerializeField] private int clearReward = 60;

        [Header("References")]
        [SerializeField] private CurrencyManager currencyManager;
        [SerializeField] private HealthBase healthBase;

        private WaveManager waveManager;
        private bool stageEnded;

        public int ClearReward => clearReward;
        public bool StageEnded => stageEnded;

        private void Awake()
        {
            waveManager = GetComponentInChildren<WaveManager>();
            if (waveManager == null)
                waveManager = FindAnyObjectByType<WaveManager>();
        }

        private void OnEnable()
        {
            GameEvents.OnStageFailed += HandleStageFail;
            GameEvents.OnEnemyReachedBase += HandleEnemyReachedBase;
        }

        private void OnDisable()
        {
            GameEvents.OnStageFailed -= HandleStageFail;
            GameEvents.OnEnemyReachedBase -= HandleEnemyReachedBase;
        }

        private void Start()
        {
            InitializeStage();
        }

        public void InitializeStage()
        {
            stageEnded = false;

            if (currencyManager != null)
                currencyManager.InitializeCurrency(startingGold);

            if (healthBase != null)
                healthBase.InitializeHp(startingBaseHp);

            if (waveManager != null)
            {
                waveManager.InitializeWaves(totalWaves);
                waveManager.StartWaveFlow();
            }

            Debug.Log($"[StageManager] Stage initialized: Gold={startingGold}, HP={startingBaseHp}, Waves={totalWaves}");
        }

        public void HandleAllWavesCleared()
        {
            if (stageEnded) return;
            stageEnded = true;
            Debug.Log("[StageManager] Stage Cleared!");
            GameEvents.StageCleared();
        }

        private void HandleStageFail()
        {
            if (stageEnded) return;
            stageEnded = true;
            Debug.Log("[StageManager] Stage Failed!");
        }

        private void HandleEnemyReachedBase(int damage)
        {
            if (healthBase != null)
                healthBase.TakeDamage(damage);
        }
    }
}
