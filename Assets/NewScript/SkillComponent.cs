using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeGolem_UI;

namespace CodeGolem.Combat
{

    struct SkillParam
    {
        Vector3 target;

        SkillParam(Vector3 destination)
        {
            target = destination;
        }
    }

    public interface ISkillInterface
    {
        void Use();
        void SetConfig(SkillComponent skillConfig);
    }

    public abstract class SkillComponent : ScriptableObject {
    
        [Header("Core Components")]
        public Image imageSprite;
        public float coolDown;
        public float mpCost;
        public float Damage;

        public abstract void AddComponent(GameObject objToAdd);
        public abstract void RegisterSkill(AbilityIcon icon);
        public abstract void Use();
    }

    
}