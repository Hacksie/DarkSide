#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        [SerializeField] private UnityEngine.Audio.AudioMixer mixer = null;
        [SerializeField] private Animator fullScreenEffectAnimator = null;


        [Header("Data")]
        [SerializeField] public int currentSlot = 0;
        [SerializeField] public List<GameData> gameSlots = new List<GameData>(3);
        [SerializeField] public GameData randomGameSlot = new GameData();

        [Header("UI")]
        [SerializeField] private UI.MainMenuPresenter? mainMenuPresenter = null;
        [SerializeField] private UI.AbstractPresenter? hudPresenter = null;
        [SerializeField] private UI.AbstractPresenter? levelOverPresenter = null;
        [SerializeField] private UI.AbstractPresenter? timeOverPresenter = null;
        [SerializeField] private UI.AbstractPresenter? runStartPresenter = null;
        [SerializeField] private UI.AbstractPresenter? shopPresenter = null;
        [SerializeField] private UI.AbstractPresenter? pausePresenter = null;
        [SerializeField] private UI.AbstractPresenter? deadPresenter = null;
        [SerializeField] private UnityEngine.UI.RawImage? fullscreenEffect = null;
        [SerializeField] private Color lowHealthColor = Color.red;

        private bool damageFlag = false;

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
        public bool Random { get { return isRandom; } }

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
        void LateUpdate()
        {
            CurrentState?.LateUpdate();
            if (CurrentState.PlayerActionAllowed)
            {
                fullScreenEffectAnimator.SetBool("takedamage", damageFlag);
                fullScreenEffectAnimator.SetInteger("health", Data.health);
            }
            damageFlag = false;
        }

        void FixedUpdate() => CurrentState?.FixedUpdate();

        public void SetPlaying() => CurrentState = new PlayingState(this.playerController, this.weaponManager, this.entityPool, this.hudPresenter);
        public void SetMainMenu() => CurrentState = new MainMenuState(this.levelGenerator, this.entityPool, this.mainMenuPresenter);
        public void SetLevelOver() => CurrentState = new LevelOverState(this.playerController, this.levelOverPresenter);
        public void SetDead() => CurrentState = new DeadState(this.deadPresenter);
        public void SetTimeOver() => CurrentState = new TimeOverState(this.playerController, this.timeOverPresenter);
        public void SetRunStart() => CurrentState = new RunStartState(this.playerController, this.runStartPresenter, this.shopPresenter);
        public void SetPaused() => CurrentState = new PauseState(this.pausePresenter);

        public void AddTime(int time) => Data.timer += time;

        public void LoadSlots()
        {
            this.gameSlots = new List<GameData>(3) { null, null, null };
            this.gameSlots[0] = LoadSaveFile(0);
            this.gameSlots[1] = LoadSaveFile(1);
            this.gameSlots[2] = LoadSaveFile(2);
            // this.gameSlots[0] = new GameData();
            // this.gameSlots[1] = new GameData();
            // this.gameSlots[2] = new GameData();
        }

        GameData LoadSaveFile(int slot)
        {
            var path = Path.Combine(Application.persistentDataPath, $"SaveFile{slot}.json");
            Logger.Log(name, "Attempting to load ", path);
            if (File.Exists(path))
            {
                Logger.Log(name, "Loading ", path);
                var contents = File.ReadAllText(path);
                return JsonUtility.FromJson<GameData>(contents);
            }
            else
            {
                Logger.Log(name, "Save file does not exist ", path);
            }

            return new GameData();
        }

        public void SaveGame()
        {
            Data.saveName = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            Logger.Log(this, "Saving state ", Data.saveName);
            string json = JsonUtility.ToJson(Data);
            string path = Path.Combine(Application.persistentDataPath, $"SaveFile{currentSlot}.json");
            File.WriteAllText(path, json);
            Logger.Log(this, "Saved ", path);
        }

        public void NewGame(int seed, string difficulty, bool permadeath, bool random)
        {
            var game = new GameData()
            {
                dead = false,
                difficulty = difficulty,
                permadeath = permadeath,
                currentLevelIndex = 0,
                seed = seed,
            };

            isRandom = random;

            if (random)
            {
                randomGameSlot = game;
            }
            else
            {
                GameManager.Instance.gameSlots[GameManager.Instance.currentSlot] = game;
            }



            GameManager.Instance.SetRunStart();
        }

        public void StartRun()
        {
            RunStarted = true;
            Data.levelStartTime = Time.time;
        }

        public void NextLevel()
        {
            Reset();
            Data.currentLevelIndex++;
        }

        public void Reset()
        {
            Data.health = Data.maxHealth;
            Data.energy = Data.maxEnergy;
            Data.shields = 0;
            Data.currentLevelScore = 0;
            Data.currentWeapon = weaponManager.GetMaxWeapon();
            Data.timer = 0;
            EntityPool.DestroyEntities();
            Player.Reset();

        }

        public float DifficultyAdjustment()
        {
            switch (Data.difficulty)
            {
                case "Normal":
                default:
                    return GameSettings.normalAdj;
                case "Hard":
                    return GameSettings.hardAdj;
                case "Ultra":
                    return GameSettings.ultraAdj;
            }
        }

        public void EndRun() => RunStarted = false;

        public void ConsumeEnergy(float energy)
        {
            if (GameSettings.unlimitedEnergy)
                return;

            GameManager.Instance.Data.energy = Mathf.Clamp(Data.energy - energy, 0, Data.maxEnergy);

        }

        public void ConsumeShields(float amount)
        {
            GameManager.Instance.Data.shields = Mathf.Clamp(Data.shields - amount, 0, Data.maxShields);
        }

        public void ConsumeHealth(int amount)
        {
            if (GameManager.Instance.GameSettings.invulnerability)
            {
                return;
            }

            // If we're already dead, we can't get more dead
            if (Data.health <= 0)
            {
                return;
            }

            var prevHealth = Data.health;
            GameManager.Instance.Data.health = Mathf.Clamp(Data.health - amount, 0, Data.maxHealth);
            if (prevHealth > Data.health)
            {
                damageFlag = true;
            }

            if (Data.health <= GameSettings.lowHealth)
            {
                fullscreenEffect.color = lowHealthColor;
            }

            if (Data.health <= 0)
            {
                SetDead();
            }
        }

        public void TakeDamage(int boltDamage, int energyDamage)
        {
            Logger.Log(this, "player hit with damage ", boltDamage.ToString(), " ", energyDamage.ToString());


            if (energyDamage > 0 && Data.shields > 0)
            {

                if ((energyDamage * GameSettings.shieldvsenergyfactor) >= Data.shields)
                {
                    var consumed = Mathf.CeilToInt((float)Data.shields / GameSettings.shieldvsenergyfactor);
                    Data.shields = 0;
                    energyDamage -= consumed;
                }
                else
                {
                    Data.shields -= (energyDamage * GameSettings.shieldvsenergyfactor);
                    energyDamage = 0;
                }
            }

            if (boltDamage > 0 && Data.shields > 0)
            {

                if ((boltDamage * GameSettings.shieldvsboltfactor) >= Data.shields)
                {
                    var consumed = Mathf.CeilToInt((float)Data.shields / GameSettings.shieldvsboltfactor);
                    Data.shields = 0;
                    boltDamage -= consumed;
                }
                else
                {
                    Data.shields -= (boltDamage * GameSettings.shieldvsboltfactor);
                    boltDamage = 0;
                }
            }

            ConsumeHealth(Mathf.FloorToInt(energyDamage * GameSettings.bodyvsenergyfactor));
            ConsumeHealth(Mathf.FloorToInt(boltDamage * GameSettings.bodyvsboltfactor));

        }

        public void ConsumeBolts(int bolts)
        {
            if (GameSettings.unlimitedBolts)
                return;

            GameManager.Instance.Data.bolts = Mathf.Clamp(Data.bolts - bolts, 0, Data.maxBolts);
        }

        public void AddScore(int score)
        {
            GameManager.Instance.Data.currentLevelScore = Mathf.Clamp(Data.currentLevelScore + score, 0, GameSettings.maxScore);
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
            preferences = new PlayerPreferences(this.mixer);
            preferences.Load();
            preferences.SetPreferences();

            LoadSlots();
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
            pausePresenter?.Hide();
            deadPresenter?.Hide();
        }
    }
}