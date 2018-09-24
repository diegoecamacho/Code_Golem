using UnityEngine;

namespace CodeGolem_WeaponSystem
{
    [CreateAssetMenu(fileName = "WeaponBase", menuName = "Weapon/Projectile", order = 1)]
    public class ProjectileAsset : AbilityBase
    {
        public float m_bulletSpeed = 1f;
        public float m_Damage = 0;

        [Header("Instance:")]
        public GameObject projectilePrefab;

        ProjectileWeapon launcher;

        public override void Initialize(GameObject obj)
        {
            launcher = obj.GetComponent<ProjectileWeapon>();
            launcher.projectileSpeed = m_bulletSpeed;
            launcher.projectile = projectilePrefab;
        }

        public override void ActivateSkill()
        {
            launcher.LaunchSkill();
        }
    }
}