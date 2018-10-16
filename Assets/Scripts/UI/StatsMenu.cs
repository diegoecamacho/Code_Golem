using CodeGolem.Level;
using CodeGolem.Player;
using UnityEngine;
using UnityEngine.UI;

namespace CodeGolem.UI
{
    public class StatsMenu : MonoBehaviour
    {
        public static PlayerController focusCharacter;

        [Header("UI Elements")]
        public Text m_LevelStatsText;

        public Text m_StrengthText;
        public Text m_ConstitutionText;
        public Text m_IntelligenceText;
        public Text m_DefenseText;

        private void Start()
        {
            focusCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            EventManager.onStatsUpdate += UpdateUI;
            UpdateUI();
        }

        private void UpdateUI()
        {
            var playerStats = LevelManager.Player;

            m_LevelStatsText.text = playerStats.GetStats().GetLevel().ToString();
            m_StrengthText.text = playerStats.GetStats().Strength.ToString();
            m_IntelligenceText.text = playerStats.GetStats().Intelligence.ToString();
            m_ConstitutionText.text = playerStats.GetStats().Constitution.ToString();
            m_DefenseText.text = playerStats.GetStats().Defense.ToString();
        }
    }
}