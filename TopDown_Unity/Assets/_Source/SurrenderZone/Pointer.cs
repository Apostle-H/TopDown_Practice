using System.Collections;
using PlayerSystem.Invokers;
using UnityEngine;
using Utils;

namespace SurrenderZone
{
    public class Pointer : MonoBehaviour
    {
        private Transform _target;

        private void OnEnable()
        {
            StartCoroutine(UpdateRotate());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator UpdateRotate()
        {
            transform.rotation = transform.LookAt2D(_target.transform.position);
            
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            StartCoroutine(UpdateRotate());
        }

        private void Active()
        {
            gameObject.SetActive(true);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }

        public void SetPositionTargetAndEvent(PlayerHookInvoker playerHookInvoker, Transform target)
        {
            playerHookInvoker.OnHooked += Active;
            playerHookInvoker.OnReleased += Disable;
            
            _target = target;
        }
    }
}
