using UnityEngine;

namespace CodeGolem.Actor
{
    public abstract class ActorStats : ScriptableObject
    {
        [Header("Core Stats")]
        [SerializeField] protected int level = 1;

        [SerializeField] protected float health = 100f;
        [SerializeField] protected float manaPoints = 100f;
        [SerializeField] protected float experience = 0f;
        [SerializeField] protected float experienceToNextLevel;

        [Header("Fixed Values")]
        protected float totalHealth = 100;

        protected float totalMana = 100f;

        [Header("Stats")]
        [SerializeField] protected float baseDamage;

        [SerializeField] protected int strength = 10;
        [SerializeField] protected int constitution = 10;
        [SerializeField] protected int intelligence = 10;
        [SerializeField] protected int defense = 10;

        public int GetLevel()
        {
            return level;
        }

        public float GetDamage()
        {
            return baseDamage;
        }

        public abstract void TakeDamage(float damage);

    }
}