using UnityEngine;

namespace CodeGolem.Combat
{
    public class ProjectileGunBehaviour : MonoBehaviour, ISkillInterface
    {
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
                activeTime += Time.deltaTime;
                
                if (activeTime >= projectileGun.coolDown)
                {
                    SkillCooldown();
                }
            }
        }

        private void FixedUpdate()
        {
            if (!useSkill || !skillActive) return;
            var bulClone = Instantiate(projectileGun.bulletPrefab, skillParam.Actor.SpawnPoint.position , Quaternion.identity, null).GetComponent<Rigidbody>();
            var bulDir = skillParam.target - skillParam.Actor.transform.position;
            bulClone.AddForce(bulDir * projectileGun.m_projectileSpeed * Time.deltaTime, ForceMode.Impulse);

            Destroy(bulClone.gameObject, 5.0f);
            useSkill = false;
        }

        private void SkillCooldown()
        {
            projectileGun.AbilityIcon.ActivateIconCoolDown();
            skillActive = false;
            activeTime = 0;
        }

        public void EnableSkill()
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

        public void UseSkill(SkillParam skillParams)
        {
            skillParam = skillParams;
            useSkill = true;
        }
    }
}