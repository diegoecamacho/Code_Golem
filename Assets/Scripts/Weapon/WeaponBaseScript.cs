using UnityEngine;
using System;

namespace WeaponSystem
{
    /// <summary>
    /// Weapon Base Class
    /// Inherit from this to begin creating weapon
    /// </summary>
    public class WeaponBaseScript : MonoBehaviour
    {
        /// <summary>Default fire method. Must be overriden.</summary>
        public virtual void Fire()
        {
            Debug.Log("Hello");
            throw new Exception("You are calling the default fire method. Implenent it in your derived class");
        }

    }

    
}