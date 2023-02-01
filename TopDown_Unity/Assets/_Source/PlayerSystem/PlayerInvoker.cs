using InputSystem;
using PlayerSystem.Shooting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSystem.Movement
{
    public class PlayerInvoker
    {
        private InputHandler _input;

        private Transform _transform;
        private Mover _mover;
        private Shooter _shooter;
        
        public PlayerInvoker(InputHandler input, Transform transform, Mover mover, Shooter shooter)
        {
            _input = input;
            _transform = transform;
            _mover = mover;
            _shooter = shooter;
        }

        public void Bind()
        {
            _input.MovementActions.Direction.performed += UpdateDirection;
            _input.MovementActions.Direction.canceled += UpdateDirection;
            _input.AttackActions.Shoot.performed += Shoot;
            _input.AttackActions.MousePos.performed += RotateGun;
        }

        public void Expose()
        {
            _input.MovementActions.Direction.performed -= UpdateDirection;
            _input.MovementActions.Direction.canceled -= UpdateDirection;
            _input.AttackActions.Shoot.performed -= Shoot;
        }

        private void UpdateDirection(InputAction.CallbackContext ctx) =>
            _mover.UpdateDirection(ctx.ReadValue<Vector2>());

        private void Shoot(InputAction.CallbackContext ctx) =>
            _shooter.Shoot();

        private void RotateGun(InputAction.CallbackContext ctx)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            float rotationAngle = Mathf.Atan2(mousePos.y - _transform.position.y, mousePos.x - _transform.position.x) * (180/Mathf.PI) - 90;
            _shooter.Rotate(rotationAngle);
        }
    }
}