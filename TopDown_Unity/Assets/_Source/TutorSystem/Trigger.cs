using System;
using UnityEngine;
using Utils;

namespace TutorSystem
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;

        public event Action OnPressed;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (layerMask.Contains(col.gameObject.layer))
            {
                OnPressed?.Invoke();
                
                gameObject.SetActive(false);
            }
        }
    }
}
