using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup blackBackground;
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject settingsPanel;
        
        private const float DURATION = 1.5f;

        private void Awake()
        {
            startButton.onClick.AddListener(StartGame);
            settingsButton.onClick.AddListener(OpenSettings);
            exitButton.onClick.AddListener(CloseSettings);
            
            blackBackground.DOFade(endValue: 0, DURATION)
                .OnComplete(() => 
                {
                    blackBackground.gameObject.SetActive(false);
                    blackBackground.DOKill();
                });
        }

        private void StartGame()
        {
            RemoveEvent();
            
            blackBackground.gameObject.SetActive(true);
            blackBackground.DOFade(endValue: 1, DURATION)
                .OnComplete(() => 
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    blackBackground.DOKill();
                });
        }

        private void OpenSettings()
        {
            settingsPanel.SetActive(true);
            mainPanel.SetActive(false);
        }

        private void CloseSettings()
        {
            settingsPanel.SetActive(false);
            mainPanel.SetActive(true);
        }

        private void RemoveEvent()
        {
            startButton.onClick.RemoveListener(StartGame);
            settingsButton.onClick.RemoveListener(OpenSettings);
            exitButton.onClick.RemoveListener(CloseSettings);
        }
    }
}