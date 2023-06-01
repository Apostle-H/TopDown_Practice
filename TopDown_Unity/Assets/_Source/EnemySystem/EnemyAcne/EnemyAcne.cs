using System.Collections;
using EnemySystem.Data.Combat;
using EnemySystem.Health;
using EntitySystem.Shooting;
using EntitySystem.Shooting.Projectiles;
using UnityEngine;
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
        [SerializeField] private AcneRangeAttackerSO firstAttackerSO;
        [SerializeField] private AcneRangeAttackerSO secondAttackerSO;
        [SerializeField] private float timeBeforeDeath;
        [SerializeField] private AudioSource source;
        
        private Attacker _attackerFirst;
        private Attacker _attackerSecond;

        private Attacker _currentAttacker;
        private AcneRangeAttackerSO _currentAttackerSO;

        private int _attackCounter;

        private void Awake()
        {
            Init();

            _currentAttacker = _attackerFirst;
            _currentAttackerSO = firstAttackerSO;
        }

        private void Dead()
        {
            interactions.OnKnock -= Dead;
            
            _attackerFirst.StopShoot();
            _attackerSecond.StopShoot();
            _currentAttacker.StopShoot();
            
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
            _currentAttacker.StartShoot();
            _currentAttacker.OnShoot += CountAttack;
            StartCoroutine(Rotation());
        }

        private void CountAttack()
        {
            _attackCounter++;

            switch (_attackCounter)
            {
                case > 5 when _currentAttacker == _attackerFirst:
                    ChangeAttack(_attackerSecond, secondAttackerSO);
                    _attackCounter = 0;
                    Debug.Log(1);
                    break;
                case > 2 when _currentAttacker == _attackerSecond:
                    ChangeAttack(_attackerFirst, firstAttackerSO);
                    _attackCounter = 0;
                    Debug.Log(2);
                    break;
            }
        }

        private void ChangeAttack(Attacker newAttacker, AcneRangeAttackerSO newAttackerSO)
        {
            _currentAttacker.OnShoot -= CountAttack;
            _currentAttacker.StopShoot();
            _currentAttacker = newAttacker;
            _currentAttacker.OnShoot += CountAttack;
            _currentAttacker.StartShoot();

            _currentAttackerSO = newAttackerSO;
        }

        private IEnumerator Rotation()
        {
            while (true)
            {
                yield return new WaitForSeconds(Time.fixedDeltaTime);
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + _currentAttackerSO.RotationInBattle);
            }
        }

        private void Init()
        {
            ProjectilePool firstPool = new ProjectilePool(firstAttackerSO.ShootDelay, firstAttackerSO.ProjectilePrefab, projectilesHolder, firstAttackerSO.CountAttackProjectile);
            _attackerFirst = new Attacker(firePoint, firstPool, firstAttackerSO, source, firstAttackerSO.CountAttackProjectile, firstAttackerSO.RotationForProjectileAxisZ);
            
            ProjectilePool secondPool = new ProjectilePool(secondAttackerSO.ShootDelay, secondAttackerSO.ProjectilePrefab, projectilesHolder, secondAttackerSO.CountAttackProjectile);
            _attackerSecond = new Attacker(firePoint, secondPool, secondAttackerSO, source, secondAttackerSO.CountAttackProjectile, secondAttackerSO.RotationForProjectileAxisZ);
        }

        public void CheckResourceCount(int resourceCount)
        {
            if (resourceCount < firstAttackerSO.AmountResourceToRemoveShield) 
                return;
            
            barrier.SetActive(false);
            interactions.CanGetDamage = true;
            interactions.OnDamaged += GetDamage;
            interactions.OnKnock += Dead;
        }
    }
}
