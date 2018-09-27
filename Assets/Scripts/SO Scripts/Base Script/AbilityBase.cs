using UnityEngine;

namespace CodeGolem_WeaponSystem
{
    public abstract class AbilityBase : ScriptableObject
    {
        //public string m_Name;
        public Sprite m_Sprite;
        public float m_baseCoolDown = 1f;
        public bool allowMovement = false;

        PlayerController owner;


        public abstract void Initialize(Transform obj);

        public abstract void ActivateSkill();

        public abstract bool GetActive();

        public abstract bool InCoolDown();
  
    }
}