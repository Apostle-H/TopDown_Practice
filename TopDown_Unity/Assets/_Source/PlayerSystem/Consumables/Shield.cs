using DG.Tweening;
using PlayerSystem.Data.Consumables;
using UnityEngine;

namespace PlayerSystem.Consumables
{
    public class Shield : IConsumable
    {
        private GameObject _area;
        private ShieldSO _so;

        private int _availableAmount;
        private Sequence _offSequence;
        
        public int Cost => _so.Cost;

        public Shield(GameObject area, ShieldSO so)
        {
            _area = area;
            _so = so;
            
            InitSequence();
        }

        public void Craft()
        {
            _availableAmount++;
        }

        public void Use()
        {
            if (_availableAmount <= 0)
            {
                return;
            }

            _area.SetActive(true);
            _availableAmount--;
            
            _offSequence.Restart();
        }

        private void Off() => 
            _area.SetActive(false);

        private void InitSequence()
        {
            _offSequence = DOTween.Sequence();
            _offSequence.SetAutoKill(false);
            _offSequence.Pause();

            _offSequence.AppendInterval(_so.Durarion);
            _offSequence.AppendCallback(Off);
        }
    }
}