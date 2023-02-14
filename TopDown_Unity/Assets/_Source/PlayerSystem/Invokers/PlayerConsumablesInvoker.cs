using InputSystem;
using PlayerSystem.Consumables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSystem
{
    public class PlayerConsumablesInvoker
    {
        private InputHandler _input;

        private PlayerResources _resources;
        private Patch _patch;

        private bool _isPatchCraft;

        public PlayerConsumablesInvoker(InputHandler input, PlayerResources resources, Patch patch)
        {
            _input = input;
            _resources = resources;
            _patch = patch;
        }

        public void Bind()
        {
            _input.ConsumablesActions.PatchCraft.performed += CraftPatch;
            _input.ConsumablesActions.PatchUse.canceled += UsePatch;
        }

        public void Expose()
        {
            _input.ConsumablesActions.PatchCraft.performed -= CraftPatch;
            _input.ConsumablesActions.PatchUse.canceled -= UsePatch;
        }

        private void CraftPatch(InputAction.CallbackContext ctx)
        {
            _isPatchCraft = true;
            if (!_resources.Consume(_patch.Cost))
            {
                return;
            }
            
            _patch.Craft();
        }

        private void UsePatch(InputAction.CallbackContext ctx)
        {
            if (_isPatchCraft)
            {
                _isPatchCraft = false;
                return;
            }
            
            _patch.Use();
        }
    }
}