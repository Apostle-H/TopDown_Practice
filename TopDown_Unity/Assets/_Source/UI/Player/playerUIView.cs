using UnityEngine;
using UnityEngine.UI;

namespace UI.Player
{
    public class playerUIView : MonoBehaviour
    {
        [SerializeField] private Image hpBar;

        public void UpdateHealth(float fillPercentage)
        {
            hpBar.fillAmount = fillPercentage;
        }
    }
}