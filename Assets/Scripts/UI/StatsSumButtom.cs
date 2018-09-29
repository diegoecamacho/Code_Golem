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
                   StatsMenu.focusCharacter.ActorStats.CharacterStats.Strength++;
                   break;
              
               case StatsType.CONSTITUTION:
                   StatsMenu.focusCharacter.ActorStats.CharacterStats.Constitution++;
                   break;
              
               case StatsType.INTELLIGENCE:
                   StatsMenu.focusCharacter.ActorStats.CharacterStats.Intelligence++;
                   break;
              
               case StatsType.DEFENSE:
                   StatsMenu.focusCharacter.ActorStats.CharacterStats.Defense++;
                   break;
              
               default:
                   break;
            }
        }
    }
}