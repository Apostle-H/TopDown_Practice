using System.Collections;
using EnemySystem.Data;
using UnityEngine;
using Until;

namespace EnemySystem.EnemyDistant
{
    public class EnemyDistant : MonoBehaviour
    {
        [SerializeField] private Transform spawnProjectilePoint;
        [SerializeField] private EnemyDistantCharacteristicsSO enemyDistantCharacteristicsSO;

        private GameObject _target;
        private CircleCollider2D _collider2D;
        private Attack _attack;
        private bool _isTargetInRadius;

        private void Start()
        {
            _collider2D = GetComponent<CircleCollider2D>();
            _attack = new Attack();
            _collider2D.radius = enemyDistantCharacteristicsSO.RadiusAttack;
        }

        private void Update()
        {
            if (_isTargetInRadius)
            {
                TurningTowardsTheTarget();
            }
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (enemyDistantCharacteristicsSO.Layer.Contains(col.gameObject.layer))
            {
                _target = col.gameObject;

                _isTargetInRadius = true;
                
                StartCoroutine(Timer());
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (enemyDistantCharacteristicsSO.Layer.Contains(col.gameObject.layer))
            {
                _target = null;

                _isTargetInRadius = false;
                
                StopAllCoroutines();
            }
        }
        
        private void TurningTowardsTheTarget() => transform.up = _target.transform.position - transform.position;

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(enemyDistantCharacteristicsSO.DelayAttack);
            _attack.MakeAttack(enemyDistantCharacteristicsSO.Projectile, spawnProjectilePoint, enemyDistantCharacteristicsSO.Damage);
            
            StartCoroutine(Timer());
        }
    }
}