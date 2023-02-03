using System.Collections;
using EnemySystem.Data;
using EnemySystem.Data.Combat;
using EnemySystem.Movement;
using EntitySystem.Data.Combat;
using EntitySystem.Shooting;
using EntitySystem.Shooting.Projectiles;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace EnemySystem.EnemyMelee
{
    public class EnemyMelee : MonoBehaviour
    {
        [SerializeField] private EnemyMeleeCharacteristicsSO enemyMeleeCharacteristicsSO;
        [SerializeField] private CircleCollider2D rangeCollider;
        [SerializeField] private Transform projectilesHolder;
        [SerializeField] private Transform firePoint;
        [SerializeField] private MeleeAttackerSettings attackerSettingsSO;

        private NavMeshAgent _navMesh;
        private NavMeshMover _navMeshMover;
        private Attacker _attacker;
        
        private GameObject _target;

        private void Awake()
        {
            _navMesh = GetComponent<NavMeshAgent>();
            _navMesh.updateRotation = false;
            _navMesh.updateUpAxis = false;
            
            _navMeshMover = new NavMeshMover(_navMesh, enemyMeleeCharacteristicsSO.MoveSpeed);

            rangeCollider.radius = attackerSettingsSO.TriggerRange;
            
            ProjectilePool pool = new ProjectilePool(attackerSettingsSO.ShootDelay, attackerSettingsSO.ProjectilePrefab, projectilesHolder); 
            _attacker = new Attacker(firePoint, pool, attackerSettingsSO);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (enemyMeleeCharacteristicsSO.TargetLayer.Contains(col.gameObject.layer))
            {
                _target = col.gameObject;
                _navMeshMover.TargetFound(_target);
                
                StartCoroutine(CheckRange());
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (enemyMeleeCharacteristicsSO.TargetLayer.Contains(col.gameObject.layer))
            {
                _target = null;
                _navMeshMover.TargetLost();
                
                StopAllCoroutines();
            }
        }
        
        private IEnumerator CheckRange()
        {
            transform.rotation = transform.LookAt2D(_target.transform.position);

            if (Vector2.Distance(transform.position, _target.transform.position) < attackerSettingsSO.AttackRange)
            {
                _attacker.Shoot();
                StartCoroutine(DelayAttack());
                
                yield break;
            }
        
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            _navMeshMover.Move();
            StartCoroutine(CheckRange());
        }
        
        private IEnumerator DelayAttack()
        {
            yield return new WaitForSeconds(attackerSettingsSO.ShootDelay);
            StartCoroutine(CheckRange());
        }
    }
}
