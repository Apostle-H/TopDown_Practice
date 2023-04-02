using EntitySystem.Data.Combat;
using PlayerSystem.Data;
using UnityEngine;

namespace ProgressSystem.Upgrades
{
    [CreateAssetMenu(menuName = "SO/ProgressSystem/Upgrades/HookShooterUpgrade", fileName = "NewHookShooterUpgrade")]
    public class HookShooterUpgradeSO : AUpgradeSO
    {
        [field: SerializeField] public HookShooterSO NewHookShooter { get; private set; }

        public override void Upgrade(PlayerSetUpSO playerSetUpSO) =>
            playerSetUpSO.SetHookShooter(NewHookShooter);
    }
}