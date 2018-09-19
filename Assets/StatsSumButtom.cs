using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menus
{
    public class StatsSumButtom : MonoBehaviour
    {
        public enum StatsType
        {
            STRENGTH,
            CONSTITUTION,
            INTELLIGENCE,
            DEFENSE
        }

        public StatsType SumStat;

        public void OnButtomClick()
        {
            switch (SumStat)
            {
                case StatsType.STRENGTH:
                    StatsMenu.focusCharacter.GetStatsCard()["Strength"]++;
                    break;
                case StatsType.CONSTITUTION:
                    StatsMenu.focusCharacter.GetStatsCard()["Constitution"]++;
                    break;
                case StatsType.INTELLIGENCE:
                    StatsMenu.focusCharacter.GetStatsCard()["Intelligence"]++;
                    break;
                case StatsType.DEFENSE:
                    StatsMenu.focusCharacter.GetStatsCard()["Defense"]++;
                    break;
                default:
                    break;
            }
        }
    }
}
