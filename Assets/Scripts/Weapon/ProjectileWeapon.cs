using System;
using UnityEngine;
namespace CodeGolem_WeaponSystem
{

    public class ProjectileWeapon : WeaponBase
    {
        bool skillActive = false;

        [HideInInspector]
        public float projectileSpeed = 100;

        [HideInInspector]
        public GameObject projectile;

        Rigidbody _rb;

        Vector3 destinationPoint;

        [Range(3, 10)]
        float deadZone = 3;

        [SerializeField] GameObject PointerArrow;
         Vector3 gizmoPosition;

         GameObject pointer;

        float currentTime;
        float totalActiveTime;
        float activeTime = 10f;

        Vector3 clickPoint;

        //public override void Fire(Vector3 _transform)
        //{
        //    destinationPoint = new Vector3(_transform.x, 0, _transform.z);
        //    var targetPosition = _transform - transform.position;
        //    targetDir = new Vector3(targetPosition.x, 0, targetPosition.z);
        //    skillActive = !skillActive;
        //}

        internal void LaunchSkill()
        {
            skillActive = true;
            totalActiveTime = Time.time + activeTime;

            if (pointer == null)
            {
                pointer = Instantiate(PointerArrow);
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

        private void FixedUpdate()
        {
            if (skillActive)
            {
                bool activate = (Time.time < totalActiveTime);
                if (activate)
                {
                    Vector3 hitPoint = GetHitPoint();
                    pointer.transform.position = new Vector3(hitPoint.x, hitPoint.y + 0.2f, hitPoint.z);
                    if (Input.GetButtonDown("PlayerActive"))
                    {
                        Rigidbody bulletClone = Instantiate(projectile, transform.position, transform.rotation).GetComponent<Rigidbody>();
                        Vector3 bulletDir = Vector3.Normalize(hitPoint - transform.position);
                        bulletClone.AddForce(bulletDir  * projectileSpeed);
                    }
                }
                else
                {
                    Destroy(pointer);
                    skillActive = false;
                }
            }
        }
    }
}