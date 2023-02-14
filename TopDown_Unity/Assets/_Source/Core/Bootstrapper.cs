using EntitySystem.Data.Combat;
using EntitySystem.Data.Interactions;
using EntitySystem.Data.Movement;
using EntitySystem.Health;
using EntitySystem.Movement;
using EntitySystem.Shooting;
using EntitySystem.Shooting.Projectiles;
using InputSystem;
using PlayerSystem;
using PlayerSystem.Consumables;
using PlayerSystem.Data.Consumables;
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
        [Header("General")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private PlayerHealth playerHealth;
        [Header("Movement")]
        [SerializeField] private Rigidbody2D playerRb;
        [SerializeField] private MoverSO playerMoverSO;
        [Header("Shooting")]
        [SerializeField] private Transform playerProjectilesHolder;
        [SerializeField] private Transform playerFirePoint;
        [SerializeField] private Transform playerGunPivotPoint;
        [SerializeField] private AttackerSO playerAttackerSO;
        [Header("Hooking")]
        [SerializeField] private AreaCheckerSO playerDragAreaCheckerSO;
        [SerializeField] private SpringJoint2D playerDraggerJoint;
        [SerializeField] private HookShooterSO playerHookShooterSO;
        [Header("Splitter")] 
        [SerializeField] private AreaCheckerSO playerSplitAreaCheckerSO;
        [Header("Consumables")]
        [SerializeField] private PatchSO playerPatchSO;

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
            PlayerResources playerResources = new PlayerResources();
            Splitter splitter = new Splitter(playerResources);
            Patch patch = new Patch(playerHealth, playerPatchSO);

            PlayerMoveInvoker moveInvoker = new PlayerMoveInvoker(_input, playerTransform, mover, shooterRotator);
            PlayerShootInvoker shootInvoker = new PlayerShootInvoker(_input, attacker);
            PlayerHookInvoker hookInvoker = new PlayerHookInvoker(_input, playerTransform, shooterRotator,
                playerDragAreaCheckerSO, dragger, hookShooter);
            PlayerSplitInvoker splitInvoker = new PlayerSplitInvoker(_input, playerTransform, splitter, playerSplitAreaCheckerSO);
            PlayerConsumablesInvoker consumablesInvoker = new PlayerConsumablesInvoker(_input, playerResources, patch);
            _playerMasterInvoker = new PlayerMasterInvoker(moveInvoker, shootInvoker, hookInvoker, splitInvoker, consumablesInvoker, playerHealth);
        }

        private void InitUI()
        {
            PlayerUIModel playerUIModel = new PlayerUIModel(playerHealthSO);
            PlayerUIController playerUIController = new PlayerUIController(playerUIModel, playerUIView, playerHealth);
        }
    }
}