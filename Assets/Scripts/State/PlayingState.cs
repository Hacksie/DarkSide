using UnityEngine;

namespace HackedDesign
{
    public class PlayingState : IState
    {
        private PlayerController player;
        private EntityPool pool;
        private UI.AbstractPresenter hudPresenter;

        public bool PlayerActionAllowed => true;

        public PlayingState(PlayerController player, EntityPool pool, UI.AbstractPresenter hudPresenter)
        {
            this.player = player;
            this.pool = pool;
            this.hudPresenter = hudPresenter;
        }


        public void Begin()
        {
            this.hudPresenter.Show();
            this.player.Reset();
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void End()
        {
            this.hudPresenter.Hide();
        }

  
        public void FixedUpdate()
        {

        }

        public void LateUpdate()
        {
            this.player.LateUpdateBehaviour();
            this.pool.UpdateLateBehaviour();
            this.hudPresenter.Repaint();
        }

   
        public void Start()
        {
            
        }

        public void Select()
        {

        }

        public void Update()
        {
            EnergyRegen();
            
            if (GameManager.Instance.RunStarted)
            {
                GameManager.Instance.Data.timer -= Time.deltaTime;
                if(GameManager.Instance.Data.timer <= 0 && !GameManager.Instance.GameSettings.infinity)
                {
                    //Logger.Log("Playing State", "Game Over");
                    GameManager.Instance.SetTimeOver();
                }
            }
            this.player.UpdateBehaviour();
            this.pool.UpdateBehaviour();
        }

        private void EnergyRegen()
        {
            GameManager.Instance.ConsumeEnergy(-1 * GameManager.Instance.GameSettings.energyBaseRegen * Time.deltaTime);
        }
    }
}