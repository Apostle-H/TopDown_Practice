using EnemySystem.Health;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnemySystem.EnemyAcne
{
    public class AcneEnemy : MonoBehaviour
    {
        [SerializeField] private EnemyInteractions interactions;
        [SerializeField] private EnemyInteractions[] enemyCount;
        [SerializeField] private CircleCollider2D invincibleCollider;

        private int _enemyCount;
        
        private void Awake()
        {
            _enemyCount = enemyCount.Length;
            
            interactions.OnKnock += Die;
            
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
            
            interactions.OnKnock -= Die;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
