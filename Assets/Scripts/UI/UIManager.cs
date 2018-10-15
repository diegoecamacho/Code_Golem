using System.Collections;
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

        [SerializeField] Slider ExperienceSlider;



        private void Start()
        {
            PlayerStats.VitalUiEvent += UpdateHealth;
        }

        private void UpdateHealth(PlayerStats actorStats)
        {
            Debug.Log("UpdateHealth");
            HealthImage.fillAmount = actorStats.Health / actorStats.TotalHealth;
            ManaImage.fillAmount = actorStats.ManaPoints / actorStats.TotalMana;
            ExperienceSlider.value = actorStats.Experience / actorStats.ExperienceToNextLevel;
            for (var i = 0; i < DashImages.Length; i++)
            {
                DashImages[i].enabled = i < actorStats.DashAmount;
            }
        }



    }
}
