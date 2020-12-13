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
            GameManager.Instance.SaveGame();
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlaySuccessMusic();
            GameManager.Instance.EntityPool.DestroyEntities();
            GameManager.Instance.Data.score += GameManager.Instance.Data.currentLevelScore;
            GameManager.Instance.Data.currentLevelScore = 0;
            GameManager.Instance.RunStarted = false;
            levelOverPresenter.Show();
            Cursor.lockState = CursorLockMode.None;
            
        }

        public void End()
        {
            GameManager.Instance.NextLevel();
            levelOverPresenter.Hide();
            AudioManager.Instance.StopMusic();
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