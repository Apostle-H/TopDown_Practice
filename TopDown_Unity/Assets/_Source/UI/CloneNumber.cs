using TMPro;
using UnityEngine;
using Utils;
using Utils.Events;

namespace UI
{
    public class CloneNumber : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        private int _cloneNumber = 1;
        
        private void Awake()
        {
            ChangeCloneNumber();

            Signals.Get<PlayerDeadSignal>().AddListener(CloneDie);
            
            GameObject[] objs = GameObject.FindGameObjectsWithTag("CloneNumber");

            if (objs.Length > 1)
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            Signals.Get<PlayerDeadSignal>().RemoveListener(CloneDie);
        }

        private void CloneDie()
        {
            _cloneNumber++;
            ChangeCloneNumber();
        }

        private void ChangeCloneNumber()
        {
            text.text = _cloneNumber.ToString();
        }
    }
}