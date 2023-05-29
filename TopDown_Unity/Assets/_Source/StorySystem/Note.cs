using System;
using UnityEngine;
using Utils;

namespace StorySystem
{
    public class Note : MonoBehaviour
    {
        [SerializeField] private GameObject hint;
        [SerializeField] private LayerMask player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                hint.SetActive(true);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Читаем записку");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                hint.SetActive(false);
            }
        }
    }
}