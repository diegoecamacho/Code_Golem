using UnityEngine;

namespace CodeGolem.Managers
{
    public class Singleton<T> : MonoBehaviour where T: Component
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = FindObjectOfType<T>();
                if (_instance != null) return _instance;

                var obj = new GameObject("Raycast Manager");
                _instance = obj.AddComponent<T>();
                return _instance;

            }
            set { _instance = value; }
        }
    }
}
