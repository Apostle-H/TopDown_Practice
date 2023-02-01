using System;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private int distance;
        [SerializeField] private int rotateSpeed;
        [SerializeField] private int moveSpeed;
        
        private Transform _playerPosition;
        
        private void OnEnable()
        {
            // TestPlayer.playerPosition += GettingPlayerPosition;
        }

        private void OnDisable()
        {
            // TestPlayer.playerPosition -= GettingPlayerPosition;
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, _playerPosition.position) < distance)
            {
                TurningTowardsTheTarget();
                MovementTowardsTheTarget();
            }
        }

        private void GettingPlayerPosition(Transform playerPosition) => _playerPosition = playerPosition; // Враг получает позицию игрока
        
        private void MovementTowardsTheTarget() => transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // Движение к цели
        
        private void TurningTowardsTheTarget() // Поворот к цели
        {
            var direction = _playerPosition.position - transform.position;
            transform.right = Vector2.Lerp(transform.right, direction, rotateSpeed * Time.deltaTime);
        }
    }
}