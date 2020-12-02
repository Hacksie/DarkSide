using UnityEngine;

namespace HackedDesign
{
    public class PlayingState : IState
    {
        private PlayerController player;

        public bool PlayerActionAllowed => true;

        public PlayingState(PlayerController player)
        {
            this.player = player;
        }


        public void Begin()
        {

        }

        public void End()
        {

        }

        public void EndDialog()
        {
            throw new System.NotImplementedException();
        }

        public void FixedUpdate()
        {

        }

        public void LateUpdate()
        {

        }

        public void ShowDialog()
        {
            throw new System.NotImplementedException();
        }

        public void Start()
        {
            throw new System.NotImplementedException();
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