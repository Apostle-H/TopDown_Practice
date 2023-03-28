using EntitySystem.Interactions;
using UnityEngine;
using Utils;

namespace SurrenderZone
{
    public class EnemyRecycling : MonoBehaviour
    {
        [SerializeField] private LayerMask enemy;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (enemy.Contains(other.gameObject.layer) &&
                other.gameObject.TryGetComponent(out IDraggable target) &&
                target.IsDraggable)
            {
                Debug.Log("Resource + 1");
                other.gameObject.SetActive(false);
            }
        }
    }
}
