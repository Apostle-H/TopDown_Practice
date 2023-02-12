using EntitySystem.Data.Combat;
using EntitySystem.Data.Interactions;
using EntitySystem.Data.Movement;
using EntitySystem.Health;
using EntitySystem.Movement;
using EntitySystem.Shooting;
using EntitySystem.Shooting.Projectiles;
using InputSystem;
using PlayerSystem;
using PlayerSystem.Data.Interactions;
using PlayerSystem.Interactions;
using PlayerSystem.Invokers;
using UI.Player;
using UnityEngine;
using UnityEngine.Rendering;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Player System")] 
        [SerializeField] private Transform playerTransform;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private Rigidbody2D playerRb;
        [SerializeField] private MoverSO playerMoverSO;
        [SerializeField] private Transform playerProjectilesHolder;
        [SerializeField] private Transform playerFirePoint;
        [SerializeField] private Transform playerGunPivotPoint;
        [SerializeField] private AttackerSO playerAttackerSO;
        [SerializeField] private AreaCheckerSO playerDragAreaCheckerSO;
        [SerializeField] private SpringJoint2D playerDraggerJoint;
        [SerializeField] private HookShooterSO playerHookShooterSO;

        [Header("UI"), Space(5f)] 
        [Header("Player")]
        [SerializeField] private PlayerUIView playerUIView;
        [SerializeField] private HealthSO playerHealthSO;

        private InputHandler _input;

        private PlayerMasterInvoker _playerMasterInvoker;

        private Game _game;

        private void Awake()
        {
            _input = new InputHandler();

            InitPlayer();
            InitUI();

            _game = new Game(_input, _playerMasterInvoker);
            _game.Start();
        }

        private void InitPlayer()
        {
            ProjectilePool projectilePool = new ProjectilePool(playerAttackerSO.ShootDelay, playerAttackerSO.ProjectilePrefab, playerProjectilesHolder);
            Hook hook = Instantiate(playerHookShooterSO.HookPrefab, playerProjectilesHolder).GetComponent<Hook>();
            hook.gameObject.SetActive(false);
            
            Mover mover = new Mover(playerRb, playerMoverSO);
            ShooterRotator shooterRotator = new ShooterRotator(playerGunPivotPoint);
            Attacker attacker = new Attacker(playerFirePoint, projectilePool, playerAttackerSO);
            Dragger dragger = new Dragger(playerDraggerJoint);
            HookShooter hookShooter = new HookShooter(playerFirePoint, hook, dragger, playerHookShooterSO);

            PlayerMoveInvoker moveInvoker = new PlayerMoveInvoker(_input, playerTransform, mover, shooterRotator);
            PlayerShootInvoker shootInvoker = new PlayerShootInvoker(_input, playerTransform, shooterRotator, attacker);
            PlayerHookInvoker hookInvoker = new PlayerHookInvoker(_input, playerTransform, shooterRotator,
                playerDragAreaCheckerSO, dragger, hookShooter);
            
            _playerMasterInvoker = new PlayerMasterInvoker(moveInvoker, shootInvoker, hookInvoker, playerHealth);
        }

        private void InitUI()
        {
            PlayerUIModel playerUIModel = new PlayerUIModel(playerHealthSO);
            PlayerUIController playerUIController = new PlayerUIController(playerUIModel, playerUIView, playerHealth);
        }
    }
}