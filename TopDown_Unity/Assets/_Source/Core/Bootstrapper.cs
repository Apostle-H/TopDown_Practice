using EntitySystem.Data.Combat;
using EntitySystem.Data.Interactions;
using EntitySystem.Data.Movement;
using EntitySystem.Movement;
using EntitySystem.Shooting;
using EntitySystem.Shooting.Projectiles;
using InputSystem;
using PlayerSystem;
using PlayerSystem.Consumables;
using PlayerSystem.Data;
using PlayerSystem.Data.Consumables;
using PlayerSystem.Data.Interactions;
using PlayerSystem.Interactions;
using PlayerSystem.Invokers;
using ProgressSystem;
using ProgressSystem.Upgrades;
using UI.Player;
using UI.Progression;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Player System"), Space(5f)] 
        [Header("SetUpSO")] 
        [SerializeField] private PlayerSetUpSO defaultPlayerSetUpSO;
        [SerializeField] private PlayerSetUpSO playerSetUpSO;
        [Header("General")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private PlayerHealth playerHealth;
        [Header("Movement")]
        [SerializeField] private Rigidbody2D playerRb;
        [Header("Shooting")]
        [SerializeField] private Transform playerProjectilesHolder;
        [SerializeField] private Transform playerFirePoint;
        [SerializeField] private Transform playerGunPivotPoint;
        [Header("Hooking")]
        [SerializeField] private SpringJoint2D playerDraggerJoint;
        [Header("Consumables")]
        [SerializeField] private GameObject playerShield;

        [Header("UI"), Space(5f)]
        [Header("Player")]
        [SerializeField] private PlayerUIView playerUIView;
        [Header("Progression")]
        [Header("FirstUpgrade")]
        [SerializeField] private UpgradeUI firstUpgradeUI;
        [Header("SecondUpgrade")]
        [SerializeField] private UpgradeUI secondUpgradeUI;
        

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
            playerHealth.Init(playerSetUpSO.HealthSO);
            
            ProjectilePool projectilePool = new ProjectilePool(playerSetUpSO.AttackerSO.ShootDelay, 
                playerSetUpSO.AttackerSO.ProjectilePrefab, playerProjectilesHolder);
            Hook hook = Instantiate(playerSetUpSO.HookShooterSO.HookPrefab, playerProjectilesHolder).GetComponent<Hook>();
            hook.gameObject.SetActive(false);
            
            Mover mover = new Mover(playerRb, playerSetUpSO.MoverSO);
            Rotator rotator = new Rotator(playerGunPivotPoint);
            
            _playerAttacker = new Attacker(playerFirePoint, projectilePool, playerSetUpSO.AttackerSO);
            Dragger dragger = new Dragger(playerDraggerJoint);
            _playerHookShooter = new HookShooter(playerFirePoint, hook, dragger, playerSetUpSO.HookShooterSO);
            
            _playerResources = new PlayerResources();
            Splitter splitter = new Splitter(_playerResources);
            _patch = new Patch(playerHealth, playerSetUpSO.PatchSO);
            _shield = new Shield(playerShield, playerSetUpSO.ShieldSO);
            
            _playerResources.Add(10);

            PlayerMoveInvoker moveInvoker = new PlayerMoveInvoker(_input, playerTransform, mover, rotator);
            PlayerShootInvoker shootInvoker = new PlayerShootInvoker(_input, _playerAttacker);
            PlayerHookInvoker hookInvoker = new PlayerHookInvoker(_input, playerTransform, rotator,
                playerSetUpSO.DragAreaCheckerSO, dragger, _playerHookShooter);
            PlayerSplitInvoker splitInvoker = new PlayerSplitInvoker(_input, playerTransform, splitter, playerSetUpSO.SplitAreaCheckerSO);
            PlayerConsumablesInvoker consumablesInvoker = new PlayerConsumablesInvoker(_input, _playerResources, _patch, _shield);
            _playerMasterInvoker = new PlayerMasterInvoker(moveInvoker, shootInvoker, hookInvoker, splitInvoker, consumablesInvoker, playerHealth);
        }

        private void InitUI()
        {
            PlayerUIModel model = new PlayerUIModel(_input);
            new PlayerUIController(model, playerUIView, playerHealth, _playerAttacker, _playerHookShooter, _playerResources, _patch, _shield);

            PlayerModifier playerModifier = new PlayerModifier(defaultPlayerSetUpSO, playerSetUpSO);
            firstUpgradeUI.Init(playerModifier);
            firstUpgradeUI.Bind();
            secondUpgradeUI.Init(playerModifier);
            secondUpgradeUI.Bind();
            
            firstUpgradeUI.Open();
            secondUpgradeUI.Open();
        }
    }
}