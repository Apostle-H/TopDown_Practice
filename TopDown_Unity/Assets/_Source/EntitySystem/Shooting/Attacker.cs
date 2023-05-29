using System;
using DG.Tweening;
using EntitySystem.Data.Combat;
using EntitySystem.Shooting.Projectiles;
using UnityEngine;

namespace EntitySystem.Shooting
{
    public class Attacker
    {
        private readonly AttackerSO _so;
        protected readonly Transform firePoint;
        private readonly int _countFireProjectile;
        private readonly float _rotationProjectileStatic;

        private readonly ProjectilePool _pool;

        private float _rotationProjectile;

        private bool _shoot;
        private Sequence _shootDelayer;

        public float ShootDelay => _so.ShootDelay;
        
        public event Action OnShoot;

        public Attacker(Transform firePoint, ProjectilePool pool, AttackerSO so, int countFireProjectile = 1, float rotationProjectileStatic = 0)
        {
            this.firePoint = firePoint;
            _pool = pool;
            _so = so;
            _countFireProjectile = countFireProjectile;
            _rotationProjectile = rotationProjectileStatic;
            _rotationProjectileStatic = rotationProjectileStatic;

            InitSequence();
        }

        public void StartShoot()
        {
            _shoot = true;
            if (_shootDelayer.IsPlaying())
            {
                return;
            }
            
            _shootDelayer.Restart();
        }

        private void Shoot()
        {
            if (!_shoot)
            {
                return;
            }

            var point = firePoint.rotation.eulerAngles;
            
            for (int i = 0; i < _countFireProjectile; i++)
            {
                Projectile projectile = _pool.Get();
                projectile.gameObject.SetActive(true);
                projectile.transform.position = firePoint.position;
                projectile.transform.rotation = Quaternion.Euler(point.x, point.y, point.z + _rotationProjectile);
                _rotationProjectile += _rotationProjectileStatic;
                
                projectile.ShootSelf();
                OnShoot?.Invoke();
            }

            _rotationProjectile = firePoint.rotation.z;
        }

        public void StopShoot()
        {
            _shoot = false;
        }

        private void InitSequence()
        {
            _shootDelayer = DOTween.Sequence();
            _shootDelayer.Pause();
            _shootDelayer.SetAutoKill(false);
            
            _shootDelayer.AppendCallback(Shoot);
            _shootDelayer.AppendInterval(_so.ShootDelay);
            _shootDelayer.AppendCallback(() => { if (_shoot) _shootDelayer.Restart(); });
        }
    }
}