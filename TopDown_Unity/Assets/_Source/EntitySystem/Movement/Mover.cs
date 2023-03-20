using DG.Tweening;
using EntitySystem.Data.Movement;
using UnityEngine;

namespace EntitySystem.Movement
{
    public class Mover
    {
        private readonly Rigidbody2D _rb;
        private readonly MoverSO _so;

        private Vector2 _direction;
        private bool _isCarrying;

        private Sequence _movePerformer;

        public Mover(Rigidbody2D rb, MoverSO so)
        {
            _rb = rb;
            _so = so;
            
            InitSequence();
        }

        public void UpdateDirection(Vector2 newDirection)
        {
            _direction = newDirection;

            _movePerformer.Play();
        }

        public void UpdateIsCarrying(bool isCarrying)
        {
            _isCarrying = isCarrying;
        }

        private void PerformMove()
        {
            _direction = _direction.normalized;
            float speed = _isCarrying ? _so.CarrySpeed : _so.Speed;
            _rb.velocity = _direction * speed;

            if (_direction.magnitude == 0f)
            {
                _movePerformer.Rewind();
            }
        }

        private void InitSequence()
        {
            _movePerformer = DOTween.Sequence();
            _movePerformer.SetAutoKill(false);

            _movePerformer.AppendInterval(Time.fixedDeltaTime);
            _movePerformer.AppendCallback(PerformMove);
            _movePerformer.AppendCallback(() => _movePerformer.Restart());
        }
    }
}