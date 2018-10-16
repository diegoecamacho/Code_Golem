using CodeGolem.Level;
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
                   LevelManager.Player.GetStats().Strength++;
                   break;
              
               case StatsType.CONSTITUTION:
                   LevelManager.Player.GetStats().Constitution++;
                   break;
              
               case StatsType.INTELLIGENCE:
                   LevelManager.Player.GetStats().Intelligence++;
                   break;
              
               case StatsType.DEFENSE:
                   LevelManager.Player.GetStats().Defense++;
                   break;
              
               default:
                   break;
            }
        }
    }
}