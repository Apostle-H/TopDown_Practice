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
        private Shield _shield;

        private bool _isPatchCraft;
        private bool _isShieldCraft;

        public PlayerConsumablesInvoker(InputHandler input, PlayerResources resources, Patch patch, Shield shield)
        {
            _input = input;
            _resources = resources;
            _patch = patch;
            _shield = shield;
        }

        public void Bind()
        {
            _input.ConsumablesActions.Patch.performed += CraftPatch;
            _input.ConsumablesActions.Patch.canceled += UsePatch;

            _input.ConsumablesActions.Shield.performed += CraftShield;
            _input.ConsumablesActions.Shield.canceled += UseShield;
        }

        public void Expose()
        {
            _input.ConsumablesActions.Patch.performed -= CraftPatch;
            _input.ConsumablesActions.Patch.canceled -= UsePatch;
            
            _input.ConsumablesActions.Shield.performed -= CraftShield;
            _input.ConsumablesActions.Shield.canceled -= UseShield;
        }

        private void CraftPatch(InputAction.CallbackContext ctx) => 
            CraftConsumable(out _isPatchCraft, _patch);

        private void UsePatch(InputAction.CallbackContext ctx) =>
            UseConsumable(ref _isPatchCraft, _patch);

        private void CraftShield(InputAction.CallbackContext ctx) =>
            CraftConsumable(out _isShieldCraft, _shield);

        private void UseShield(InputAction.CallbackContext ctx) => 
            UseConsumable(ref _isShieldCraft, _shield);

        private void CraftConsumable(out bool isCraft, IConsumable consumable)
        {
            isCraft = true;
            if (!_resources.Consume(consumable.Cost))
                return;
            
            consumable.Craft();
        }
        
        private void UseConsumable(ref bool isCraft, IConsumable consumable)
        {
            if (isCraft)
            {
                isCraft = false;
                return;
            }
            
            consumable.Use();
        }
    }
}