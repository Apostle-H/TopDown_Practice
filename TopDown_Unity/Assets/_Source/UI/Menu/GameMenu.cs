using DG.Tweening;
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
        [SerializeField] private CanvasGroup blackBackground;
        [SerializeField] private GameObject resulPanel;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string loseText;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button exitButton;

        private const float DURATION = 1.5f;

        private void Awake()
        {
            continueButton.onClick.AddListener(Continue);
            exitButton.onClick.AddListener(Exit);
            
            blackBackground.DOFade(endValue: 0, DURATION)
                .OnComplete(() => 
                {
                    blackBackground.gameObject.SetActive(false);
                    blackBackground.GetComponent<CanvasGroup>().DOKill();
                });
        }

        private void OnEnable()
        {
            Signals.Get<PlayerDeadSignal>().AddListener(Lose);
            Signals.Get<AllLevelEndSignal>().AddListener(NextScene);
        }

        private void OnDisable()
        {
            Signals.Get<PlayerDeadSignal>().RemoveListener(Lose);
            Signals.Get<AllLevelEndSignal>().RemoveListener(NextScene);
        }

        private void RemoveEvent()
        {
            continueButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveListener(Exit);
        }

        private void Lose()
        {
            Time.timeScale = 0;
            
            resulPanel.SetActive(true);
            text.text = loseText;
        }

        private void NextScene()
        {
            TransitionBetweenScenes(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void Continue()
        {
            Time.timeScale = 1;

            RemoveEvent();
            TransitionBetweenScenes(SceneManager.GetActiveScene().buildIndex);
        }

        private void Exit()
        {
            Time.timeScale = 1;

            RemoveEvent();
            TransitionBetweenScenes(0);
        }

        private void TransitionBetweenScenes(int sceneIndex)
        {
            blackBackground.gameObject.SetActive(true);
            blackBackground.DOFade(endValue: 1, DURATION)
                .OnComplete(() => 
                {
                    SceneManager.LoadScene(sceneIndex);
                    blackBackground.DOKill();
                });
        }
    }
}