
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class GameManager : MonoBehaviour
    {
        public const string gameVersion = "1.0";

        [Header("Game")]
        [SerializeField] private PlayerController playerController = null;
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private int gameLength = 24;
        [SerializeField] private bool isRandom = false;
        [SerializeField] private bool runStarted = false;

        [Header("Data")]
        [SerializeField] public int currentSlot = 0;
        [SerializeField] public List<GameData> gameSlots = new List<GameData>(3);
        [SerializeField] public GameData randomGameSlot = new GameData();
        

        private IState currentState;

        public static GameManager Instance { get; private set; }

        public PlayerController Player { get { return playerController; } private set { playerController = value; } }
        public GameData Data { get { return isRandom ? randomGameSlot : this.gameSlots[this.currentSlot]; } private set { if (isRandom) { randomGameSlot = value; } else { this.gameSlots[this.currentSlot] = value; } } }
        public bool RunStarted { get => runStarted; set => runStarted = value; }

        public IState CurrentState
        {
            get
            {
                return this.currentState;
            }
            private set
            {
                if (currentState != null)
                {
                    this.currentState.End();
                }
                this.currentState = value;
                if (this.currentState != null)
                {
                    this.currentState.Begin();
                }
            }
        }

        

        void Awake() => CheckBindings();
        void Start() => Initialization();

        void Update() => CurrentState.Update();
        void LateUpdate() => CurrentState.LateUpdate();
        void FixedUpdate() => CurrentState.FixedUpdate();

        public void SetPlaying() => CurrentState = new PlayingState(this.playerController);
        public void AddTime(int time) => Data.timer += time;
        public void StartRun() => RunStarted = true;
        public void EndRun() => RunStarted = false;

        private GameManager() => Instance = this;

        

        private void CheckBindings()
        {
            Player = this.playerController ?? FindObjectOfType<PlayerController>();
            // LevelRenderer = this.levelRenderer ?? FindObjectOfType<LevelRenderer>();
            // EntityPool = this.entityPool ?? FindObjectOfType<EntityPool>();
        }

        private void Initialization()
        {
            RunStarted = false;
            for(int i = 0; i < 3; i++)
            {
                gameSlots.Add(new GameData());
            }
            LoadLevel();
            SetPlaying();
            
        }

        private void LoadLevel()
        {
            levelGenerator.Generate(gameLength);
            //this.currentLevel = levelGenerator.Generate(10, 9);
        }

    }
}