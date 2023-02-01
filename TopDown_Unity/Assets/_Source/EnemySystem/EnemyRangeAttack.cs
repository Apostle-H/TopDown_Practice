using System;
using System.Collections;
using EnemySystem.Data;
using UnityEngine;
using Until;

namespace EnemySystem
{
    public class EnemyRangeAttack : MonoBehaviour
    {
        [SerializeField] private EnemyCharacteristicsSO enemyCharacteristicsSO;

        private GameObject _target;
        private CircleCollider2D _collider2D;

        private void Start()
        {
            _collider2D = GetComponent<CircleCollider2D>();
            _collider2D.radius = enemyCharacteristicsSO.RadiusAttack;
        }

        private void Update()
        {
            if (_target != null 
                && Vector2.Distance(transform.position, _target.transform.position) < enemyCharacteristicsSO.RadiusAttack)
            {
                TurningTowardsTheTarget();
            }
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (enemyCharacteristicsSO.Layer.Contains(col.gameObject.layer))
            {
                _target = col.gameObject;
                
                StartCoroutine(Timer());
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (enemyCharacteristicsSO.Layer.Contains(col.gameObject.layer))
            {
                _target = null;
            }
        }
        
        private void TurningTowardsTheTarget() // Поворот к цели
        {
            var direction = _target.transform.position - transform.position;
            transform.right = Vector2.Lerp(transform.right, direction, enemyCharacteristicsSO.RotateSpeed * Time.deltaTime);
        }

        private void Attack()
        {
            if (_target != null)
            {
                Debug.Log($"Урон = {enemyCharacteristicsSO.Damage} по {_target.name}");

                StartCoroutine(Timer());
            }
        }
        
        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(enemyCharacteristicsSO.DelayAttack);
            Attack();
        }
    }
}