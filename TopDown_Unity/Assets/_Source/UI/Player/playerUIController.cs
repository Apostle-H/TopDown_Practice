using PlayerSystem;

namespace UI.Player
{
    public class playerUIController
    {
        private playerUIModel _model;
        private playerUIView _view;

        private PlayerHealth _health;

        public playerUIController(playerUIModel model, playerUIView view, PlayerHealth health)
        {
            _model = model;
            _view = view;
            _health = health;

            _health.OnDamaged += UpdateHealth;
        }

        private void UpdateHealth()
        {
            _view.UpdateHealth((float)_health.CurrentHealth / (float)_model.MaxHealth);
        }
    }
}