using UnityEngine;
using Utils;

namespace TutorSystem
{
    public class EducationalDialogues : MonoBehaviour
    {
        [SerializeField] private GameObject dialog;
        [SerializeField] private LayerMask player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                dialog.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                dialog.SetActive(false);
            }
        }
    }
}