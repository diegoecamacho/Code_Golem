using UnityEngine;
using CodeGolem.UI;
using CodeGolem.Player;

namespace CodeGolem.Combat
{
    /// <summary>
    /// Skill Parameter Structure
    /// Used to pass in target Information to Skills.
    /// </summary>
    public struct SkillParam
    {
        public PlayerController Actor;
        public Vector3 target;

        public SkillParam(PlayerController ActorGO, Vector3 destination)
        {
            Actor = ActorGO;
            target = destination;
        }
    }

    /// <summary>
    /// Skill Interface all skills must inherit from this.
    /// </summary>
    public interface ISkillInterface
    {
        void EnableSkill();

        void UseSkill(SkillParam skillParams);

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

        public bool allowMovement;

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
        public abstract void Enable();


        public abstract void Use(SkillParam skillParam);


        public abstract ISkillInterface GetBehaviour();
    }
}