using System;
using UnityEngine;
using Utils;
using Utils.Events;

namespace StorySystem
{
    public class Note : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private GameObject hint;
        [SerializeField] private LayerMask player;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Signals.Get<ReadNoteSignal>().Dispatch(id);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                hint.SetActive(true);
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