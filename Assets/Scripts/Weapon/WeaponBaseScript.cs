using UnityEngine;

namespace WeaponSystem
{
    internal abstract class WeaponBaseScript : MonoBehaviour
    {
        public bool m_InitBullet = false;

        protected virtual void OnEnable()
        {
            m_InitBullet = !m_InitBullet;
        }

        protected virtual void OnDisable()
        {
            m_InitBullet = !m_InitBullet;
        }
    }
}