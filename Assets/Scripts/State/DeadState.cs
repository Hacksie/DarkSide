using UnityEngine;

namespace HackedDesign
{
    public class DeadState : IState
    {

        public bool PlayerActionAllowed => false;

        private UI.AbstractPresenter deadPresenter = null;

        public DeadState(UI.AbstractPresenter deadPresenter)
        {
            this.deadPresenter = deadPresenter;

        }

        public void Begin()
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayDeathMusic();
            if (GameManager.Instance.Data.permadeath)
            {
                GameManager.Instance.Data.dead = true;
                GameManager.Instance.SaveGame();
                AudioManager.Instance.PlayLoser();
            }
            else
            {
                AudioManager.Instance.PlayDeath();
            }

            GameManager.Instance.RunStarted = false;
            this.deadPresenter.Show();
            Cursor.lockState = CursorLockMode.None;
        }

        public void End()
        {
            this.deadPresenter.Hide();
            AudioManager.Instance.StopMusic();
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