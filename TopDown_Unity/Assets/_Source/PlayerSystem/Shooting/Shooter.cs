using System.Collections;
using DG.Tweening;
using PlayerSystem.Data.Shooting;
using PlayerSystem.Shooting.Projectiles;
using UnityEngine;

namespace PlayerSystem.Shooting
{
    public class Shooter
    {
        private ShooterSettingsSO _settingsSO;
        private Transform _firePoint;
        private Transform _pivotPoint;

        private ProjectilePool _pool;

        private float _rotation;
        
        private Sequence _delayer;
        private bool _canShoot;

        public Shooter(Transform firePoint, Transform pivotPoint, ProjectilePool pool, ShooterSettingsSO settingsSO)
        {
            _firePoint = firePoint;
            _pivotPoint = pivotPoint;
            _pool = pool;
            _settingsSO = settingsSO;

            _canShoot = true;

            InitSequence();
        }

        public void Rotate(float rotation)
        {
            _pivotPoint.eulerAngles = new Vector3(0f, 0f, rotation);
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
            _delayer.Restart();
        }

        private void InitSequence()
        {
            _delayer = DOTween.Sequence();
            _delayer.Pause();
            _delayer.SetAutoKill(false);
            
            _delayer.AppendCallback(() => _canShoot = false);
            _delayer.AppendInterval(_settingsSO.ShootDelay);
            _delayer.AppendCallback(() => _canShoot = true);
        }
    }
}