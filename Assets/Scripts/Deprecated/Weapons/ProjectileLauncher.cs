//using UnityEngine;

//namespace CodeGolem.Combat.Deprecated
//{
//    public class ProjectileLauncher : WeaponBase
//    {
//        private bool skillActive = false;

//        private bool m_inCoolDown = false;

//        public bool SkillActive
//        {
//            get
//            {
//                return skillActive;
//            }
//        }

//        public bool InCoolDown
//        {
//            get
//            {
//                return m_inCoolDown;
//            }
//        }

//        [HideInInspector]
//        public float projectileSpeed = 0;

//        [HideInInspector]
//        public GameObject projectile;

//        [HideInInspector]
//        public float bulletDamage = 0;

//        [HideInInspector]
//        public float activeTime = 0;

//        [HideInInspector]
//        public float coolDownTime = 0;

//        [SerializeField] private GameObject PointerArrow;

//        private GameObject pointer;

//        private float totalActiveTime = 0;

//        private float totalCoolDownTime = 0;

//        internal void LaunchSkill()
//        {
//            skillActive = true;
//            m_inCoolDown = true;
//            totalActiveTime = Time.time + activeTime;
//            totalCoolDownTime = Time.time + activeTime + coolDownTime;

//            if (pointer == null)
//            {
//                pointer = Instantiate(PointerArrow);
//            }
//        }

//        private void Update()
//        {
//            if (m_inCoolDown == true)
//            {
//                if (Time.time > totalCoolDownTime)
//                {
//                    m_inCoolDown = false;
//                }
//            }
//        }

//        private void FixedUpdate()
//        {
//            if (skillActive)
//            {
//                bool activate = (Time.time < totalActiveTime);
//                if (activate)
//                {
//                    Vector3 hitPoint = GetHitPoint();
//                    pointer.transform.position = new Vector3(hitPoint.x, hitPoint.y + 0.2f, hitPoint.z);
//                    Vector3 hitPointatLevel = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
//                    if (Input.GetButtonDown("PlayerActive"))
//                    {
//                        //ProjectileBase bulletClone = Instantiate(projectile, transform.position, transform.rotation).GetComponent<ProjectileBase>();
//                        //bulletClone.Initialize(hitPointatLevel , projectileSpeed, bulletDamage);
//                    }
//                }
//                else
//                {
//                    Destroy(pointer);
//                    totalCoolDownTime = Time.time + coolDownTime;
//                    skillActive = false;
//                }
//            }
//        }

//        private Vector3 GetHitPoint()
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;

//            if (Physics.Raycast(ray, out hit))
//            {
//                return hit.point;
//            }

//            return Vector3.zero;
//        }
//    }
//}