﻿using System.Collections;
using EnemySystem.Data;
using EnemySystem.Data.Combat;
using EntitySystem.Health;
using EntitySystem.Shooting;
using EntitySystem.Shooting.Projectiles;
using UnityEngine;
using Utils;

namespace EnemySystem.EnemyRange
{
    public class EnemyRange : MonoBehaviour
    {
        [SerializeField] private Damageable damageable;
        [SerializeField] private EnemyRangeCharacteristicsSO enemyRangeCharacteristicsSO;
        [SerializeField] private CircleCollider2D rangeCollider;
        [SerializeField] private Transform projectilesHolder;
        [SerializeField] private Transform firePoint;
        [SerializeField] private RangeAttackerSettings attackerSettingsSO;

        private CircleCollider2D _collider2D;

        private Attacker _attacker;
        
        private bool _isTargetInRadius;
        private GameObject _target;

        private void Awake()
        {
            rangeCollider.radius = attackerSettingsSO.AttackRange;
            
            ProjectilePool pool = new ProjectilePool(attackerSettingsSO.ShootDelay, attackerSettingsSO.ProjectilePrefab, projectilesHolder);
            _attacker = new Attacker(firePoint, pool, attackerSettingsSO);

            damageable.OnDeath += Die;
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
            if (enemyRangeCharacteristicsSO.TargetLayer.Contains(col.gameObject.layer))
            {
                _target = col.gameObject;

                _isTargetInRadius = true;
                
                StartCoroutine(Timer());
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (enemyRangeCharacteristicsSO.TargetLayer.Contains(col.gameObject.layer))
            {
                _target = null;

                _isTargetInRadius = false;
                
                StopAllCoroutines();
            }
        }
        
        private void Die()
        {
            rangeCollider.enabled = false;
            StopAllCoroutines();
        }

        private void TurningTowardsTheTarget()
        {
            transform.rotation = transform.LookAt2D(_target.transform.position);
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(attackerSettingsSO.ShootDelay);
            _attacker.Shoot();

            StartCoroutine(Timer());
        }
    }
}