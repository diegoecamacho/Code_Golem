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

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Use()
        {
            Debug.Log("Fire");
        }

        public void SetConfig(SkillComponent skillConfig)
        {
            projectileGun = (ProjectileGunInterface)skillConfig;
        }
    }
}
