using UnityEngine;
using LastLineDefense.Data;

namespace LastLineDefense.Game
{
    public class StageDataLoader : MonoBehaviour
    {
        [Header("All Stage Data")]
        [SerializeField] private StageData[] allStages;

        public StageData GetStage(int index)
        {
            if (allStages == null || index < 0 || index >= allStages.Length)
            {
                Debug.LogWarning($"[StageDataLoader] Stage {index} not found. Total: {allStages?.Length ?? 0}");
                return null;
            }
            return allStages[index];
        }

        public int TotalStages => allStages != null ? allStages.Length : 0;

        public StageData GetCurrentStage()
        {
            int index = Core.GameManager.Instance != null
                ? Core.GameManager.Instance.SelectedStageIndex
                : 0;
            return GetStage(index);
        }
    }
}
