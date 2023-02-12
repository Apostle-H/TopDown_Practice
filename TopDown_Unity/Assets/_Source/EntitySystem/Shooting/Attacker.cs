using DG.Tweening;
using EntitySystem.Data.Combat;
using EntitySystem.Shooting.Projectiles;
using UnityEngine;

namespace EntitySystem.Shooting
{
    public class Attacker
    {
        private AttackerSO _so;
        protected Transform _firePoint;

        private ProjectilePool _pool;

        private float _rotation;
        
        private bool _shoot;
        private Sequence _shootDelayer;

        public Attacker(Transform firePoint, ProjectilePool pool, AttackerSO so)
        {
            _firePoint = firePoint;
            _pool = pool;
            _so = so;

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
            
            Projectile projectile = _pool.Get();
            projectile.gameObject.SetActive(true);
            projectile.transform.position = _firePoint.position;
            projectile.transform.rotation = _firePoint.rotation;
            
            projectile.ShootSelf();
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