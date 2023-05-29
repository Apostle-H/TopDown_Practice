using System;
using UnityEngine;
using Utils;
using Utils.Events;

namespace UI.Note
{
    public class NoteUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] notes;

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                for (int i = 0; i < notes.Length; i++)
                {
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
            notes[id - 1].SetActive(true);
        }
    }
}