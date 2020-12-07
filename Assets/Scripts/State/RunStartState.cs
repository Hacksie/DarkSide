using UnityEngine;

namespace HackedDesign
{
    public class RunStartState : IState
    {
        private PlayerController player;
        private UI.AbstractPresenter runStartPresenter;
        private UI.AbstractPresenter shopPresenter;

        private  RunStartUIState state;


        public bool PlayerActionAllowed => false;

        public RunStartState(PlayerController player, UI.AbstractPresenter runStartPresenter, UI.AbstractPresenter shopPresenter)
        {
            this.shopPresenter = shopPresenter;
            this.player = player;
            this.runStartPresenter = runStartPresenter;

        }


        public void Begin()
        {
            this.player.Reset();
            GameManager.Instance.Data.timer = GameManager.Instance.GameSettings.initialAddTime;
            GameManager.Instance.LoadLevel();
            state = RunStartUIState.RunStart;
            runStartPresenter.Show();
            Cursor.lockState = CursorLockMode.None;
            AudioManager.Instance.PlayWaitingMusic();
        }

        public void End()
        {
            runStartPresenter.Hide();
            AudioManager.Instance.StopMusic();
        }

  
        public void FixedUpdate()
        {

        }

        public void LateUpdate()
        {
            if(state == RunStartUIState.RunStart)
            {
                runStartPresenter.Repaint();
            }
            else 
            {
                shopPresenter.Repaint();
            }
            
        }

   
        public void Start()
        {
            state = RunStartUIState.RunStart;
            runStartPresenter.Show();
            shopPresenter.Hide();
        }

        public void Select()
        {
            state = RunStartUIState.Shop;
            runStartPresenter.Hide();
            shopPresenter.Show();
        }        


        public void Update()
        {
            
        }

        private enum RunStartUIState {
            RunStart,
            Shop
        }

        
    }
}