using PlayerSystem;

namespace UI.Player
{
    public class PlayerUIController
    {
        private PlayerUIModel _model;
        private PlayerUIView _view;

        private PlayerHealth _health;

        public PlayerUIController(PlayerUIModel model, PlayerUIView view, PlayerHealth health)
        {
            _model = model;
            _view = view;
            _health = health;

            _health.OnDamaged += UpdateHealth;
            _health.OnHeal += UpdateHealth;
        }

        private void UpdateHealth()
        {
            _view.UpdateHealth((float)_health.CurrentHealth / (float)_model.MaxHealth);
        }
    }
}