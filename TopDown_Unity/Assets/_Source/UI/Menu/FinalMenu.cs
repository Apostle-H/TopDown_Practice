using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menu
{
    public class FinalMenu : MonoBehaviour
    {
        [SerializeField] private Button exit;

        private void Awake()
        {
            exit.onClick.AddListener(ReturnMainMenu);
        }
        
        private void ReturnMainMenu()
        {
            Time.timeScale = 1;

            exit.onClick.RemoveListener(ReturnMainMenu);
            
            SceneManager.LoadScene(0);
        }
    }
}