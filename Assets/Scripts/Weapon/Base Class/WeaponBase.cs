using UnityEngine;

namespace CodeGolem_WeaponSystem
{
    /// <summary>
    /// Weapon Base Class
    /// Inherit from this to begin creating weapon
    /// </summary>
    public abstract class WeaponBase : MonoBehaviour
    {
        /// <summary>
        /// Initialize Weapon from Scriptable Object
        /// </summary>
        /// <param name="obj">Gameobject to initialize</param>
       // public abstract void Initialize();

        /// <summary>Default fire method. Must be overridden.</summary>
        public virtual void Fire() { }
        /// <summary>Default fire method. Must be overridden.</summary>
        public virtual void Fire(Transform _transform) { }
        /// <summary>Default fire method. Must be overridden.</summary>
        public virtual void Fire(Vector3 position) { }

    }

    
}