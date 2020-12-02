
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

        [Header("Data")]
        [SerializeField] public int currentSlot = 0;
        [SerializeField] public List<GameData> gameSlots = new List<GameData>(3);
        [SerializeField] public GameData randomGameSlot = new GameData();
        

        private IState currentState;

        public static GameManager Instance { get; private set; }

        public PlayerController Player { get { return playerController; } private set { playerController = value; } }
        public GameData Data { get { return isRandom ? randomGameSlot : this.gameSlots[this.currentSlot]; } private set { if (isRandom) { randomGameSlot = value; } else { this.gameSlots[this.currentSlot] = value; } } }

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

        public void SetPlaying() => CurrentState = new PlayingState(this.playerController);

        private GameManager() => Instance = this;

        void Awake() => CheckBindings();
        void Start() => Initialization();

        void Update() => CurrentState.Update();
        void LateUpdate() => CurrentState.LateUpdate();
        void FixedUpdate() => CurrentState.FixedUpdate();

        private void CheckBindings()
        {
            Player = this.playerController ?? FindObjectOfType<PlayerController>();
            // LevelRenderer = this.levelRenderer ?? FindObjectOfType<LevelRenderer>();
            // EntityPool = this.entityPool ?? FindObjectOfType<EntityPool>();
        }

        private void Initialization()
        {
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