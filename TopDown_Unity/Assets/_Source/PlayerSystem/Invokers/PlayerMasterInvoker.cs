using System;
using EntitySystem.Health;
using InputSystem;
using PlayerSystem.Interactions;
using EntitySystem.Shooting;
using EntitySystem.Movement;
using PlayerSystem.Data.Interactions;
using PlayerSystem.Invokers;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Utils.Services;

namespace PlayerSystem
{
    public class PlayerMasterInvoker
    {
        private PlayerMoveInvoker _moveInvoker;
        private PlayerShootInvoker _shootInvoker;
        private PlayerHookInvoker _hookInvoker;

        private PlayerHealth _health;

        public PlayerMasterInvoker(PlayerMoveInvoker moveInvoker, PlayerShootInvoker shootInvoker, PlayerHookInvoker hookInvoker, 
            PlayerHealth health)
        {
            _moveInvoker = moveInvoker;
            _shootInvoker = shootInvoker;
            _hookInvoker = hookInvoker;
            _health = health;
        }

        public void Bind()
        {
            _health.OnDeath += _moveInvoker.Expose;
            _health.OnDeath += _shootInvoker.Expose;
            _health.OnDeath += _hookInvoker.Expose;
            
            _moveInvoker.Bind();
            _shootInvoker.Bind();
            _hookInvoker.Bind();

            _hookInvoker.OnHookOut += _shootInvoker.Expose;
            _hookInvoker.OnHookIn += _shootInvoker.Bind;
            _hookInvoker.OnHooked += _moveInvoker.SlowDownCarrying;
            _hookInvoker.OnReleased += _moveInvoker.SpeedUpReleasing;
        }

        public void Expose()
        {
            _hookInvoker.OnHookOut -= _shootInvoker.Expose;
            _hookInvoker.OnHookIn -= _shootInvoker.Bind;
            _hookInvoker.OnHooked -= _moveInvoker.SlowDownCarrying;
            _hookInvoker.OnReleased -= _moveInvoker.SpeedUpReleasing;
            
            _moveInvoker.Expose();
            _shootInvoker.Expose();
            _hookInvoker.Expose();
        }
    }
}