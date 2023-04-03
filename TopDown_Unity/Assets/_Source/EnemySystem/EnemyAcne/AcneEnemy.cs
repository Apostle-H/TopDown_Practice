using EnemySystem.Health;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnemySystem.EnemyAcne
{
    public class AcneEnemy : MonoBehaviour
    {
        [SerializeField] private EnemyInteractions interactions;
        [SerializeField] private int amountResourceToRemoveShield;

        private void Die()
        {
            gameObject.SetActive(false);
            
            interactions.OnKnock -= Die;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void CheckResourceCount(int resourceCount)
        {
            if (resourceCount >= amountResourceToRemoveShield)
            {
                interactions.OnKnock += Die;
            }
        }
    }
}
