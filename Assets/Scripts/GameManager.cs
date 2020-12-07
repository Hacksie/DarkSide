#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class GameManager : MonoBehaviour
    {
        public const string gameVersion = "1.0";

        [Header("Game")]
        [SerializeField] private Camera? mainCamera = null;
        [SerializeField] private PlayerController? playerController = null;
        [SerializeField] private LevelGenerator? levelGenerator = null;
        [SerializeField] private EntityPool? entityPool = null;
        [SerializeField] private WeaponManager? weaponManager = null;
        [SerializeField] private int gameLength = 24;
        [SerializeField] private bool isRandom = false;
        [SerializeField] private bool runStarted = false;
        [SerializeField] private GameSettings? settings = null;
        [SerializeField] private PlayerPreferences? preferences = null;
        

        [Header("Data")]
        [SerializeField] public int currentSlot = 0;
        [SerializeField] public List<GameData> gameSlots = new List<GameData>(3);
        [SerializeField] public GameData randomGameSlot = new GameData();

        [Header("UI")]
        [SerializeField] private UI.AbstractPresenter? mainMenuPresenter = null;
        [SerializeField] private UI.AbstractPresenter? hudPresenter = null;
        [SerializeField] private UI.AbstractPresenter? levelOverPresenter = null;
        [SerializeField] private UI.AbstractPresenter? timeOverPresenter = null;
        [SerializeField] private UI.AbstractPresenter? runStartPresenter = null;
        [SerializeField] private UI.AbstractPresenter? shopPresenter = null;

        private IState currentState = new EmptyState();

        #pragma warning disable CS8618
        public static GameManager Instance { get; private set; }
        #pragma warning restore CS8618

        public Camera? MainCamera { get { return mainCamera; } private set { mainCamera = value; } }
        public PlayerController? Player { get { return playerController; } private set { playerController = value; } }
        public GameData Data { get { return isRandom ? randomGameSlot : this.gameSlots[this.currentSlot]; } private set { if (isRandom) { randomGameSlot = value; } else { this.gameSlots[this.currentSlot] = value; } } }
        public bool RunStarted { get => runStarted; set => runStarted = value; }
        public GameSettings? GameSettings { get { return settings; } private set { settings = value; } }
        public EntityPool? EntityPool { get { return entityPool; } private set { entityPool = value; } }
        public WeaponManager? WeaponManager { get { return weaponManager; } private set { weaponManager = value; } }
        public PlayerPreferences? PlayerPreferences { get { return preferences; } private set { preferences = value; } }

        public IState CurrentState
        {
            get
            {
                return this.currentState;
            }
            private set
            {
                this.currentState.End();

                this.currentState = value;

                this.currentState.Begin();
            }
        }

        //static GameManager() => Instance = new GameManager();
        private GameManager() => Instance = this;

        void Awake() => CheckBindings();
        void Start() => Initialization();

        void Update() => CurrentState?.Update();
        void LateUpdate() => CurrentState?.LateUpdate();
        void FixedUpdate() => CurrentState?.FixedUpdate();

        public void SetPlaying() => CurrentState = new PlayingState(this.playerController, this.entityPool, this.hudPresenter);
        public void SetMainMenu() => CurrentState = new MainMenuState(this.levelGenerator, this.entityPool, this.mainMenuPresenter);
        public void SetLevelOver() => CurrentState = new LevelOverState(this.playerController, this.levelOverPresenter);
        public void SetTimeOver() => CurrentState = new TimeOverState(this.playerController, this.timeOverPresenter);
        public void SetRunStart() => CurrentState = new RunStartState(this.playerController, this.runStartPresenter, this.shopPresenter);

        public void AddTime(int time) => Data.timer += time;
        public void StartRun() => RunStarted = true;
        public void EndRun() => RunStarted = false;

        public void ConsumeEnergy(float energy)
        {
            GameManager.Instance.Data.energy = Mathf.Clamp(GameManager.Instance.Data.energy - energy, 0, GameManager.Instance.Data.maxEnergy);
        }

        public void ConsumeBolts(int bullets)
        {
            GameManager.Instance.Data.bullets = Mathf.Clamp(GameManager.Instance.Data.bullets - bullets, 0, GameManager.Instance.Data.maxBullets);
        }

        public void LoadLevel()
        {
            entityPool?.DestroyEntities();
            levelGenerator?.Generate(gameLength);
        }

        public void LoadLevelSelect()
        {
            entityPool?.DestroyEntities();
            levelGenerator?.GenerateLevelSelect();
        }

        private void CheckBindings()
        {
            Player = this.playerController ?? FindObjectOfType<PlayerController>();
        }

        private void Initialization()
        {
            mainCamera = mainCamera ?? Camera.main;
            preferences = new PlayerPreferences();
            RunStarted = false;
            for (int i = 0; i < 3; i++)
            {
                gameSlots.Add(new GameData());
            }

            HideAllUI();
            SetMainMenu();
        }

        private void HideAllUI()
        {
            hudPresenter?.Hide();
            levelOverPresenter?.Hide();
            timeOverPresenter?.Hide();
            runStartPresenter?.Hide();
            shopPresenter?.Hide();
        }
    }
}