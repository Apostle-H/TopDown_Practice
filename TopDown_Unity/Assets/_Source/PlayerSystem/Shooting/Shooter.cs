using DG.Tweening;
using PlayerSystem.Data.Shooting;
using PlayerSystem.Shooting.Projectiles;
using UnityEngine;

namespace PlayerSystem.Shooting
{
    public class Shooter
    {
        private ShooterSettingsSO _settingsSO;
        protected Transform _firePoint;

        private ProjectilePool _pool;

        private float _rotation;
        
        private Sequence _shootDelayer;
        private bool _canShoot;

        public Shooter(Transform firePoint, ProjectilePool pool, ShooterSettingsSO settingsSO)
        {
            _firePoint = firePoint;
            _pool = pool;
            _settingsSO = settingsSO;

            _canShoot = true;

            InitSequence();
        }

        public void Shoot()
        {
            if (!_canShoot)
            {
                return;
            }
            
            Projectile projectile = _pool.Get();
            projectile.gameObject.SetActive(true);
            projectile.transform.position = _firePoint.position;
            projectile.transform.rotation = _firePoint.rotation;
            
            projectile.ShootSelf();
            _shootDelayer.Restart();
        }

        private void InitSequence()
        {
            _shootDelayer = DOTween.Sequence();
            _shootDelayer.Pause();
            _shootDelayer.SetAutoKill(false);
            
            _shootDelayer.AppendCallback(() => _canShoot = false);
            _shootDelayer.AppendInterval(_settingsSO.ShootDelay);
            _shootDelayer.AppendCallback(() => _canShoot = true);
        }
    }
}