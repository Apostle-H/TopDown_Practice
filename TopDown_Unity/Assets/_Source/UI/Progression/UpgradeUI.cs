using System;
using ProgressSystem;
using ProgressSystem.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Progression
{
    public class UpgradeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private AUpgradeSO upgradeSO;
        [SerializeField] private Button btn;
        
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private TextMeshProUGUI infoText;

        private PlayerModifier _playerModifier;

        public event Action OnChosen; 

        private void Awake()
        {
            infoText.text = upgradeSO.Info;
        }

        public void Init(PlayerModifier playerModifier)
        {
            _playerModifier = playerModifier;
        }

        public void Bind()
        {
            btn.onClick.AddListener(ApplyUpgrade);
            btn.onClick.AddListener(Chosen);
        }

        public void Open() => 
            mainPanel.SetActive(true);

        private void ApplyUpgrade() =>
            _playerModifier.Modify(upgradeSO);

        private void Chosen() => 
            OnChosen?.Invoke();

        public void OnPointerEnter(PointerEventData eventData) =>
            infoPanel.SetActive(true);

        public void OnPointerExit(PointerEventData eventData) =>
            infoPanel.SetActive(false);
    }
}