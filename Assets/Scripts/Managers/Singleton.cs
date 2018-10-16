using UnityEngine;

namespace CodeGolem.Managers
{
    /// <summary>
    /// Singleton Class Creates a singleton out of the template type.
    /// </summary>
    /// <typeparam name="T">Singleton Class</typeparam>
    public class Singleton<T> : MonoBehaviour where T: Component
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance != null) return instance;

                instance = FindObjectOfType<T>();
                if (instance != null) return instance;

                var obj = new GameObject("Raycast Manager");
                instance = obj.AddComponent<T>();
                return instance;

            }
            set { instance = value; }
        }
    }
}
