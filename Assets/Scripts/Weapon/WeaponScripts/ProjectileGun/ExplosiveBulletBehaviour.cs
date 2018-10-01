using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeGolem.Combat;

public class ExplosiveBulletBehaviour : MonoBehaviour , ISkillInterface {
    private ProjectileGunInterface projectileGun;

    [Header("Instance Specific")]
    private bool skillActive = false;
    private bool useSkill = false;

    private float activeTime = 0;

    SkillParam skillParam;

    // Update is called once per frame
    private void Update()
    {
        if (skillActive)
        {
            Debug.Log("Skill Active");
            activeTime += Time.deltaTime;
            if (activeTime >= projectileGun.coolDown)
            {
                SkillCooldown();
            }
        }
    }

    void FixedUpdate()
    {
        if (useSkill && skillActive)
        {
            Debug.LogWarning("Weapon Behaviour not Implemented!");
        }
    }

    private void SkillCooldown()
    {
        projectileGun.AbilityIcon.ActivateIconCoolDown();
        skillActive = false;
        activeTime = 0;
    }

    public void Use()
    {
        skillActive = true;
    }

    public bool IsActive()
    {
        return skillActive;
    }

    public void SetConfig(SkillComponent skillConfig)
    {
        projectileGun = (ProjectileGunInterface)skillConfig;
    }

    public void DestroyComponent()
    {
        Destroy(GetComponent<ProjectileGunBehaviour>());
    }

    public void EnableSkill()
    {
        skillActive = true;
    }

    public void UseSkill(SkillParam skillParams)
    {
        skillParam = skillParams;
        useSkill = true;
    }
}
