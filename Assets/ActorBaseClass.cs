using UnityEngine;
using CodeGolem.Enviroment;
using CodeGolem.StateController;
using UnityEngine.Serialization;

namespace CodeGolem.Actor
{
    public abstract class ActorBase<T> : MonoBehaviour, IDamageable, IInteract where T : ActorStats
    {
        protected StateMachine StateMachine;

        [Header("Character Stats")]
        [SerializeField] protected T ActorStats;

        public T GetStats() 
        {
            return ActorStats;
        }

        public abstract void Attack();

        public abstract void TakeDamage(float damage);

        public abstract void Interact();
    }
}