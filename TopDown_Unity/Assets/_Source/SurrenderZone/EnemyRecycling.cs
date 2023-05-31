using System;
using EntitySystem.Interactions;
using UnityEngine;
using Utils;

namespace SurrenderZone
{
    public class EnemyRecycling : MonoBehaviour
    {
        public event Action<int> OnEnemyRecycled;
        
        [SerializeField] private LayerMask enemy;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (enemy.Contains(other.gameObject.layer) &&
                other.gameObject.TryGetComponent(out ISplittable target) &&
                target.IsSplittable)
            {
                OnEnemyRecycled?.Invoke(target.Worth);
                other.gameObject.SetActive(false);
            }
        }
    }
}
