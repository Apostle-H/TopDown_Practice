using EntitySystem.Interactions;
using InputSystem;
using PlayerSystem.Data.Interactions;
using PlayerSystem.Interactions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using Utils.Services;

namespace PlayerSystem.Invokers
{
    public class PlayerSplitInvoker
    {
        private InputHandler _input;
        private Transform _transform;
        private Splitter _splitter;
        private AreaCheckerSO _splitAreaCheckerSO;

        public PlayerSplitInvoker(InputHandler input, Transform transform, Splitter splitter, AreaCheckerSO splitAreaCheckerSO)
        {
            _input = input;
            _transform = transform;
            _splitter = splitter;
            _splitAreaCheckerSO = splitAreaCheckerSO;
        }

        public void Bind()
        {
            _input.InteractionActions.Split.performed += Split;
        }

        public void Expose()
        {
            _input.InteractionActions.Split.performed -= Split;
        }

        private void Split(InputAction.CallbackContext ctx)
        {
            Rigidbody2D splitTargetRb;
            if ((splitTargetRb = PhysicsService.CheckArea(_transform, _splitAreaCheckerSO)) && 
                splitTargetRb.TryGetComponent(out ISplittable splittableTarget) && 
                splittableTarget.IsSplittable)
            {
                _splitter.Split(splittableTarget);
                splitTargetRb.gameObject.SetActive(false);
            }
        }
    }
}