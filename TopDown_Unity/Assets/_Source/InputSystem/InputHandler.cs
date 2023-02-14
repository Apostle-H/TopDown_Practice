namespace InputSystem
{
    public class InputHandler
    {
        private MainActions _actions;

        public MainActions.MovementActions MovementActions => _actions.Movement;
        public MainActions.ShootActions ShootActions => _actions.Shoot;
        public MainActions.InteractionsActions InteractionActions => _actions.Interactions;
        public MainActions.ConsumablesActions ConsumablesActions => _actions.Consumables;
        
        public bool IsEnabled => _actions.asset.enabled;

        public InputHandler()
        {
            _actions = new MainActions();
        }

        public void Enable()
        {
            _actions.Enable();
        }

        public void Disable()
        {
            _actions.Disable();
        }
    }
}
