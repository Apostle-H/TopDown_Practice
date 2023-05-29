namespace PlayerSystem.Invokers
{
    public class PlayerMasterInvoker
    {
        private readonly PlayerMoveInvoker _moveInvoker;
        private readonly PlayerShootInvoker _shootInvoker;
        private readonly PlayerHookInvoker _hookInvoker;
        private readonly PlayerSplitInvoker _splitInvoker;
        private readonly PlayerConsumablesInvoker _consumablesInvoker;

        private readonly PlayerHealth _health;

        public PlayerMasterInvoker(PlayerMoveInvoker moveInvoker, PlayerShootInvoker shootInvoker, PlayerHookInvoker hookInvoker, 
            PlayerSplitInvoker splitInvoker, PlayerConsumablesInvoker consumablesInvoker, PlayerHealth health)
        {
            _moveInvoker = moveInvoker;
            _shootInvoker = shootInvoker;
            _hookInvoker = hookInvoker;
            _consumablesInvoker = consumablesInvoker;
            _splitInvoker = splitInvoker;
            
            _health = health;
        }

        public void Bind()
        {
            _health.OnDeath += Expose;
            
            _moveInvoker.Bind();
            _shootInvoker.Bind();
            _hookInvoker.Bind();
            _splitInvoker.Bind();
            _consumablesInvoker.Bind();

            // _hookInvoker.OnHookOut += _shootInvoker.Expose;
            // _hookInvoker.OnHookIn += _shootInvoker.Bind;
            _hookInvoker.OnHooked += _moveInvoker.SlowDownCarrying;
            _hookInvoker.OnReleased += _moveInvoker.SpeedUpReleasing;
        }

        public void Expose()
        {
            // _hookInvoker.OnHookOut -= _shootInvoker.Expose;
            // _hookInvoker.OnHookIn -= _shootInvoker.Bind;
            _hookInvoker.OnHooked -= _moveInvoker.SlowDownCarrying;
            _hookInvoker.OnReleased -= _moveInvoker.SpeedUpReleasing;
            
            _moveInvoker.Expose();
            // _shootInvoker.Expose();
            _hookInvoker.Expose();
            _splitInvoker.Bind();
            _consumablesInvoker.Expose();
        }
    }
}