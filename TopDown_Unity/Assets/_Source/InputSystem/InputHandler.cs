namespace InputSystem
{
    public class InputHandler
    {
        private MainActions _actions;

        public MainActions.MovementActions MovementActions => _actions.Movement;
        public MainActions.AttackActions AttackActions => _actions.Attack;
        
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
