using DG.Tweening;
using EntitySystem.Data.Movement;
using UnityEngine;

namespace EntitySystem.Movement
{
    public class Mover
    {
        private Rigidbody2D _rb;
        private MoverSettingsSO _settingsSO;

        private Vector2 _direction;
        private bool _isCarrying;

        private Sequence _movePerformer;

        public Mover(Rigidbody2D rb, MoverSettingsSO settingsSO)
        {
            _rb = rb;
            _settingsSO = settingsSO;
            
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
            float speed = _isCarrying ? _settingsSO.CarrySpeed : _settingsSO.Speed;
            Vector2 newPos = _rb.position + _direction * (speed * Time.fixedDeltaTime);
            _rb.MovePosition(newPos);

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