using InputSystem;
using UnityEngine.InputSystem;

namespace UI.Player
{
    public class PlayerUIModel
    {
        private InputHandler _input;
        

        public string PatchKey => _input.ConsumablesActions.Patch.GetBindingDisplayString().Split(' ')[1];
        public string ShieldKey => _input.ConsumablesActions.Shield.GetBindingDisplayString().Split(' ')[1];
        
        public PlayerUIModel(InputHandler input)
        {
            _input = input;
        }
    }
}