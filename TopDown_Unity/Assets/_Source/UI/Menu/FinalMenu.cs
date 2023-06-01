using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menu
{
    public class FinalMenu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup blackBackground;
        [SerializeField] private Button exit;
        
        private const float DURATION = 1.5f;

        private void Awake()
        {
            exit.onClick.AddListener(ReturnMainMenu);
            
            blackBackground.DOFade(endValue: 0, DURATION)
                .OnComplete(() => 
                {
                    blackBackground.gameObject.SetActive(false);
                    blackBackground.DOKill();
                });
        }
        
        private void ReturnMainMenu()
        {
            Time.timeScale = 1;

            exit.onClick.RemoveListener(ReturnMainMenu);
            
            blackBackground.gameObject.SetActive(true);
            blackBackground.DOFade(endValue: 1, DURATION)
                .OnComplete(() => 
                {
                    SceneManager.LoadScene(0);
                    blackBackground.DOKill();
                });
        }
    }
}