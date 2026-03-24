using UnityEngine;

namespace LastLineDefense.Save
{
    public class SaveManager : MonoBehaviour
    {
        private const string SaveKey = "LastLineDefense_SaveData";

        private SaveData currentData;

        public SaveData GetSaveData()
        {
            if (currentData == null)
                Load();
            return currentData;
        }

        private void Awake()
        {
            Load();
        }

        public void Save()
        {
            if (currentData == null) return;

            try
            {
                string json = JsonUtility.ToJson(currentData);
                PlayerPrefs.SetString(SaveKey, json);
                PlayerPrefs.Save();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[SaveManager] Save failed: {ex.Message}");
            }
        }

        public void Load()
        {
            try
            {
                if (PlayerPrefs.HasKey(SaveKey))
                {
                    string json = PlayerPrefs.GetString(SaveKey);
                    currentData = JsonUtility.FromJson<SaveData>(json);

                    if (currentData == null)
                    {
                        Debug.LogWarning("[SaveManager] Corrupted save data, resetting to default");
                        currentData = SaveData.CreateDefault();
                    }
                }
                else
                {
                    currentData = SaveData.CreateDefault();
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[SaveManager] Load failed: {ex.Message}");
                currentData = SaveData.CreateDefault();
            }
        }

        public void ResetData()
        {
            currentData = SaveData.CreateDefault();
            Save();
        }

        public void AddCoins(int amount)
        {
            if (currentData == null) return;
            currentData.totalCoins += amount;
            Save();
        }

        public void SetHighestStage(int stage)
        {
            if (currentData == null) return;
            if (stage > currentData.highestClearedStage)
            {
                currentData.highestClearedStage = stage;
                Save();
            }
        }
    }
}
