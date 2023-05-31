using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject settingsPanel;

        private void Awake()
        {
            startButton.onClick.AddListener(StartGame);
            settingsButton.onClick.AddListener(OpenSettings);
            exitButton.onClick.AddListener(CloseSettings);
        }

        private void StartGame()
        {
            RemoveEvent();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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