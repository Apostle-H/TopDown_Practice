using PlayerSystem.Data;
using ProgressSystem.Upgrades;

namespace ProgressSystem
{
    public class PlayerModifier
    {
        private PlayerSetUpSO _defaultSetUpSO;
        private PlayerSetUpSO _currentSetUpSO;

        public PlayerModifier(PlayerSetUpSO defaultSetUpSO, PlayerSetUpSO currentSetUpSO)
        {
            _defaultSetUpSO = defaultSetUpSO;
            _currentSetUpSO = currentSetUpSO;
        }

        public void Modify(AUpgradeSO newUpgrade) => 
            newUpgrade.Upgrade(_currentSetUpSO);
    }
}