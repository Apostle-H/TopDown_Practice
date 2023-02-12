using InputSystem;
using PlayerSystem;

namespace Core
{
    public class Game
    {
        private InputHandler _input;
        private PlayerMasterInvoker _playerMasterInvoker;
        
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