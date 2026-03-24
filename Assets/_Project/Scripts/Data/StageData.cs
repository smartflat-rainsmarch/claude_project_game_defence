using UnityEngine;

namespace LastLineDefense.Data
{
    [CreateAssetMenu(fileName = "NewStageData", menuName = "Defense/Stage Data")]
    public class StageData : ScriptableObject
    {
        public int stageIndex;
        public int startingGold = 120;
        public int baseHp = 20;
        public int clearReward = 60;
        public int failReward = 18;
        public WaveData[] waves;
    }
}
