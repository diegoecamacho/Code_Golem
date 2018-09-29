﻿using CodeGolem.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
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
            m_LevelStatsText.text = focusCharacter.ActorStats.CharacterStats.GetPlayerLevel().ToString();
            m_StrengthText.text = focusCharacter.ActorStats.CharacterStats.Strength.ToString();
            m_IntelligenceText.text = focusCharacter.ActorStats.CharacterStats.Intelligence.ToString();
            m_ConstitutionText.text = focusCharacter.ActorStats.CharacterStats.Constitution.ToString();
            m_DefenseText.text = focusCharacter.ActorStats.CharacterStats.Defense.ToString();
        }
    }
}