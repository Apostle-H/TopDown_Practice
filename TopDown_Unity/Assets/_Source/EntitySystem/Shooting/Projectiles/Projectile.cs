using System;
using DG.Tweening;
using EntitySystem.Data.Combat.Projectiles;
using EntitySystem.Health;
using UnityEngine;
using Utils;

namespace EntitySystem.Shooting.Projectiles
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            Damage(other);

            if (settingsSO.StayTillLifeTime || settingsSO.IgnoreMask.Contains(other.gameObject.layer))
            {
                return;
            }
            Kill();
        }

        public void ShootSelf()
        {
            rb.velocity = transform.up * settingsSO.Speed;
            _killSequence.Restart();
        }

        private void Damage(Collider2D other)
        {
            Damageable target;
            if (!settingsSO.TargetMask.Contains(other.gameObject.layer) || 
                (target = other.gameObject.GetComponent<Damageable>()) == null)
            {
                return;
            }
            
            target.TakeDamage(settingsSO.Damage);
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