using InputSystem;
using PlayerSystem.Invokers;

namespace Core
{
    public class Game
    {
        private readonly InputHandler _input;
        private readonly PlayerMasterInvoker _playerMasterInvoker;
        
        public Game(InputHandler input, PlayerMasterInvoker playerMasterInvoker)
        {
            _input = input;
            _playerMasterInvoker = playerMasterInvoker;
        }

        public void Start()
        {
            _input.Enable();
            _playerMasterInvoker.Bind();
        }
    }
}