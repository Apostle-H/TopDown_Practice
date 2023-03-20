using EntitySystem.Shooting;
using InputSystem;
using UnityEngine.InputSystem;

namespace PlayerSystem.Invokers
{
    public class PlayerShootInvoker
    {
        private readonly InputHandler _input;

        private readonly Attacker _attacker;

        public PlayerShootInvoker(InputHandler input, Attacker attacker)
        {
            _input = input;
            _attacker = attacker;
        }

        public void Bind()
        {
            _input.ShootActions.Attack.started += SetShoot;
            _input.ShootActions.Attack.canceled += SetShoot;
        }

        public void Expose()
        {
            _input.ShootActions.Attack.started -= SetShoot;
            _input.ShootActions.Attack.canceled -= SetShoot;
            
            _attacker.StopShoot();
        }

        private void SetShoot(InputAction.CallbackContext ctx)
        {
            if (ctx.phase == InputActionPhase.Started)
            {
                _attacker.StartShoot();
            }
            else if (ctx.phase == InputActionPhase.Canceled)
            {
                _attacker.StopShoot();
            }
        }
    }
}