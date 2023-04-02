using EntitySystem.Data.Movement;
using PlayerSystem.Data;
using UnityEngine;

namespace ProgressSystem.Upgrades
{
    [CreateAssetMenu(menuName = "SO/ProgressSystem/Upgrades/MoverUpgrade", fileName = "NewMoverUpgrade")]
    public class MoverUpgradeSO : AUpgradeSO
    {
        [field: SerializeField] public MoverSO NewMoverSO { get; private set; }

        public override void Upgrade(PlayerSetUpSO playerSetUpSO) =>
            playerSetUpSO.SetMover(NewMoverSO);
    }
}