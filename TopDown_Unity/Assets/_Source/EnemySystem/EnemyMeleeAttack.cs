using System.Collections;
using EnemySystem.Data;
using UnityEngine;
using Until;

namespace EnemySystem
{
    public class EnemyMeleeAttack : MonoBehaviour
    {
        [SerializeField] private EnemyCharacteristicsSO enemyCharacteristicsSO;

        private GameObject _target;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (enemyCharacteristicsSO.Layer.Contains(col.gameObject.layer))
            {
                _target = col.gameObject;
                
                StartCoroutine(CheckRange());
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (enemyCharacteristicsSO.Layer.Contains(col.gameObject.layer))
            {
                _target = null;
                StopAllCoroutines();
            }
        }

        private void Attack()
        {
            if (_target != null)
            {
                Debug.Log($"Урон = {enemyCharacteristicsSO.Damage}");
                
                StartCoroutine(DelayAttack());
            }
        }

        private IEnumerator CheckRange() // Проверяет растоянние между врагом и игроком
        {
            if (Vector2.Distance(transform.position, _target.transform.position) < enemyCharacteristicsSO.RadiusAttack)
            {
                Attack();
                
                yield break;
            }

            yield return new WaitForSeconds(Time.fixedDeltaTime);
            StartCoroutine(CheckRange());
        }
        
        private IEnumerator DelayAttack() // Ведёс отсчёт до следующей атаки
        {
            yield return new WaitForSeconds(enemyCharacteristicsSO.DelayAttack);
            StartCoroutine(CheckRange());
        }
    }
}
