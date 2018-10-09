using CodeGolem.UI;
using System;
using UnityEngine;

namespace CodeGolem.Combat
{
    [CreateAssetMenu(menuName = "Skills/ProjectileGun")]
    public class ProjectileGunInterface : SkillComponent
    {
        [Header("Projectile Components")]
         public float m_projectileSpeed;
         public GameObject bulletPrefab;

        private ISkillInterface projectileGunBehaviour;

        private AbilityIcon abilityIcon;

        public AbilityIcon AbilityIcon
        {
            get
            {
                return abilityIcon;
            }
        }

        public override ISkillInterface GetBehaviour()
        {
            return projectileGunBehaviour;
        }

        public override void AddComponent(GameObject objToAdd)
        {
            projectileGunBehaviour = objToAdd.AddComponent<ProjectileGunBehaviour>();
            projectileGunBehaviour.SetConfig(this);
        }

        public override void RegisterSkill(AbilityIcon icon)
        {
            abilityIcon = icon;

            if (!abilityIcon)
            {
                throw new ArgumentNullException("Failed to Register Skill");
            }

            abilityIcon.buttomImage.sprite = imageSprite;
            abilityIcon.coolDownDuration = coolDown;
           
        }

        public override void Enable()
        {
            if (!IsActive())
            {
                //abilityIcon.ActivateSkill();
                projectileGunBehaviour.EnableSkill();
                abilityIcon.EnableIconEffect();
            }
        }

        public override void Use(SkillParam skillParam)
        {
            projectileGunBehaviour.UseSkill(skillParam);
        }

        private bool IsActive()
        {
            Debug.Log(abilityIcon.IsActive() || projectileGunBehaviour.IsActive());
            return (abilityIcon.IsActive() || projectileGunBehaviour.IsActive());
        }
    }
}