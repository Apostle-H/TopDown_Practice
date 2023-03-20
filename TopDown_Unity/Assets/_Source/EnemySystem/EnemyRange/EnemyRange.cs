using EnemySystem.Data;
using EnemySystem.Data.Combat;
using EnemySystem.Health;
using EntitySystem.Shooting;
using EntitySystem.Shooting.Projectiles;
using UnityEngine;
using Utils;

namespace EnemySystem.EnemyRange
{
    public class EnemyRange : MonoBehaviour
    {
        [SerializeField] private EnemyInteractions interactions;
        [SerializeField] private EnemyStaticSO enemyStaticSO;
        [SerializeField] private CircleCollider2D rangeCollider;
        [SerializeField] private Transform projectilesHolder;
        [SerializeField] private Transform firePoint;
        [SerializeField] private StaticRangeAttackerSO attackerSO;
        [SerializeField] private Rigidbody2D rb;

        private CircleCollider2D _collider2D;

        private Attacker _attacker;
        
        private bool _isTargetInRadius;
        private GameObject _target;

        private void Awake()
        {
            rangeCollider.radius = attackerSO.AttackRange;
            
            ProjectilePool pool = new ProjectilePool(attackerSO.ShootDelay, attackerSO.ProjectilePrefab, projectilesHolder);
            _attacker = new Attacker(firePoint, pool, attackerSO);

            interactions.OnKnock += Knock;
            interactions.OnDeath += Die;
        }

        private void Update()
        {
            if (_isTargetInRadius)
            {
                LookAtTarget();
            }
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (enemyStaticSO.TargetMask.Contains(col.gameObject.layer))
            {
                _target = col.gameObject;
                _isTargetInRadius = true;
                
                LookAtTarget();
                _attacker.StartShoot();
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (enemyStaticSO.TargetMask.Contains(col.gameObject.layer))
            {
                _target = null;
                _isTargetInRadius = false;
                
                _attacker.StopShoot();
            }
        }

        private void Knock()
        {
            rangeCollider.enabled = false;
            StopAllCoroutines();
            rb.bodyType = RigidbodyType2D.Dynamic;
            
            interactions.OnKnock -= Knock;
        }
        
        private void Die()
        {
            gameObject.SetActive(false);
            
            interactions.OnKnock -= Die;
        }

        private void LookAtTarget()
        {
            transform.rotation = transform.LookAt2D(_target.transform.position);
        }
    }
}