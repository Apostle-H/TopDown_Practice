using EntitySystem.Movement;
using EntitySystem.Shooting;
using InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace PlayerSystem.Invokers
{
    public class PlayerMoveInvoker
    {
        private readonly InputHandler _input;

        private readonly Transform _transform;
        private readonly Mover _mover;
        private readonly Rotator _rotator;

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

        private void UpdateDirection(InputAction.CallbackContext ctx)
        {
            _mover.UpdateDirection(ctx.ReadValue<Vector2>());
        }

        private void RotateGun(InputAction.CallbackContext ctx)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            float rotationAngle = _transform.LookAt2D(mousePos).eulerAngles.z;
            _rotator.Rotate(rotationAngle);
        }
    }
}