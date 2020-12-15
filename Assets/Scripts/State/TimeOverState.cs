using UnityEngine;

namespace HackedDesign
{
    public class TimeOverState : IState
    {
        private PlayerController player;
        private UI.AbstractPresenter timeOverPresenter;

        public bool PlayerActionAllowed => false;

        public TimeOverState(PlayerController player, UI.AbstractPresenter timeOverPresenter)
        {
            this.player = player;
            this.timeOverPresenter = timeOverPresenter;
        }


        public void Begin()
        {
            GameManager.Instance.LoadLevel();
            timeOverPresenter.Show();
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayTimeOver();
            
            GameManager.Instance.Reset();
            GameManager.Instance.RunStarted = false;
            Cursor.lockState = CursorLockMode.None;
        }

        public void End()
        {
            timeOverPresenter.Hide();
        }

  
        public void FixedUpdate()
        {

        }

        public void LateUpdate()
        {
            timeOverPresenter.Repaint();
        }

   
        public void Start()
        {
            
        }

        public void Select()
        {

        }

        public void Update()
        {
            //this.player.UpdateBehaviour();
        }
    }
}