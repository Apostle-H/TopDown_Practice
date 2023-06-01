using DG.Tweening;
using EntitySystem.Shooting;
using PlayerSystem;

namespace UI.Player
{
    public class PlayerUIController
    {
        private readonly PlayerUIView _view;

        private readonly PlayerHealth _health;
        private readonly Attacker _attacker;
        private readonly HookShooter _hookShooter;
        private readonly PlayerResources _resources;

        private Sequence _weaponRecharge;

        private float _currentRechargeTime;

        public PlayerUIController(PlayerUIView view, PlayerHealth health, Attacker attacker,
            HookShooter hookShooter, PlayerResources resources)
        {
            _view = view;
            _health = health;
            _attacker = attacker;
            _hookShooter = hookShooter;
            _resources = resources;

            InitSequence();

            _health.OnDamaged += UpdateHealth;
            _health.OnHeal += UpdateHealth;
            
            _attacker.OnShoot += StartWeaponRecharge;
            _hookShooter.OnHookOut += WeaponDisabled;
            _hookShooter.OnHookIn += WeaponEnabled;

            _resources.OnAdded += UpdateResources;
            _resources.OnConsumed += UpdateResources;
            
            UpdateResources();
        }

        private void UpdateHealth()
        {
            _view.UpdateHealth((float)_health.CurrentHealth / (float)_health.MaxHealth);
        }

        private void StartWeaponRecharge()
        {
            _currentRechargeTime = _attacker.ShootDelay;
            _weaponRecharge.Restart();
        }

        private void WeaponDisabled()
        {
            _weaponRecharge.Pause();
            _view.UpdateGun(1f);
        }

        private void WeaponEnabled() => 
            _view.UpdateGun(0f);

        private void UpdateResources() =>
            _view.UpdateResources(_resources.Amount);
        
        private void InitSequence()
        {
            _weaponRecharge = DOTween.Sequence();
            _weaponRecharge.SetAutoKill(false);
            _weaponRecharge.Pause();

            float viewUpdateInterval = _attacker.ShootDelay < 0.01f ? _attacker.ShootDelay : 0.01f;
            
            _weaponRecharge.AppendInterval(viewUpdateInterval);
            _weaponRecharge.AppendCallback(() => _currentRechargeTime -= viewUpdateInterval);
            _weaponRecharge.AppendCallback(() => _view.UpdateGun(_currentRechargeTime / _attacker.ShootDelay));
            _weaponRecharge.AppendCallback(() =>
            {
                if (_currentRechargeTime / _attacker.ShootDelay > 0) 
                    _weaponRecharge.Restart();
            });
        }
    }
}