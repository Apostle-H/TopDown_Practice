using EntitySystem.Data.Combat;
using EntitySystem.Data.Movement;
using EntitySystem.Movement;
using EntitySystem.Shooting;
using EntitySystem.Shooting.Projectiles;
using InputSystem;
using PlayerSystem;
using PlayerSystem.Data.Interactions;
using PlayerSystem.Interactions;
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
        [SerializeField] private AttackerSettingsSO playerAttackerSettingsSO;
        [SerializeField] private HookShooterSettingsSO playerHookShooterSettingsSO;
        [SerializeField] private DraggerSettingsSO playerDraggerSettingsSO;
        [SerializeField] private SpringJoint2D playerDraggerJoint;
        
        private InputHandler _input;

        private PlayerInvoker _playerInvoker;
        private Mover _playerMover;
        private ShooterRotator _playerShooterRotator;
        private ProjectilePool _playerProjectilePool;
        private Attacker _playerAttacker;
        private HookShooter _playerHookShooter;
        private Dragger _playerDragger;

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
            _playerProjectilePool = new ProjectilePool(playerAttackerSettingsSO.ShootDelay, playerAttackerSettingsSO.ProjectilePrefab, playerProjectilesHolder);
            Hook hook = Instantiate(playerHookShooterSettingsSO.HookPrefab, playerProjectilesHolder).GetComponent<Hook>();
            hook.gameObject.SetActive(false);
            
            _playerMover = new Mover(playerRb, playerMoverSettingSO);
            _playerShooterRotator = new ShooterRotator(playerGunPivotPoint);
            _playerAttacker = new Attacker(playerFirePoint, _playerProjectilePool, playerAttackerSettingsSO);
            _playerHookShooter = new HookShooter(playerFirePoint, hook, playerHookShooterSettingsSO);
            _playerDragger = new Dragger(playerTransform, playerDraggerJoint, playerDraggerSettingsSO);
            _playerInvoker = new PlayerInvoker(_input, playerTransform, _playerMover, 
                _playerShooterRotator, _playerAttacker, _playerHookShooter,
                _playerDragger);
        }
    }
}