using UnityEngine;
using UnityEngine.Serialization;

namespace CodeGolem.Actor
{
    public abstract class ActorStats : ScriptableObject
    {
        [Header("Core Stats")]
        [SerializeField] public int Level = 1;

        public float Health = 100f;
        public float ManaPoints = 100f;
        public float Experience = 0f;
        public float ExperienceToNextLevel;

        [Header("Fixed Values")]
        public float TotalHealth = 100;

        public float TotalMana = 100f;

        [Header("Stats")]
        public float BaseDamage;

        public float ApproachDistance;

        public int Strength = 10;
        public int Constitution = 10;
        public int Intelligence = 10;
        public int Defense = 10;

        public int GetLevel()
        {
            return Level;
        }

        public float GetDamage()
        {
            return BaseDamage;
        }

        public abstract void TakeDamage(float damage);

    }
}