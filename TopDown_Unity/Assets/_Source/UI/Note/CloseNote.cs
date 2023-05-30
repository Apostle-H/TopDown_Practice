using UnityEngine;

namespace UI.Note
{
    public class CloseNote : MonoBehaviour
    {
        private void Update()
        {
            if (Input.anyKeyDown)
            {
                gameObject.SetActive(false);
            }
        }
    }
}