using UnityEngine;

namespace HackedDesign
{
    public class LevelOverState : IState
    {
        private PlayerController player;
        private UI.AbstractPresenter levelOverPresenter;

        public bool PlayerActionAllowed => false;

        public LevelOverState(PlayerController player, UI.AbstractPresenter levelOverPresenter)
        {
            this.player = player;
            this.levelOverPresenter = levelOverPresenter;
        }


        public void Begin()
        {
            //GameManager.Instance.LoadLevel();
            levelOverPresenter.Show();
            Cursor.lockState = CursorLockMode.None;
        }

        public void End()
        {
            levelOverPresenter.Hide();
        }

  
        public void FixedUpdate()
        {

        }

        public void LateUpdate()
        {
            levelOverPresenter.Repaint();
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