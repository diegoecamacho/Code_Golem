using UnityEngine;
using WeaponSystem;

public class Boomerang : WeaponBaseScript
{
    bool m_InitBullet = false;

    public override void Fire()
    {
        m_InitBullet = true; 
    }

    private void FixedUpdate()
    {
        if (m_InitBullet)
        {
            transform.Translate(Vector3.forward * 3);

        }
    }


}