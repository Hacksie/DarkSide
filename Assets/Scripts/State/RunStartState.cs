using UnityEngine;

namespace HackedDesign
{
    public class RunStartState : IState
    {
        private PlayerController player;
        private UI.AbstractPresenter runStartPresenter;

        public bool PlayerActionAllowed => false;

        public RunStartState(PlayerController player, UI.AbstractPresenter runStartPresenter)
        {

            this.player = player;
            this.runStartPresenter = runStartPresenter;

        }


        public void Begin()
        {
            this.player.Reset();
            GameManager.Instance.Data.timer = GameManager.Instance.GameSettings.initialAddTime;
            GameManager.Instance.LoadLevelSelect();
            runStartPresenter.Show();
            Cursor.lockState = CursorLockMode.None;
        }

        public void End()
        {
            runStartPresenter.Hide();
        }

  
        public void FixedUpdate()
        {

        }

        public void LateUpdate()
        {
            runStartPresenter.Repaint();
        }

   
        public void Start()
        {
            
        }


        public void Update()
        {
            
        }
    }
}