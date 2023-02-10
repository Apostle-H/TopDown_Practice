using EnemySystem.Health;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnemySystem.EnemyAcne
{
    public class AcneEnemy : MonoBehaviour
    {
        [SerializeField] private EnemyHealth health;
        [SerializeField] private EnemyHealth[] enemyCount;
        [SerializeField] private CircleCollider2D invincibleCollider;

        private int _enemyCount;
        
        private void Awake()
        {
            _enemyCount = enemyCount.Length;
            
            health.OnKnock += Die;
            
            for (int i = 0; i < enemyCount.Length; i++)
            {
                enemyCount[i].OnKnock += CheckCountEnemy;
            }
        }

        private void CheckCountEnemy()
        {
            _enemyCount--;

            if (_enemyCount <= 0)
            {
                invincibleCollider.enabled = false;
            }
        }
        
        private void Die()
        {
            gameObject.SetActive(false);
            
            health.OnKnock -= Die;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
