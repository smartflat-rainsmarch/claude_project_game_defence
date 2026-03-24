using System;

namespace LastLineDefense.Save
{
    [Serializable]
    public class SaveData
    {
        public int highestClearedStage;
        public int totalCoins;
        public int totalGems;
        public int[] upgradeLevels;
        public bool adRemovalPurchased;
        public bool tutorialCompleted;
        public long lastDailyRewardTime;
        public int totalPlayTimeSeconds;
        public int totalStagesPlayed;

        public static SaveData CreateDefault()
        {
            return new SaveData
            {
                highestClearedStage = 0,
                totalCoins = 0,
                totalGems = 0,
                upgradeLevels = new int[8],
                adRemovalPurchased = false,
                tutorialCompleted = false,
                lastDailyRewardTime = 0,
                totalPlayTimeSeconds = 0,
                totalStagesPlayed = 0
            };
        }
    }
}
