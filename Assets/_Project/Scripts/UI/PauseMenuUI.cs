using UnityEngine;
using UnityEngine.UI;
using LastLineDefense.Core;

namespace LastLineDefense.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] private GameObject pausePanel;

        [Header("Buttons")]
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button quitButton;

        private bool isPaused;

        private void Start()
        {
            if (pausePanel != null)
                pausePanel.SetActive(false);

            if (pauseButton != null)
                pauseButton.onClick.AddListener(Pause);

            if (resumeButton != null)
                resumeButton.onClick.AddListener(Resume);

            if (quitButton != null)
                quitButton.onClick.AddListener(QuitToLobby);
        }

        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0f;

            if (pausePanel != null)
                pausePanel.SetActive(true);
        }

        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1f;

            if (pausePanel != null)
                pausePanel.SetActive(false);
        }

        private void QuitToLobby()
        {
            Time.timeScale = 1f;
            if (GameManager.Instance != null)
                GameManager.Instance.LoadLobby();
        }

        private void OnDestroy()
        {
            if (isPaused)
                Time.timeScale = 1f;
        }
    }
}
