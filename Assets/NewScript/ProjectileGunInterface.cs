using System.Collections;
using System.Collections.Generic;
using CodeGolem_UI;
using UnityEngine;

namespace CodeGolem.Combat
{
    [CreateAssetMenu(menuName = "Skills/ProjectileGun")]
    public class ProjectileGunInterface : SkillComponent
    {
        [Header("Projectile Components")]
        [SerializeField] float m_projectileSpeed;

        ISkillInterface projectileGunBehaviour;

        private AbilityIcon abilityIcon;
        public AbilityIcon AbilityIcon
        {
            get
            {
                return abilityIcon;
            }
        }

        public override void AddComponent(GameObject objToAdd)
        {
            projectileGunBehaviour = objToAdd.AddComponent<ProjectileGunBehaviour>();
            projectileGunBehaviour.SetConfig(this);
        }

        public override void RegisterSkill(AbilityIcon icon)
        {
            abilityIcon = icon;
            abilityIcon.buttomImage.sprite = imageSprite;
            abilityIcon.coolDownDuration = coolDown;
        }

        public override void Use()
        {
            if (!IsActive())
            {
                //abilityIcon.ActivateSkill();
                projectileGunBehaviour.Use();
                abilityIcon.EnableIconEffect();
            }
        }



        bool IsActive()
        {
            Debug.Log(abilityIcon.IsActive() || projectileGunBehaviour.IsActive());
            return (abilityIcon.IsActive() || projectileGunBehaviour.IsActive());
        }

    }
}
