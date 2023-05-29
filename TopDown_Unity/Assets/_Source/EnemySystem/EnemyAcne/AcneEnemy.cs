using System;
using System.Collections;
using EnemySystem.Data.Combat;
using EnemySystem.Health;
using EntitySystem.Shooting;
using EntitySystem.Shooting.Projectiles;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnemySystem.EnemyAcne
{
    public class AcneEnemy : MonoBehaviour
    {
        [SerializeField] private EnemyInteractions interactions;
        [SerializeField] private GameObject barrier;
        [SerializeField] private Transform projectilesHolder;
        [SerializeField] private Transform firePoint;
        [SerializeField] private AcneRangeAttackerSO attackerSO;
        [SerializeField] private int amountResourceToRemoveShield;
        
        private Attacker _attackers;

        private void Awake()
        {
            Init();
        }

        private void Die()
        {
            gameObject.SetActive(false);
            
            interactions.OnKnock -= Die;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void GetDamage()
        {
            interactions.OnDamaged -= GetDamage;
            _attackers.StartShoot();
            StartCoroutine(Rotation());
        }

        private IEnumerator Rotation()
        {
            while (true)
            {
                yield return new WaitForSeconds(Time.fixedDeltaTime);
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 0.5f);
            }
        }

        private void Init()
        {
            ProjectilePool pool = new ProjectilePool(attackerSO.ShootDelay, attackerSO.ProjectilePrefab, projectilesHolder, 12);
            
            _attackers = new Attacker(firePoint, pool, attackerSO, 12, 30);
        }

        public void CheckResourceCount(int resourceCount)
        {
            if (resourceCount >= amountResourceToRemoveShield)
            {
                barrier.SetActive(false);
                interactions.OnDamaged += GetDamage;
                interactions.OnKnock += Die;
            }
        }
    }
}
