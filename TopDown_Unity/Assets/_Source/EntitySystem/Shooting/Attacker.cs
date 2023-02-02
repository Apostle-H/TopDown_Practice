using DG.Tweening;
using EntitySystem.Data.Combat;
using EntitySystem.Shooting.Projectiles;
using UnityEngine;

namespace EntitySystem.Shooting
{
    public class Attacker
    {
        private AttackerSettingsSO _settingsSO;
        protected Transform _firePoint;

        private ProjectilePool _pool;

        private float _rotation;
        
        private Sequence _shootDelayer;
        private bool _canShoot;

        public Attacker(Transform firePoint, ProjectilePool pool, AttackerSettingsSO settingsSO)
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