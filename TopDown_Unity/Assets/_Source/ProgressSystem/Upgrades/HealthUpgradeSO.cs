using EntitySystem.Data.Interactions;
using PlayerSystem.Data;
using UnityEngine;

namespace ProgressSystem.Upgrades
{
    [CreateAssetMenu(menuName = "SO/ProgressSystem/Upgrades/HealthUpgrade", fileName = "NewHealthUpgrade")]
    public class HealthUpgradeSO : AUpgradeSO
    {
        [field: SerializeField] public HealthSO NewHealthSO { get; private set; }

        public override void Upgrade(PlayerSetUpSO playerSetUpSO) =>
            playerSetUpSO.SetHealth(NewHealthSO);
    }
}