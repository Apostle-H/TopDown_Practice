using UnityEngine;

namespace TutorSystem
{
    public class DoorOnTriggers : MonoBehaviour
    {
        [SerializeField] private AudioSource open;
        [SerializeField] private Trigger[] buttonsToOpen;

        private int _counter;

        private void Awake()
        {
            _counter = buttonsToOpen.Length;

            foreach (var trigger in buttonsToOpen)
            {
                trigger.OnPressed += CountDown;
            }
        }

        private void CountDown()
        {
            _counter--;
            
            if (_counter <= 0)
            {
                gameObject.SetActive(false);
                open.Play();
            }
        }
    }
}