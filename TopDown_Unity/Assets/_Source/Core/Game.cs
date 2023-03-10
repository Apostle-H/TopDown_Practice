using InputSystem;
using PlayerSystem;

namespace Core
{
    public class Game
    {
        private InputHandler _input;
        private PlayerInvoker _playerInvoker;
        
        public Game(InputHandler input, PlayerInvoker playerInvoker)
        {
            _input = input;
            _playerInvoker = playerInvoker;
        }

        public void Start()
        {
            _input.Enable();
            _playerInvoker.Bind();
        }
    }
}