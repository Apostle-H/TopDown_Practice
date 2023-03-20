using EntitySystem.Movement;
using EntitySystem.Shooting;
using InputSystem;
using PlayerSystem.Data.Interactions;
using PlayerSystem.Interactions;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Utils.Services;

namespace PlayerSystem.Invokers
{
    public class PlayerMoveInvoker
    {
        private InputHandler _input;

        private Transform _transform;
        private Mover _mover;
        private Rotator _rotator;

        public PlayerMoveInvoker(InputHandler input, Transform transform, Mover mover, Rotator rotator)
        {
            _input = input;
            _transform = transform;
            _mover = mover;
            _rotator = rotator;
        }

        public void Bind()
        {
            _input.MovementActions.MoveDirection.performed += UpdateDirection;
            _input.MovementActions.MoveDirection.canceled += UpdateDirection;
            _input.MovementActions.LookDirection.performed += RotateGun;
        }

        public void Expose()
        {
            _input.MovementActions.MoveDirection.performed -= UpdateDirection;
            _input.MovementActions.MoveDirection.canceled -= UpdateDirection;
            _input.MovementActions.LookDirection.performed -= RotateGun;

            _mover.UpdateDirection(Vector2.zero);
        }

        public void SlowDownCarrying() =>
            _mover.UpdateIsCarrying(true);
        
        public void SpeedUpReleasing() =>
            _mover.UpdateIsCarrying(false);

        private void UpdateDirection(InputAction.CallbackContext ctx) =>
            _mover.UpdateDirection(ctx.ReadValue<Vector2>());
        
        private void RotateGun(InputAction.CallbackContext ctx)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            float rotationAngle = _transform.LookAt2D(mousePos).eulerAngles.z;
            _rotator.Rotate(rotationAngle);
        }
    }
}