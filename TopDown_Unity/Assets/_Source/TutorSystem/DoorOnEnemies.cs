using EnemySystem.Health;
using EntitySystem.Health;
using UnityEngine;

namespace TutorSystem
{
    public class DoorOnEnemies : MonoBehaviour
    {
        [SerializeField] private EnemyHealth[] damageableToOpen;

        private int _counter;

        private void Awake()
        {
            _counter = damageableToOpen.Length;

            for (int i = 0; i < damageableToOpen.Length; i++)
            {
                damageableToOpen[i].OnDeath += CountDown;
            } 
        }

        private void CountDown()
        {
            _counter--;
            
            if (_counter <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}