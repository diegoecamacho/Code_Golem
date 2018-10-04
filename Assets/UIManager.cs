﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeGolem.Actor;

namespace CodeGolem.UI {
    public class UIManager : MonoBehaviour {

        [Header("Image Components")]
        [SerializeField] Image HealthImage;
        [SerializeField] Image ManaImage;
        [SerializeField] Image[] DashImages;

        [SerializeField] AbilityIcon[] abilityIcons;



        private void Start()
        {
            PlayerStats.VitalUISEvent += UpdateHealth;
        }

        private void Update()
        {
           
        }

        void UpdateHealth(PlayerStats actorStats)
        {
            HealthImage.fillAmount = actorStats.Health / actorStats.TotalHealth;
            ManaImage.fillAmount = actorStats.ManaPoints / actorStats.TotalMana;
            for (int i = 0; i < DashImages.Length; i++)
            {
                if (i >= actorStats.DashAmount)
                {
                    DashImages[i].enabled = false;
                    

                }
                else
                {
                    DashImages[i].enabled = true;
                }
            }
        }



    }
}
