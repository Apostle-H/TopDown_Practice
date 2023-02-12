using EntitySystem.Shooting;
using InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSystem.Invokers
{
    public class PlayerShootInvoker
    {
        private InputHandler _input;

        private Attacker _attacker;

        public PlayerShootInvoker(InputHandler input, Transform transform, ShooterRotator shooterRotator, Attacker attacker)
        {
            _input = input;
            _attacker = attacker;
        }

        public void Bind()
        {
            _input.AttackActions.Shoot.started += SetShoot;
            _input.AttackActions.Shoot.canceled += SetShoot;
        }

        public void Expose()
        {
            _input.AttackActions.Shoot.started -= SetShoot;
            _input.AttackActions.Shoot.canceled -= SetShoot;
            
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