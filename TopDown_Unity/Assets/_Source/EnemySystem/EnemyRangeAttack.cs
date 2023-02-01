using UnityEngine;

namespace EnemySystem
{
    public class EnemyRangeAttack : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private int distanceAttack;
        [SerializeField] private float attackPause; // Неизменаеммое время паузы между аттаками
        [SerializeField] private Transform lineStartPoint; // начальная точка откуда идёт луч для Raycast
        [SerializeField] private LayerMask layer;

        private float _attackPause; // Изменаеммое время паузы между аттаками

        private void Update()
        {
            if (_attackPause > 0)
            {
                _attackPause -= Time.deltaTime;
            }
            else if (BeamTouchCheck())
            {
                Attack();
            }
        }

        private bool BeamTouchCheck() // Проверяет, что задел луч, если игрок, то вернёт True, иначе False
        {
            RaycastHit2D hit = Physics2D.Raycast(lineStartPoint.position, lineStartPoint.TransformDirection(Vector3.right), distanceAttack);
            
            if ((layer.value & (1 << hit.collider.gameObject.layer)) != 0)
            {
                return true;
            }
            
            return false;
        }

        private void Attack()
        {
            Debug.Log($"Вижу цель и атакую с уроно {damage}");
            
            _attackPause = attackPause;
        }
    }
}