using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CodeGolem.Combat
{
    public class ProjectileGunBehaviour : MonoBehaviour, ISkillInterface
    {

        ProjectileGunInterface projectileGun;

        [Header("Instance Specific")]
        bool skillInUse = false;
        float timeActive = 0;

        public bool IsActive()
        {
            return skillInUse;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (skillInUse)
            {
                timeActive += Time.deltaTime;
                if (timeActive >= projectileGun.coolDown)
                {
                    SkillCooldown(); 
                }
            }
        }

        public void Use()
        {
            skillInUse = true;
            
        }

        void SkillCooldown()
        {
            projectileGun.AbilityIcon.ActivateIconCoolDown();
            skillInUse = false;
            timeActive = 0;
            
            
        }

        public void SetConfig(SkillComponent skillConfig)
        {
            projectileGun = (ProjectileGunInterface)skillConfig;
        }
    }
}
