﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using Utils.Events;

namespace UI.Menu
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private GameObject resulPanel;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string winText;
        [SerializeField] private string loseText;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            Signals.Get<PlayerDeadSignal>().AddListener(Lose);
            Signals.Get<AllLevelEndSignal>().AddListener(Win);
            
            continueButton.onClick.AddListener(Continue);
            exitButton.onClick.AddListener(Exit);
        }

        private void RemoveEvent()
        {
            Signals.Get<PlayerDeadSignal>().RemoveListener(Lose);
            Signals.Get<AllLevelEndSignal>().RemoveListener(Win);
            
            continueButton.onClick.RemoveListener(Continue);
            exitButton.onClick.RemoveListener(Exit);
        }

        private void Lose()
        {
            Time.timeScale = 0;
            
            resulPanel.SetActive(true);
            text.text = loseText;
        }

        private void Win()
        {
            if (SceneManager.sceneCountInBuildSettings - 1 > SceneManager.GetActiveScene().buildIndex)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
            Time.timeScale = 0;
            
            resulPanel.SetActive(true);
            text.text = winText;
        }

        private void Continue()
        {
            Time.timeScale = 1;

            RemoveEvent();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Exit()
        {
            Time.timeScale = 1;

            RemoveEvent();
            
            SceneManager.LoadScene(0);
        }
    }
}