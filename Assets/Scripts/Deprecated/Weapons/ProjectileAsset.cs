//using UnityEngine;

//namespace CodeGolem.Combat.Deprecated
//{
//    [CreateAssetMenu(fileName = "WeaponBase", menuName = "Weapon/Projectile", order = 1)]
//    public class ProjectileAsset : AbilityBase
//    {
//        public float m_bulletSpeed = 1f;
//        public float m_Damage = 0;

//        public float m_activeDuration = 3;

//        [Header("Instance:")]
//        public GameObject projectileTriggerPrefab;

//        public GameObject projectilePrefab;

//        private GameObject projectileTriggerInstance;
//        private ProjectileLauncher launcher;

//        public override void Initialize(Transform spawnTransform)
//        {
//            projectileTriggerInstance = Instantiate(projectileTriggerPrefab, spawnTransform);
//            launcher = projectileTriggerInstance.GetComponent<ProjectileLauncher>();
//            launcher.projectileSpeed = m_bulletSpeed;
//            launcher.projectile = projectilePrefab;
//            launcher.bulletDamage = m_Damage;
//            launcher.activeTime = m_activeDuration;
//            launcher.coolDownTime = m_baseCoolDown;
//        }

//        public override void ActivateSkill()
//        {
//            launcher.LaunchSkill();
//        }

//        public override bool GetActive()
//        {
//            return launcher.SkillActive;
//        }

//        public override bool InCoolDown()
//        {
//            return launcher.InCoolDown;
//        }
//    }
//}