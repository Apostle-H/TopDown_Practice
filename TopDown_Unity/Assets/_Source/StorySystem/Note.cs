using UnityEngine;
using Utils;
using Utils.Events;

namespace StorySystem
{
    public class Note : MonoBehaviour
    {
        [SerializeField] private string textNote;
        [SerializeField] private GameObject hint;
        [SerializeField] private LayerMask player;

        private bool _playerStay;

        private void Update()
        {
            if (_playerStay
                && Input.GetKeyDown(KeyCode.E))
            {
                Signals.Get<ReadNoteSignal>().Dispatch(textNote);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                _playerStay = true;
                hint.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                _playerStay = false;
                hint.SetActive(false);
            }
        }
    }
}