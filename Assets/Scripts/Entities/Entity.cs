using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

namespace HackedDesign
{
    public abstract class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] protected NavMeshAgent agent;

        //public EntityState state = EntityState.Passive;
        protected Animator animator = null;
        protected bool fire = false;

        protected Vector2 velocity;
        protected Vector2 direction;

        public EntityState State { get; protected set; }

        protected void Awake()
        {
            this.animator = GetComponent<Animator>();
        }

        public virtual void Hit(int boltAmount, int energyAmount)
        {
            //hitEvent.Invoke(boltAmount, energyAmount);
            //state = EntityState.Dead;
            //this.gameObject.SetActive(false);
        }

        public virtual void Alert()
        {
 
        }

        public virtual void UpdateBehaviour()
        {

        }

        public virtual void UpdateLateBehaviour()
        {
            Animate();
        }

        protected virtual void Animate()
        {
            if (animator != null)
            {
                animator.SetFloat("velocity", agent.velocity.magnitude);
            }
        }
    }

    public enum EntityState
    {
        Passive,
        Tracking,
        Angry,
        Avoiding,
        Dead
    }
    
}