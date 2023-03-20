using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Movement
{
    public class NavMeshMover
    {
        private GameObject _target;
        private readonly NavMeshAgent _navMesh;
        private readonly int _speed;
        
        public NavMeshMover(NavMeshAgent navMesh, int speed)
        {
            _navMesh = navMesh;

            _speed = speed;
        }

        public void TargetFound(GameObject target)
        {
            _target = target;
            
            _navMesh.speed = _speed;
        }
        
        public void Move()
        {
            _navMesh.SetDestination(_target.transform.position);
        }

        public void TargetLost()
        {
            _target = null;

            _navMesh.speed = 0;
        }
    }
}