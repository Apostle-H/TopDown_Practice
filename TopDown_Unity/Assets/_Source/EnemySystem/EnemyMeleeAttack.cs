using System;
using System.Linq;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyMeleeAttack : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float attackPause; // Неизменаеммое время паузы между аттаками
        [SerializeField] private LayerMask layer;

        private float _attackPause; // Изменаеммое время паузы между аттаками

        private void Update()
        {
            if (_attackPause > 0)
            {
                _attackPause -= Time.deltaTime;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if ((layer.value & (1 << collision.gameObject.layer)) != 0
                && _attackPause <= 0)
            {
                Attack();
            }
        }

        private void Attack()
        {
            Debug.Log($"Урон = {damage}");
            
            _attackPause = attackPause;
        }

        // private bool CheckAttackPause() // Проверка может враг атакавать или нет
        // {
        //     return _attackPause <= 0;
        // }
    }
}
