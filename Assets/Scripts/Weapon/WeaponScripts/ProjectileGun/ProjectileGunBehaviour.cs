using UnityEngine;

namespace CodeGolem.Combat
{
    public class ProjectileGunBehaviour : MonoBehaviour, ISkillInterface
    {
        private ProjectileGunInterface projectileGun;

        [Header("Instance Specific")]
        private bool skillInUse = false;

        private float activeTime = 0;

        GameObject pointer;

        // Update is called once per frame
        private void Update()
        {
            if (skillInUse)
            {
                activeTime += Time.deltaTime;
                if (pointer == null)
                {
                    pointer = Instantiate(Resources.Load("MousePointer")) as GameObject;
                }
                if (activeTime >= projectileGun.coolDown)
                {
                    if (pointer)
                    {
                        Destroy(pointer);
                    }
                    SkillCooldown();
                }
            }
        }

        void LateUpdate()
        {
            if (skillInUse)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray , out hit))
                {
                    pointer.transform.position = new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z);
                    Vector3 hitPointatLevel = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    if (Input.GetButtonDown("PlayerActive"))
                    {
                      // Rigidbody bulletClone = Instantiate(projectileGun.bulletPrefab,;
                    }
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
}