using System;
using UnityEngine;
using LastLineDefense.Save;

namespace LastLineDefense.Game
{
    public class DailyRewardManager : MonoBehaviour
    {
        [SerializeField] private int baseRewardCoins = 50;
        [SerializeField] private int maxStreak = 7;
        [SerializeField] private float streakMultiplier = 0.2f;

        private SaveManager saveManager;

        private void Awake()
        {
            saveManager = FindFirstObjectByType<SaveManager>();
        }

        public bool CanClaimDaily()
        {
            if (saveManager == null) return false;

            var data = saveManager.GetSaveData();
            long lastClaim = data.lastDailyRewardTime;

            if (lastClaim == 0) return true;

            var lastDate = DateTimeOffset.FromUnixTimeSeconds(lastClaim).Date;
            var today = DateTimeOffset.UtcNow.Date;

            return today > lastDate;
        }

        public int GetRewardAmount()
        {
            return baseRewardCoins;
        }

        public int ClaimDailyReward()
        {
            if (!CanClaimDaily()) return 0;

            int reward = GetRewardAmount();

            var data = saveManager.GetSaveData();
            data.lastDailyRewardTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            data.totalCoins += reward;
            saveManager.Save();

            Debug.Log($"[DailyReward] Claimed: {reward} coins");
            return reward;
        }
    }
}
