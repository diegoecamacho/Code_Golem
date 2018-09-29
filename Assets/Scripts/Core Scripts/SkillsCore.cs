using CodeGolem.UI;
using UnityEngine;

namespace CodeGolem.Combat
{
    /// <summary>
    /// Skill Parameter Structure
    /// Used to pass in target Information to Skills.
    /// </summary>
    internal struct SkillParam
    {
        private Vector3 target;

        private SkillParam(Vector3 destination)
        {
            target = destination;
        }
    }

    /// <summary>
    /// Skill Interface all skills must inherit from this.
    /// </summary>
    public interface ISkillInterface
    {
        void Use();

        void SetConfig(SkillComponent skillConfig);

        bool IsActive();

        void DestroyComponent();
    }

    /// <summary>
    /// Skill Data Container all skills.
    /// Required: All skills must have a Data Container<Skill Component> and a behavior<ISkillInterface>
    /// </summary>
    public abstract class SkillComponent : ScriptableObject
    {
        [Header("Core Components")]
        public Sprite imageSprite;
        public float coolDown;
        public float mpCost;
        public float Damage;

        /// <summary>
        /// Adds the skill behavior to the Actor GameObject
        /// </summary>
        /// <param name="objToAdd"></param>
        public abstract void AddComponent(GameObject objToAdd);

        /// <summary>
        /// Register skill to specific UI element
        /// </summary>
        /// <param name="icon"></param>
        public abstract void RegisterSkill(AbilityIcon icon);

        /// <summary>
        /// Use function for the skill
        /// </summary>
        public abstract void Use();

        public abstract ISkillInterface GetBehaviour();
    }
}