using InputSystem;
using PlayerSystem.Data.Movement;
using PlayerSystem.Data.Shooting;
using PlayerSystem.Movement;
using PlayerSystem.Shooting;
using PlayerSystem.Shooting.Projectiles;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Player System")] 
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Rigidbody2D playerRb;
        [SerializeField] private MoverSettingsSO playerMoverSettingSO;
        [SerializeField] private Transform playerProjectilesHolder;
        [SerializeField] private Transform playerFirePoint;
        [SerializeField] private Transform playerGunPivotPoint;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private ShooterSettingsSO playerShooterSettingsSO;
        
        private InputHandler _input;

        private PlayerInvoker _playerInvoker;
        private Mover _playerMover;
        private ProjectilePool _playerProjectilePool;
        private Shooter _playerShooter;

        private Game _game;

        private void Awake()
        {
            _input = new InputHandler();

            InitPlayer();

            _game = new Game(_input, _playerInvoker);
            _game.Start();
        }

        private void InitPlayer()
        {
            _playerProjectilePool = new ProjectilePool(playerShooterSettingsSO.ShootDelay, projectilePrefab, playerProjectilesHolder);
            
            _playerMover = new Mover(playerRb, playerMoverSettingSO);
            _playerShooter = new Shooter(playerFirePoint, playerGunPivotPoint, _playerProjectilePool, playerShooterSettingsSO);
            _playerInvoker = new PlayerInvoker(_input, playerTransform, _playerMover, _playerShooter);
        }
    }
}