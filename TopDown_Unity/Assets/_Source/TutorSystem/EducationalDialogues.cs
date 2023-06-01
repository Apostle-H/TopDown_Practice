using System.Collections;
using TMPro;
using UnityEngine;
using Utils;

namespace TutorSystem
{
    public class EducationalDialogues : MonoBehaviour
    {
        [SerializeField] private GameObject dialog;
        [SerializeField] private TMP_Text textObject;
        [SerializeField] private string text;
        [SerializeField] private float timeDialog;
        [SerializeField] private LayerMask player;
        [SerializeField] private AudioSource source;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                dialog.SetActive(true);
                source.Play();
                textObject.text = text;
                StartCoroutine(EndDialog());
            }
        }

        private IEnumerator EndDialog()
        {
            yield return new WaitForSeconds(timeDialog);
            dialog.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}