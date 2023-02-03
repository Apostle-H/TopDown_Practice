using System.Collections;
using EnemySystem.Data;
using UnityEngine;
using UnityEngine.AI;
using Until;
using Utils;

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
            if (Check.Contains(enemyMeleeCharacteristicsSO.Layer, col.gameObject.layer))
            {
                _target = col.gameObject;
                _movement.TargetFound(_target);
                
                StartCoroutine(CheckRange());
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (Check.Contains(enemyMeleeCharacteristicsSO.Layer, col.gameObject.layer))
            {
                _target = null;
                _movement.TargetLost();
                
                StopAllCoroutines();
            }
        }
        
        private IEnumerator CheckRange()
        {
            transform.rotation = transform.LookAt2D(_target.transform.position);

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
        
        private IEnumerator DelayAttack()
        {
            yield return new WaitForSeconds(enemyMeleeCharacteristicsSO.DelayAttack);
            StartCoroutine(CheckRange());
        }
    }
}
