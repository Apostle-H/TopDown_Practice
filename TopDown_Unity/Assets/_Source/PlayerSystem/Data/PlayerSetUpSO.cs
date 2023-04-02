using EntitySystem.Data.Combat;
using EntitySystem.Data.Interactions;
using EntitySystem.Data.Movement;
using PlayerSystem.Data.Consumables;
using PlayerSystem.Data.Interactions;
using UnityEngine;

namespace PlayerSystem.Data
{
    [CreateAssetMenu(menuName = "SO/PlayerSystem/SetUp", fileName = "NewPlayerSetUp")]
    public class PlayerSetUpSO : ScriptableObject
    {
        [field: SerializeField] public HealthSO HealthSO { get; private set; }
        [field: SerializeField] public MoverSO MoverSO { get; private set; }
        [field: SerializeField] public AttackerSO AttackerSO { get; private set; }
        [field: SerializeField] public AreaCheckerSO DragAreaCheckerSO { get; private set; }
        [field: SerializeField] public HookShooterSO HookShooterSO { get; private set; }
        [field: SerializeField] public AreaCheckerSO SplitAreaCheckerSO { get; private set; }
        [field: SerializeField] public PatchSO PatchSO { get; private set; }
        [field: SerializeField] public ShieldSO ShieldSO { get; private set; }

        public void SetHealth(HealthSO newHealthSO) =>
            HealthSO = newHealthSO;
        
        public void SetMover(MoverSO newMoverSO) =>
            MoverSO = newMoverSO;
        
        public void SetAttacker(AttackerSO newAttackerSO) =>
            AttackerSO = newAttackerSO;
        
        public void SetHookShooter(HookShooterSO newHookShooterSO) =>
            HookShooterSO = newHookShooterSO;
    }
}