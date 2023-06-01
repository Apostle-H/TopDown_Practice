using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Player
{
    public class PlayerUIView : MonoBehaviour
    {
        [SerializeField] private Image hpBar;
        [SerializeField] private Image gunBar;

        [SerializeField] private TextMeshProUGUI resourcesText;
        
        // [SerializeField] private TextMeshProUGUI patchKeyText;
        // [SerializeField] private TextMeshProUGUI patchCraftedText;
        //
        // [SerializeField] private TextMeshProUGUI shieldKeyText;
        // [SerializeField] private TextMeshProUGUI shieldCraftedText;

        public void UpdateHealth(float fillPercentage)
        {
            hpBar.fillAmount = fillPercentage;
        }

        public void UpdateGun(float fillPercentage)
        {
            gunBar.fillAmount = fillPercentage;
        }

        public void UpdateResources(int amount) =>
            resourcesText.text = amount.ToString();

        // public void SetConsumablesKeys(string patchKey, string shieldKey)
        // {
        //     patchKeyText.text = patchKey;
        //     shieldKeyText.text = shieldKey;
        // }

        // public void UpdatePatchCrafted(int crafted) =>
        //     UpdateCrafted(patchCraftedText, crafted.ToString());
        
        // public void UpdateShieldCrafted(int crafted) =>
        //     UpdateCrafted(shieldCraftedText, crafted.ToString());

        private void UpdateCrafted(TextMeshProUGUI craftedText, string crafted) => 
            craftedText.text = crafted;
    }
}