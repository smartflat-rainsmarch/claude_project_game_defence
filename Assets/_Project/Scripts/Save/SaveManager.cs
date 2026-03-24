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
            string json = JsonUtility.ToJson(currentData);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
            Debug.Log("[SaveManager] Data saved");
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                string json = PlayerPrefs.GetString(SaveKey);
                currentData = JsonUtility.FromJson<SaveData>(json);
                Debug.Log("[SaveManager] Data loaded");
            }
            else
            {
                currentData = SaveData.CreateDefault();
                Debug.Log("[SaveManager] No save found, created default");
            }
        }

        public void ResetData()
        {
            currentData = SaveData.CreateDefault();
            Save();
            Debug.Log("[SaveManager] Data reset");
        }

        public void AddCoins(int amount)
        {
            currentData.totalCoins += amount;
            Save();
        }

        public void SetHighestStage(int stage)
        {
            if (stage > currentData.highestClearedStage)
            {
                currentData.highestClearedStage = stage;
                Save();
            }
        }
    }
}
