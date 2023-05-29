using System.Collections.Generic;
using UnityEngine;

namespace EntitySystem.Shooting.Projectiles
{
    public class ProjectilePool
    {
        private readonly Queue<Projectile> _pool;

        public ProjectilePool(float shootDelay, GameObject projectilePrefab, Transform projectilesParent, int countAttackProjectile = 1)
        {
            _pool = new Queue<Projectile>();
            float projectileLifeTime = projectilePrefab.GetComponent<Projectile>().LifeTime;
            int neededProjectileCount = (1 + Mathf.CeilToInt(projectileLifeTime / shootDelay)) * countAttackProjectile;
            for (int i = 0; i < neededProjectileCount; i++)
            {
                Projectile projectile = Object.Instantiate(projectilePrefab, projectilesParent).GetComponent<Projectile>();
                projectile.gameObject.SetActive(false);
                projectile.OnDie += Return;
                
                _pool.Enqueue(projectile);
            }
        }

        public Projectile Get() =>
            _pool.Dequeue();

        public void Return(Projectile returnable) =>
            _pool.Enqueue(returnable);
    }
}