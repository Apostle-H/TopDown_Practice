using DG.Tweening;
using EntitySystem.Shooting;
using PlayerSystem;
using PlayerSystem.Consumables;

namespace UI.Player
{
    public class PlayerUIController
    {
        private readonly PlayerUIModel _model;
        private readonly PlayerUIView _view;

        private readonly PlayerHealth _health;
        private readonly Attacker _attacker;
        private readonly HookShooter _hookShooter;
        private readonly PlayerResources _resources;
        private readonly Patch _patch;
        private readonly Shield _shield;

        private Sequence _weaponRecharge;

        private float _currentRechargeTime;

        public PlayerUIController(PlayerUIModel model, PlayerUIView view, PlayerHealth health, Attacker attacker,
            HookShooter hookShooter, PlayerResources resources, Patch patch, Shield shield)
        {
            _model = model;
            _view = view;
            _health = health;
            _attacker = attacker;
            _hookShooter = hookShooter;
            _resources = resources;
            _patch = patch;
            _shield = shield;

            InitSequence();
            // _view.SetConsumablesKeys(model.PatchKey, model.ShieldKey);

            _health.OnDamaged += UpdateHealth;
            _health.OnHeal += UpdateHealth;
            
            _attacker.OnShoot += StartWeaponRecharge;
            _hookShooter.OnHookOut += WeaponDisabled;
            _hookShooter.OnHookIn += WeaponEnabled;

            _resources.OnAdded += UpdateResources;
            _resources.OnConsumed += UpdateResources;
            
            // _patch.OnCrafted += UpdatePatchCrafted;
            // _patch.OnUsed += UpdatePatchCrafted;
            // _shield.OnCrafted += UpdateShieldCrated;
            // _shield.OnUsed += UpdateShieldCrated;
            
            UpdateResources();
            // UpdatePatchCrafted();
            // UpdateShieldCrated();
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
        
        // private void UpdatePatchCrafted() =>
        //     _view.UpdatePatchCrafted(_patch.Crafted);
        
        // private void UpdateShieldCrated() =>
        //     _view.UpdateShieldCrafted(_shield.Crafted);

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