using PlayerSystem.Data;
using UnityEngine;

namespace ProgressSystem.Upgrades
{
    public abstract class AUpgradeSO : ScriptableObject
    {
        public abstract void Upgrade(PlayerSetUpSO playerSetUpSO);
        
        [field: SerializeField] public string Info { get; private set; }
    }
}