using UnityEngine;
using Utils;
using Utils.Events;

namespace UI.Note
{
    public class NoteUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] notes;
        [SerializeField] private AudioSource source;
        
        private void OnEnable()
        {
            Signals.Get<ReadNoteSignal>().AddListener(ReadNote);
        }

        private void OnDisable()
        {
            Signals.Get<ReadNoteSignal>().RemoveListener(ReadNote);
        }

        private void ReadNote(int id)
        {
            source.Play();
            notes[id - 1].SetActive(true);
        }
    }
}