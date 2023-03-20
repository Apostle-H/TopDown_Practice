using EntitySystem.Data.Combat;
using EntitySystem.Data.Interactions;
using EntitySystem.Data.Movement;
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
        [SerializeField] private GameObject playerShield;
        [SerializeField] private ShieldSO playerShieldSO;

        [Header("UI"), Space(5f)] 
        [Header("Player")]
        [SerializeField] private PlayerUIView playerUIView;
        [SerializeField] private HealthSO playerHealthSO;

        private InputHandler _input;

        private Attacker _playerAttacker;
        private HookShooter _playerHookShooter;
        private PlayerResources _playerResources;
        private Patch _patch;
        private Shield _shield;
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
            Rotator rotator = new Rotator(playerGunPivotPoint);
            
            _playerAttacker = new Attacker(playerFirePoint, projectilePool, playerAttackerSO);
            Dragger dragger = new Dragger(playerDraggerJoint);
            _playerHookShooter = new HookShooter(playerFirePoint, hook, dragger, playerHookShooterSO);
            
            _playerResources = new PlayerResources();
            Splitter splitter = new Splitter(_playerResources);
            _patch = new Patch(playerHealth, playerPatchSO);
            _shield = new Shield(playerShield, playerShieldSO);
            
            _playerResources.Add(10);

            PlayerMoveInvoker moveInvoker = new PlayerMoveInvoker(_input, playerTransform, mover, rotator);
            PlayerShootInvoker shootInvoker = new PlayerShootInvoker(_input, _playerAttacker);
            PlayerHookInvoker hookInvoker = new PlayerHookInvoker(_input, playerTransform, rotator,
                playerDragAreaCheckerSO, dragger, _playerHookShooter);
            PlayerSplitInvoker splitInvoker = new PlayerSplitInvoker(_input, playerTransform, splitter, playerSplitAreaCheckerSO);
            PlayerConsumablesInvoker consumablesInvoker = new PlayerConsumablesInvoker(_input, _playerResources, _patch, _shield);
            _playerMasterInvoker = new PlayerMasterInvoker(moveInvoker, shootInvoker, hookInvoker, splitInvoker, consumablesInvoker, playerHealth);
        }

        private void InitUI()
        {
            PlayerUIModel model = new PlayerUIModel(_input);
            new PlayerUIController(model, playerUIView, playerHealth, _playerAttacker, _playerHookShooter, _playerResources, _patch, _shield);
        }
    }
}