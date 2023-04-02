using EntitySystem.Data.Combat;
using PlayerSystem.Data;
using UnityEngine;

namespace ProgressSystem.Upgrades
{
    [CreateAssetMenu(menuName = "SO/ProgressSystem/Upgrades/AttackerUpgrade", fileName = "NewAttackerUpgrade")]
    public class AttackerUpgradeSO : AUpgradeSO
    {
        [field: SerializeField] public AttackerSO NewAttackerSO { get; private set; }
        
        public override void Upgrade(PlayerSetUpSO playerSetUpSO) =>
            playerSetUpSO.SetAttacker(NewAttackerSO);
    }
}