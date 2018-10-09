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
            m_LevelStatsText.text = focusCharacter.ActorStats.GetLevel().ToString();
            m_StrengthText.text = focusCharacter.ActorStats.Strength.ToString();
            m_IntelligenceText.text = focusCharacter.ActorStats.Intelligence.ToString();
            m_ConstitutionText.text = focusCharacter.ActorStats.Constitution.ToString();
            m_DefenseText.text = focusCharacter.ActorStats.Defense.ToString();
        }
    }
}