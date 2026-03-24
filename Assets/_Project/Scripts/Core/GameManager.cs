using UnityEngine;
using UnityEngine.SceneManagement;

namespace LastLineDefense.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Scene Names")]
        [SerializeField] private string lobbySceneName = "Lobby";
        [SerializeField] private string stageSceneName = "Stage";

        [Header("State")]
        [SerializeField] private int selectedStageIndex;

        public int SelectedStageIndex => selectedStageIndex;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetSelectedStage(int stageIndex)
        {
            selectedStageIndex = stageIndex;
        }

        public void LoadLobby()
        {
            GameEvents.ClearAll();
            SceneManager.LoadScene(lobbySceneName);
        }

        public void LoadStage()
        {
            GameEvents.ClearAll();
            SceneManager.LoadScene(stageSceneName);
        }

        public void LoadStage(int stageIndex)
        {
            selectedStageIndex = stageIndex;
            LoadStage();
        }
    }
}
