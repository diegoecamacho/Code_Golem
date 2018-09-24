using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeGolem_WeaponSystem
{

    public abstract class AbilityBase : ScriptableObject
    {

        public string m_Name;
        public Sprite m_Sprite;
        public float m_baseCoolDown = 1f;

        public abstract void Initialize(GameObject obj);
        public abstract void ActivateSkill();

    }
}