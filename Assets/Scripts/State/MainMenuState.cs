using UnityEngine;

namespace HackedDesign
{
    public class MainMenuState : IState
    {
        private UI.MainMenuPresenter menuPresenter;
        private LevelGenerator levelGenerator;
        private EntityPool entityPool;

        public MainMenuState(LevelGenerator levelGenerator, EntityPool entityPool, UI.MainMenuPresenter mainMenuPresenter)
        {
            this.menuPresenter = mainMenuPresenter;
            this.levelGenerator = levelGenerator;
            this.entityPool = entityPool;
            
        }

        public bool PlayerActionAllowed => false;
                

        public void Begin()
        {
            this.levelGenerator.DestroyLevel();
            this.entityPool.DestroyEntities();
            this.menuPresenter.PopulateValues();
            this.menuPresenter.Reset();
            this.menuPresenter.Show();
            Cursor.lockState = CursorLockMode.None;
            //AudioManager.Instance.PlayMenuMusic();
        }

        public void End()
        {
            this.menuPresenter.Hide();
        }

        public void Update()
        {

        }

        public void FixedUpdate()
        {

        }        

        public void LateUpdate()
        {
            menuPresenter.Repaint();
        }

        public void Start()
        {

        }

        public void Select()
        {

        }        

    }
}