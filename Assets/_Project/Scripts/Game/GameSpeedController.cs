using UnityEngine;
using UnityEngine.UI;

namespace LastLineDefense.Game
{
    public class GameSpeedController : MonoBehaviour
    {
        [SerializeField] private Button speedButton;
        [SerializeField] private TMPro.TMP_Text speedText;

        private int speedIndex;
        private readonly float[] speedOptions = { 1f, 2f };

        private void Start()
        {
            speedIndex = 0;
            ApplySpeed();

            if (speedButton != null)
                speedButton.onClick.AddListener(ToggleSpeed);
        }

        private void ToggleSpeed()
        {
            speedIndex = (speedIndex + 1) % speedOptions.Length;
            ApplySpeed();
        }

        private void ApplySpeed()
        {
            Time.timeScale = speedOptions[speedIndex];

            if (speedText != null)
                speedText.text = $"x{speedOptions[speedIndex]}";
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }
    }
}
