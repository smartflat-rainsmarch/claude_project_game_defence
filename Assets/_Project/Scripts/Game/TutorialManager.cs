using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LastLineDefense.Core;
using LastLineDefense.Save;

namespace LastLineDefense.Game
{
    public class TutorialManager : MonoBehaviour
    {
        [Header("Tutorial Steps")]
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private TMP_Text tutorialText;
        [SerializeField] private Button nextButton;

        private int currentStep;
        private bool tutorialActive;

        private readonly string[] tutorialMessages = new string[]
        {
            "Welcome! Enemies will march along the path.",
            "Tap a slot to place a tower. Towers attack automatically.",
            "Spend gold wisely. Defeat enemies to earn more!",
            "Survive all waves to clear the stage. Good luck!"
        };

        private void Start()
        {
            var saveManager = FindAnyObjectByType<SaveManager>();
            if (saveManager != null && saveManager.GetSaveData().tutorialCompleted)
            {
                if (tutorialPanel != null)
                    tutorialPanel.SetActive(false);
                return;
            }

            StartTutorial();
        }

        private void StartTutorial()
        {
            currentStep = 0;
            tutorialActive = true;

            if (tutorialPanel != null)
                tutorialPanel.SetActive(true);

            if (nextButton != null)
            {
                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(NextStep);
            }

            ShowStep();
        }

        private void ShowStep()
        {
            if (currentStep >= tutorialMessages.Length)
            {
                CompleteTutorial();
                return;
            }

            if (tutorialText != null)
                tutorialText.text = tutorialMessages[currentStep];
        }

        private void NextStep()
        {
            currentStep++;
            ShowStep();
        }

        private void CompleteTutorial()
        {
            tutorialActive = false;

            if (tutorialPanel != null)
                tutorialPanel.SetActive(false);

            var saveManager = FindAnyObjectByType<SaveManager>();
            if (saveManager != null)
            {
                var data = saveManager.GetSaveData();
                data.tutorialCompleted = true;
                saveManager.Save();
            }

            Debug.Log("[Tutorial] Completed");
        }
    }
}
