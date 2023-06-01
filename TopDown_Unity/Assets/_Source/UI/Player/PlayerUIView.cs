using EnemySystem.Data.Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Player
{
    public class PlayerUIView : MonoBehaviour
    {
        [SerializeField] private Image hpBar;
        [SerializeField] private Image gunBar;

        [SerializeField] private AcneRangeAttackerSO acne; 

        [SerializeField] private TextMeshProUGUI resourcesText;
        
        public void UpdateHealth(float fillPercentage)
        {
            hpBar.fillAmount = fillPercentage;
        }

        public void UpdateGun(float fillPercentage)
        {
            gunBar.fillAmount = fillPercentage;
        }

        public void UpdateResources(int amount) =>
            resourcesText.text = $"{amount.ToString()}/{acne.AmountResourceToRemoveShield}";
    }
}