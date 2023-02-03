using System.Collections.Generic;
using TutorSystem.ButtonSystem;
using UnityEngine;
// using EntitySystem.Health;
    
namespace TutorSystem.DoorSystem
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private List<GameObject> barriersToOpening;

        private int _countBarriersToOpening;

        private void Start()
        {
            _countBarriersToOpening = barriersToOpening.Count;
        }

        private void OnEnable()
        {
            Button.OnGotOnButton += CheckCondition;
            // Damageable.OnDeath += CheckCondition;
        }

        private void OnDisable()
        {
            Button.OnGotOnButton -= CheckCondition;
            // Damageable.OnDeath -= CheckCondition;
        }

        private void CheckCondition()
        {
            _countBarriersToOpening--;
            
            if (_countBarriersToOpening <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}