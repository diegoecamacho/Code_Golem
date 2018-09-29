using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeGolem.Combat;

public class ExplosiveBulletBehaviour : MonoBehaviour , ISkillInterface {
    private ProjectileGunInterface projectileGun;

    [Header("Instance Specific")]
    private bool skillInUse = false;

    private float activeTime = 0;

    // Update is called once per frame
    private void Update()
    {
        if (skillInUse)
        {
            Debug.Log("Skill Active");
            activeTime += Time.deltaTime;
            if (activeTime >= projectileGun.coolDown)
            {
                SkillCooldown();
            }
        }
    }

    private void SkillCooldown()
    {
        projectileGun.AbilityIcon.ActivateIconCoolDown();
        skillInUse = false;
        activeTime = 0;
    }

    public void Use()
    {
        skillInUse = true;
    }

    public bool IsActive()
    {
        return skillInUse;
    }

    public void SetConfig(SkillComponent skillConfig)
    {
        projectileGun = (ProjectileGunInterface)skillConfig;
    }

    public void DestroyComponent()
    {
        Destroy(GetComponent<ProjectileGunBehaviour>());
    }
}
