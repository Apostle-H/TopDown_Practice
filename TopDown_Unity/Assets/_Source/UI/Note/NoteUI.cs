using TMPro;
using UnityEngine;
using Utils;
using Utils.Events;

namespace UI.Note
{
    public class NoteUI : MonoBehaviour
    {
        [SerializeField] private GameObject note;
        [SerializeField] private TMP_Text text;
        [SerializeField] private AudioSource source;
        
        private void OnEnable()
        {
            Signals.Get<ReadNoteSignal>().AddListener(ReadNote);
        }

        private void OnDisable()
        {
            Signals.Get<ReadNoteSignal>().RemoveListener(ReadNote);
        }

        private void ReadNote(string textNote)
        {
            source.Play();
            text.text = textNote;
            note.SetActive(true);
        }
    }
}