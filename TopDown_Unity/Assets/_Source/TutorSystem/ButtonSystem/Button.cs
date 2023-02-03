using System;
using UnityEngine;
using Until;

namespace TutorSystem.ButtonSystem
{
    public class Button : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;

        // public static event Action<GameObject> OnGotOnButton;
        public static event Action OnGotOnButton;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (layerMask.Contains(col.gameObject.layer))
            {
                OnGotOnButton?.Invoke();
                
                gameObject.SetActive(false);
            }
        }
    }
}
