using UnityEngine;

namespace HackedDesign
{
    public class MainMenuState : IState
    {
        private UI.AbstractPresenter menuPresenter;
        private LevelGenerator levelGenerator;

        public MainMenuState(LevelGenerator levelGenerator, UI.AbstractPresenter mainMenuPresenter)
        {
            this.menuPresenter = mainMenuPresenter;
            this.levelGenerator = levelGenerator;
            
        }

        public bool PlayerActionAllowed => false;
                

        public void Begin()
        {
            this.levelGenerator.DestroyLevel();
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

    }
}