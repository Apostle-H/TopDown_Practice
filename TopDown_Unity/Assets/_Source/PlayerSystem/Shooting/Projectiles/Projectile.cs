using System;
using DG.Tweening;
using PlayerSystem.Data.Shooting;
using UnityEngine;

namespace PlayerSystem.Shooting.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileSettingsSO settingsSO;
        [SerializeField] private Rigidbody2D rb;

        public Action<Projectile> OnDie;

        public float LifeTime => settingsSO.LifeTime;

        private Sequence _killSequence;

        private void Awake()
        {
            InitSequence();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Kill();
        }

        public void ShootSelf()
        {
            rb.velocity = transform.up * settingsSO.Speed;
            _killSequence.Restart();
        }

        private void Kill()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
            _killSequence.Pause();
            
            gameObject.SetActive(false);
            OnDie?.Invoke(this);
        }

        private void InitSequence()
        {
            _killSequence = DOTween.Sequence();
            _killSequence.Pause();
            _killSequence.SetAutoKill(false);

            _killSequence.AppendInterval(settingsSO.LifeTime);
            _killSequence.AppendCallback(Kill);
        }
    }
}