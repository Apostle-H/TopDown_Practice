using System.Collections;
using EnemySystem.Data.Combat;
using EnemySystem.Health;
using EntitySystem.Shooting;
using EntitySystem.Shooting.Projectiles;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Utils.Events;

namespace EnemySystem.EnemyAcne
{
    public class EnemyAcne : MonoBehaviour
    {
        [SerializeField] private EnemyInteractions interactions;
        [SerializeField] private GameObject barrier;
        [SerializeField] private Transform projectilesHolder;
        [SerializeField] private Transform firePoint;
        [SerializeField] private AcneRangeAttackerSO attackerSO;
        [SerializeField] private float timeBeforeDeath;
        [SerializeField] private AudioSource source;
        
        private Attacker _attacker;

        private void Awake()
        {
            Init();
        }

        private void Dead()
        {
            interactions.OnKnock -= Dead;
            
            _attacker.StopShoot();
            
            StartCoroutine(Die());
        }

        private IEnumerator Die()
        {
            yield return new WaitForSeconds(timeBeforeDeath);
            
            gameObject.SetActive(false);

            Signals.Get<AllLevelEndSignal>().Dispatch();
        }

        private void GetDamage()
        {
            interactions.OnDamaged -= GetDamage;
            _attacker.StartShoot();
            StartCoroutine(Rotation());
        }

        private IEnumerator Rotation()
        {
            while (true)
            {
                yield return new WaitForSeconds(Time.fixedDeltaTime);
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + attackerSO.RotationInBattle);
            }
        }

        private void Init()
        {
            ProjectilePool pool = new ProjectilePool(attackerSO.ShootDelay, attackerSO.ProjectilePrefab, projectilesHolder, attackerSO.CountAttackProjectile);
            
            _attacker = new Attacker(firePoint, pool, attackerSO, source, attackerSO.CountAttackProjectile, attackerSO.RotationForProjectileAxisZ);
        }

        public void CheckResourceCount(int resourceCount)
        {
            if (resourceCount >= attackerSO.AmountResourceToRemoveShield)
            {
                barrier.SetActive(false);
                interactions.CanGetDamage = true;
                interactions.OnDamaged += GetDamage;
                interactions.OnKnock += Dead;
            }
        }
    }
}
