using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "WeaponBase", menuName = "Weapon/WeaponBase", order = 1)]
    public class BulletInstance : ScriptableObject
    {
        public GameObject objectPrefab;

        public WeaponType weaponType;

        private Transform m_transform;

        private WeaponBaseScript weapon;

        public BulletInstance(GameObject prefab)
        {
            objectPrefab = prefab;
            m_transform = prefab.transform;

            objectPrefab.SetActive(false);

            if (objectPrefab.GetComponent<WeaponBaseScript>() != null)
            {
                weapon = objectPrefab.GetComponent<WeaponBaseScript>();
            }
        }

        public void ReuseBullet(Vector3 position, Quaternion rotation)
        {
            m_transform.position = position;
            m_transform.rotation = rotation;

            objectPrefab.SetActive(true);

            //TODO: Reset Bullet
        }

        public void SetParent(Transform parent)
        {
            m_transform.parent = parent;
        }
    }
}