using System.Collections;
using EnemySystem.Data;
using UnityEngine;
using Utils;

namespace EnemySystem.EnemyRange
{
    public class EnemyRange : MonoBehaviour
    {
        [SerializeField] private Transform spawnProjectilePoint;
        [SerializeField] private EnemyRangeCharacteristicsSO enemyRangeCharacteristicsSO;

        private GameObject _target;
        private CircleCollider2D _collider2D;
        private Attack _attack;
        private bool _isTargetInRadius;

        private void Start()
        {
            _collider2D = GetComponent<CircleCollider2D>();
            _attack = new Attack();
            _collider2D.radius = enemyRangeCharacteristicsSO.RadiusAttack;
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
            if (enemyRangeCharacteristicsSO.Layer.Contains(col.gameObject.layer))
            {
                _target = col.gameObject;

                _isTargetInRadius = true;
                
                StartCoroutine(Timer());
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (enemyRangeCharacteristicsSO.Layer.Contains(col.gameObject.layer))
            {
                _target = null;

                _isTargetInRadius = false;
                
                StopAllCoroutines();
            }
        }
        
        private void TurningTowardsTheTarget() // Поворот к цели
        {
            var direction = _target.transform.position - transform.position;
            transform.up = Vector2.Lerp(transform.up, direction, enemyRangeCharacteristicsSO.RotateSpeed * Time.deltaTime);
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(enemyRangeCharacteristicsSO.DelayAttack);
            _attack.MakeAttack(enemyRangeCharacteristicsSO.Projectile, spawnProjectilePoint, enemyRangeCharacteristicsSO.Damage);
            
            StartCoroutine(Timer());
        }
    }
}