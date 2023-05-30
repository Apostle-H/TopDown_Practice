using System;
using UnityEngine;
using Utils;
using Utils.Events;

namespace UI.Note
{
    public class NoteUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] notes;

        private bool _read;
        
        private void Update()
        {
            if (_read
                && Input.anyKeyDown)
            {
                for (int i = 0; i < notes.Length; i++)
                {
                    _read = false;
                    notes[i].SetActive(false);
                }
            }
        }

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
            Debug.Log(id);
            _read = true;
            // notes[id - 1].SetActive(true);
            notes[0].SetActive(true);
            notes[1].SetActive(true);
        }
    }
}