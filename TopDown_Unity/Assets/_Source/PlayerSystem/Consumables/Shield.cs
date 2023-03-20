using System;
using DG.Tweening;
using PlayerSystem.Data.Consumables;
using UnityEngine;

namespace PlayerSystem.Consumables
{
    public class Shield : IConsumable
    {
        private GameObject _area;
        private ShieldSO _so;

        private Sequence _offer;
        
        public int Cost => _so.Cost;
        public int Crafted { get; private set; }

        public event Action OnCrafted;
        public event Action OnUsed;

        public Shield(GameObject area, ShieldSO so)
        {
            _area = area;
            _so = so;
            
            InitSequence();
        }

        public void Craft()
        {
            Crafted++;

            OnCrafted?.Invoke();
        }

        public void Use()
        {
            if (Crafted <= 0)
            {
                return;
            }

            _area.SetActive(true);
            Crafted--;
            _offer.Restart();
            
            OnUsed?.Invoke();
        }

        private void Off() => 
            _area.SetActive(false);

        private void InitSequence()
        {
            _offer = DOTween.Sequence();
            _offer.SetAutoKill(false);
            _offer.Pause();

            _offer.AppendInterval(_so.Durarion);
            _offer.AppendCallback(Off);
        }
    }
}