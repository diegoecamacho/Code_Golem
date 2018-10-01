using UnityEngine;
using CodeGolem.Combat;
using CodeGolem.UI;

public class ExplosiveBulletInterface : SkillComponent {

    [Header("Projectile Components")]
    [SerializeField] private float m_projectileSpeed;

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
        abilityIcon.buttomImage.sprite = imageSprite;
        abilityIcon.coolDownDuration = coolDown;
    }

    public override void Enable()
    {
        if (!IsActive())
        {
            projectileGunBehaviour.EnableSkill();
            abilityIcon.EnableIconEffect();
        }
    }

    private bool IsActive()
    {
        Debug.Log(abilityIcon.IsActive() || projectileGunBehaviour.IsActive());
        return (abilityIcon.IsActive() || projectileGunBehaviour.IsActive());
    }

    public override void Use(SkillParam skillParam)
    {
        throw new System.NotImplementedException();
    }
}
