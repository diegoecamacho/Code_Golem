using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "WeaponBase", menuName = "Weapon/WeaponBase", order = 1)]
    public class WeaponObject : ScriptableObject
    {
        [Header("Weapon Stats:")]
        public string weaponName;

        public WeaponType weaponType;

        public float Damage = 0;

        [Header("Instance:")]
        public GameObject objectPrefab;

        private WeaponBaseScript weapon;

        public WeaponBaseScript WeaponScript
        {
            get
            {
                return weapon;
            }
        }

        private void Awake()
        {
            if (objectPrefab.GetComponent<WeaponBaseScript>() != null)
            {
                weapon = objectPrefab.GetComponent<WeaponBaseScript>();
            }
        }

        public GameObject Instantiate(Vector3 position, Quaternion rotation)
        {
           return Instantiate(objectPrefab, position, rotation);
        }

        public void ReuseBullet(Vector3 position, Quaternion rotation)
        {
            //_transform.position = position;
            //_transform.rotation = rotation;

            objectPrefab.SetActive(true);

            //TODO: Reset Bullet
        }

        public void SetParent(Transform parent)
        {
            //_transform.parent = parent;
        }
    }
}