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
            this.hudPresenter.Show();
            this.player.Reset();
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
            player.LateUpdateBehaviour();
            hudPresenter.Repaint();
        }

   
        public void Start()
        {
            
        }


        public void Update()
        {
            EnergyRegen();
            

            if (GameManager.Instance.RunStarted && !GameManager.Instance.GameSettings.infinity)
            {
                GameManager.Instance.Data.timer -= Time.deltaTime;
                if(GameManager.Instance.Data.timer <= 0 )
                {
                    //Logger.Log("Playing State", "Game Over");
                    GameManager.Instance.SetTimeOver();
                }
            }
            this.player.UpdateBehaviour();
        }

        private void EnergyRegen()
        {
            GameManager.Instance.ConsumeEnergy(-1 * GameManager.Instance.GameSettings.energyBaseRegen * Time.deltaTime);
        }
    }
}