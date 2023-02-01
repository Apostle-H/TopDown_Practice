using UnityEngine;
using Until;

namespace EnemySystem
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private int radius;
        [SerializeField] private int rotateSpeed;
        [SerializeField] private int moveSpeed;
        [SerializeField] private LayerMask layer;
        
        private GameObject _playerPosition;
        private CircleCollider2D _collider2D;
        
        private void Start()
        {
            _collider2D = GetComponent<CircleCollider2D>();
            _collider2D.radius = radius;
        }

        private void Update()
        {
            if (_playerPosition != null 
                && Vector2.Distance(transform.position, _playerPosition.transform.position) < radius)
            {
                TurningTowardsTheTarget();
                MovementTowardsTheTarget();
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (layer.Contains(col.gameObject.layer))
            {
                _playerPosition = col.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (layer.Contains(col.gameObject.layer))
            {
                _playerPosition = null;
            }
        }

        private void MovementTowardsTheTarget() => transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // Движение к цели
        
        private void TurningTowardsTheTarget() // Поворот к цели
        {
            var direction = _playerPosition.transform.position - transform.position;
            transform.right = Vector2.Lerp(transform.right, direction, rotateSpeed * Time.deltaTime);
        }
    }
}