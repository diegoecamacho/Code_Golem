using System;
using UnityEngine;
namespace CodeGolem_WeaponSystem
{

    public class ProjectileLauncher : WeaponBase
    {
        bool skillActive = false;

        bool m_inCoolDown = false;

        public bool SkillActive
        {
            get
            {
                return skillActive;
            }
        }

        public bool InCoolDown
        {
            get
            {
                return m_inCoolDown;
            }
        }

        [HideInInspector]
        public float projectileSpeed = 0;

        [HideInInspector]
        public GameObject projectile;

        [HideInInspector]
        public float bulletDamage = 0;

        [HideInInspector]
        public float activeTime = 0;

        [HideInInspector]
        public float coolDownTime = 0;


        [SerializeField] GameObject PointerArrow;

         GameObject pointer;

        float totalActiveTime = 0;

        float totalCoolDownTime = 0;

        internal void LaunchSkill()
        {
            skillActive = true;
            m_inCoolDown = true;
            totalActiveTime = Time.time + activeTime;
            totalCoolDownTime = Time.time + activeTime + coolDownTime;

            if (pointer == null)
            {
                pointer = Instantiate(PointerArrow);
            }
        }

        private void Update()
        {
            if (m_inCoolDown == true)
            {
                if (Time.time > totalCoolDownTime)
                {
                    m_inCoolDown = false;
                }
            }
        }

        private void FixedUpdate()
        {
            if (skillActive)
            {
                bool activate = (Time.time < totalActiveTime );
                if (activate)
                {

                    Vector3 hitPoint = GetHitPoint();
                    pointer.transform.position = new Vector3(hitPoint.x, hitPoint.y + 0.2f, hitPoint.z);
                    Vector3 hitPointatLevel = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
                    if (Input.GetButtonDown("PlayerActive"))
                    {
                         ProjectileBase bulletClone = Instantiate(projectile, transform.position, transform.rotation).GetComponent<ProjectileBase>();
                         bulletClone.Initialize(hitPointatLevel , projectileSpeed, bulletDamage);
  
                    }
                }
                else
                {
                    Destroy(pointer);
                    totalCoolDownTime = Time.time + coolDownTime;
                    skillActive = false;
                }
            }
        }

        Vector3 GetHitPoint()
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                return hit.point;
            }

            return Vector3.zero;

        }
    }
}