using UnityEngine;
using CodeGolem.Enviroment;

namespace CodeGolem.Actor
{
    public abstract class ActorBase : MonoBehaviour, IDamageable, IInteract
    {
        [SerializeField] private ActorStats actorStats;

        protected const float approachRange = 1.5F;

        public ActorStats ActorStats
        {
            get
            {
                return actorStats;
            }

            set
            {
                actorStats = value;
            }
        }

        public abstract void Attack();

        public abstract void TakeDamage(float damage);

        public abstract void Interact();
    }
}