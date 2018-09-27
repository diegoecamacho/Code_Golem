using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeGolem_WeaponSystem;
using System;

namespace CodeGolem_UI {


    public class UIController : MonoBehaviour {

        [SerializeField] List<AbilityIcon> AbilitySlots;

        public void InitializePlayerUI(List<AbilityBase> abilityList)
        {
            if (AbilitySlots == null)
            {
                Debug.LogError("No Ability Slots, Failed to Initialize");
                return;
            }

          //for (var i = 0; i < abilityList.Count; i++)
          //{
          //    AbilitySlots[i].Initialize(abilityList[i]);
          //    AbilitySlots[i].AbilityEnable();
          //}
        }

   
    }
}
