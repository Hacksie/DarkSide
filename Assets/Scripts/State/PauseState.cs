using UnityEngine;

namespace HackedDesign
{
    public class PauseState : IState
    {

        public bool PlayerActionAllowed => false;

        private UI.AbstractPresenter pausePresenter;

        public PauseState(UI.AbstractPresenter pausePresenter)
        {
            this.pausePresenter = pausePresenter;

        }

        public void Begin()
        {
            AudioManager.Instance.PauseMusic();
            Cursor.lockState = CursorLockMode.None;
            this.pausePresenter.Show();
        }

        public void End()
        {
            this.pausePresenter.Hide();
            AudioManager.Instance.PlayMusic();
        }


        public void FixedUpdate()
        {

        }

        public void LateUpdate()
        {
            
        }


        public void Start()
        {

        }

        public void Select()
        {

        }

        public void Update()
        {

        }
    }
}