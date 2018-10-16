using CodeGolem.Actor;
using CodeGolem.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace CodeGolem.UI
{
    /// <summary>
    /// UI Manager Controls All UI Components
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        [Header("Image Components")]
        [SerializeField] private Image healthImage;

        [SerializeField] private Image manaImage;
        [SerializeField] private Image[] dashImages;

        [SerializeField] private AbilityIcon[] abilityIcons;

        [SerializeField] private Slider experienceSlider;

        [SerializeField] private GameObject pauseMenu;

        public bool ActiveUI = false;

        private void Start()
        {
            Debug.Assert(pauseMenu != null, "Missing Pause Menu");

            PlayerStats.VitalUiEvent += UpdateHealth;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Menu/Esc"))
            {
                EnableMenu();
            }
        }

        /// <summary>
        /// Enable/Disable Menu UI
        /// </summary>
        public void EnableMenu()
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            ActiveUI = pauseMenu.activeSelf;
        }

        private void UpdateHealth(PlayerStats actorStats)
        {
            Debug.Log("UpdateHealth");
            this.healthImage.fillAmount = actorStats.Health / actorStats.TotalHealth;
            manaImage.fillAmount = actorStats.ManaPoints / actorStats.TotalMana;
            experienceSlider.value = actorStats.Experience / actorStats.ExperienceToNextLevel;
            for (var i = 0; i < dashImages.Length; i++)
            {
                dashImages[i].enabled = i < actorStats.DashAmount;
            }
        }
    }
}