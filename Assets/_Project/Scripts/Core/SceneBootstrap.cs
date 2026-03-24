using UnityEngine;
using UnityEngine.SceneManagement;

namespace LastLineDefense.Core
{
    public class SceneBootstrap : MonoBehaviour
    {
        [Header("Boot Settings")]
        [SerializeField] private bool isBootScene;
        [SerializeField] private string nextSceneName = "Lobby";

        private void Start()
        {
            if (isBootScene)
            {
                Debug.Log("[Bootstrap] Boot scene initialized. Loading: " + nextSceneName);
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
