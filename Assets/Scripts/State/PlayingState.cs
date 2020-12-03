using UnityEngine;

namespace HackedDesign
{
    public class PlayingState : IState
    {
        private PlayerController player;
        private UI.AbstractPresenter hudPresenter;

        public bool PlayerActionAllowed => true;

        public PlayingState(PlayerController player, UI.AbstractPresenter hudPresenter)
        {
            this.player = player;
            this.hudPresenter = hudPresenter;
        }


        public void Begin()
        {
            GameManager.Instance.LoadLevel();
            hudPresenter.Show();
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void End()
        {
            hudPresenter.Hide();
        }

  
        public void FixedUpdate()
        {

        }

        public void LateUpdate()
        {
            hudPresenter.Repaint();
        }

   
        public void Start()
        {
            
        }


        public void Update()
        {
            if (GameManager.Instance.RunStarted)
            {
                GameManager.Instance.Data.timer -= Time.deltaTime;
                if(GameManager.Instance.Data.timer <= 0)
                {
                    Logger.Log("Playing State", "Game Over");
                }
            }
            this.player.UpdateBehaviour();
        }
    }
}