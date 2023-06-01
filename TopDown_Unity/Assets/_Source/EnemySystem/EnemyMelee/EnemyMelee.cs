using System.Collections;
using EnemySystem.Data;
using EnemySystem.Data.Combat;
using EnemySystem.Health;
using EnemySystem.Movement;
using EntitySystem.Shooting;
using EntitySystem.Shooting.Projectiles;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace EnemySystem.EnemyMelee
{
    public class EnemyMelee : MonoBehaviour
    {
        [SerializeField] private EnemyInteractions interactions;
        [SerializeField] private EnemyMeleeSO enemyMeleeSO;
        [SerializeField] private CircleCollider2D rangeCollider;
        [SerializeField] private Transform projectilesHolder;
        [SerializeField] private Transform firePoint;
        [SerializeField] private MeleeAttackerSO attackerSO;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private AudioSource source;

        private NavMeshAgent _navMesh;
        private NavMeshMover _navMeshMover;
        private Attacker _attacker;
        
        private GameObject _target;

        private void Awake()
        {
            _navMesh = GetComponent<NavMeshAgent>();
            _navMesh.updateRotation = false;
            _navMesh.updateUpAxis = false;
            
            _navMeshMover = new NavMeshMover(_navMesh, enemyMeleeSO.MoveSpeed);

            rangeCollider.radius = attackerSO.TriggerRange;
            
            ProjectilePool pool = new ProjectilePool(attackerSO.ShootDelay, attackerSO.ProjectilePrefab, projectilesHolder);
            _attacker = new Attacker(firePoint, pool, attackerSO, source);

            interactions.OnKnock += Knock;
            interactions.OnDeath += Die;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (enemyMeleeSO.TargetMask.Contains(col.gameObject.layer))
            {
                _target = col.gameObject;
                _navMeshMover.TargetFound(_target);
                
                StartCoroutine(CheckRange());
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (enemyMeleeSO.TargetMask.Contains(col.gameObject.layer))
            {
                _target = null;
                _navMeshMover.TargetLost();
                
                StopAllCoroutines();
            }
        }

        private void Knock()
        {
            rangeCollider.enabled = false;
            
            _attacker.StopShoot();
            StopAllCoroutines();
            
            rb.bodyType = RigidbodyType2D.Dynamic;
            
            interactions.OnKnock -= Knock;
        }

        private void Die()
        {
            gameObject.SetActive(false);
            
            interactions.OnDeath -= Die;
        }
        
        private IEnumerator CheckRange()
        {
            transform.rotation = transform.LookAt2D(_target.transform.position);

            if (Vector2.Distance(transform.position, _target.transform.position) < attackerSO.AttackRange)
            {
                _attacker.StartShoot();
                StartCoroutine(DelayAttack());
                
                yield break;
            }
                
            _attacker.StopShoot();
        
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            _navMeshMover.Move();
            StartCoroutine(CheckRange());
        }
        
        private IEnumerator DelayAttack()
        {
            yield return new WaitForSeconds(attackerSO.ShootDelay);
            StartCoroutine(CheckRange());
        }
    }
}
