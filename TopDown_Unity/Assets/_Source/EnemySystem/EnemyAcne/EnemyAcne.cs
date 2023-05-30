using System.Collections;
using EnemySystem.Data.Combat;
using EnemySystem.Health;
using EntitySystem.Shooting;
using EntitySystem.Shooting.Projectiles;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnemySystem.EnemyAcne
{
    public class EnemyAcne : MonoBehaviour
    {
        [SerializeField] private EnemyInteractions interactions;
        [SerializeField] private GameObject barrier;
        [SerializeField] private Transform projectilesHolder;
        [SerializeField] private Transform firePoint;
        [SerializeField] private AcneRangeAttackerSO attackerSO;
        [SerializeField] private float rotationInBattle;
        [SerializeField] private int amountResourceToRemoveShield;
        [SerializeField] private int countAttackProjectile;
        [SerializeField] private float rotationForProjectileAxisZ;
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
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationInBattle);
            }
        }

        private void Init()
        {
            ProjectilePool pool = new ProjectilePool(attackerSO.ShootDelay, attackerSO.ProjectilePrefab, projectilesHolder, countAttackProjectile);
            
            _attacker = new Attacker(firePoint, pool, attackerSO, source, countAttackProjectile, rotationForProjectileAxisZ);
        }

        public void CheckResourceCount(int resourceCount)
        {
            if (resourceCount >= amountResourceToRemoveShield)
            {
                barrier.SetActive(false);
                interactions.CanGetDamage = true;
                interactions.OnDamaged += GetDamage;
                interactions.OnKnock += Dead;
            }
        }
    }
}
