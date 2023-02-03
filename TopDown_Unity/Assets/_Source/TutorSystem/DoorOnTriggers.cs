using System.Collections.Generic;
using UnityEngine;

namespace TutorSystem
{
    public class DoorOnTriggers : MonoBehaviour
    {
        [SerializeField] private Trigger[] buttonsToOpen;

        private int _counter;

        private void Awake()
        {
            _counter = buttonsToOpen.Length;

            for (int i = 0; i < buttonsToOpen.Length; i++)
            {
                buttonsToOpen[i].OnPressed += CountDown;
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