using UnityEngine;

namespace CodeGolem.UI
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
                   StatsMenu.focusCharacter.ActorStats.Strength++;
                   break;
              
               case StatsType.CONSTITUTION:
                   StatsMenu.focusCharacter.ActorStats.Constitution++;
                   break;
              
               case StatsType.INTELLIGENCE:
                   StatsMenu.focusCharacter.ActorStats.Intelligence++;
                   break;
              
               case StatsType.DEFENSE:
                   StatsMenu.focusCharacter.ActorStats.Defense++;
                   break;
              
               default:
                   break;
            }
        }
    }
}