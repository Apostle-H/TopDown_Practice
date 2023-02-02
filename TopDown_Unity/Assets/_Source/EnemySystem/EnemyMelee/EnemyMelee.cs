using System.Collections;
using EnemySystem.Data;
using UnityEngine;
using UnityEngine.AI;
using Until;

namespace EnemySystem.EnemyMelee
{
    public class EnemyMelee : MonoBehaviour
    {
        [SerializeField] private EnemyMeleeCharacteristicsSO enemyMeleeCharacteristicsSO;

        private CircleCollider2D _collider2D;
        private Attack _attack;
        private NavMeshAgent _navMesh;
        private Movement _movement;
        private GameObject _target;

        private void Start()
        {
            _collider2D = GetComponent<CircleCollider2D>();
            _collider2D.radius = enemyMeleeCharacteristicsSO.RadiusAttack;
            
            _attack = new Attack();

            _navMesh = GetComponent<NavMeshAgent>();
            _navMesh.updateRotation = false;
            _navMesh.updateUpAxis = false;
            
            _movement = new Movement(_navMesh, enemyMeleeCharacteristicsSO.MoveSpeed);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (enemyMeleeCharacteristicsSO.Layer.Contains(col.gameObject.layer))
            {
                _target = col.gameObject;
                _movement.TargetFound(_target);
                
                StartCoroutine(CheckRange());
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (enemyMeleeCharacteristicsSO.Layer.Contains(col.gameObject.layer))
            {
                _target = null;
                _movement.TargetLost();
                
                StopAllCoroutines();
            }
        }
        
        private IEnumerator CheckRange() // Проверяет растоянние между врагом и игроком
        {
            transform.up = Vector2.Lerp(transform.up, _target.transform.position - transform.position, enemyMeleeCharacteristicsSO.RotateSpeed * Time.deltaTime);
            
            if (Vector2.Distance(transform.position, _target.transform.position) < enemyMeleeCharacteristicsSO.RadiusAttack)
            {
                _attack.MakeAttack(_target, enemyMeleeCharacteristicsSO.Damage);
                StartCoroutine(DelayAttack());
                
                yield break;
            }
        
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            _movement.Move();
            StartCoroutine(CheckRange());
        }
        
        private IEnumerator DelayAttack() // Ведёс отсчёт до следующей атаки
        {
            yield return new WaitForSeconds(enemyMeleeCharacteristicsSO.DelayAttack);
            StartCoroutine(CheckRange());
        }
    }
}
