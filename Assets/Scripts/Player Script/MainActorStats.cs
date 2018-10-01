using CodeGolem.Combat;
using CodeGolem.UI;
using System.Collections.Generic;
using UnityEngine;

namespace CodeGolem.Player
{
    [CreateAssetMenu(fileName = "MainActorStatsTemplate", menuName = "Character/Stats", order = 1)]
    public class MainActorStats : ScriptableObject
    {
        public delegate void VitalUIUpdater(MainActorStats actor);
        public static event VitalUIUpdater VitalUISEvent;

        public delegate void StatsUIUpdater(MainActorStats actor);
        public static event StatsUIUpdater StatsUIEvent;

        [Header("Core Stats")]
        [SerializeField] private int level = 1;
        [SerializeField] private float health = 100f;
        [SerializeField] private float manaPoints = 100f;
        [SerializeField] private  int  dashAmount = 4;

     
        private float totalHealth = 100;
        private float totalMana = 100f;

        [Header("Player Stats")]
        [SerializeField] private int strength = 10;
        [SerializeField] private int constitution = 10;
        [SerializeField] private int intelligence = 10;
        [SerializeField] private int defense = 10;

        [Header("Player Skills")]
        [SerializeField] private List<SkillComponent> playerSkills = new List<SkillComponent>();

        public float Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
                VitalUISEvent(this);
            }
        }

        public float ManaPoints
        {
            get
            {
                return manaPoints;
            }

            set
            {
                manaPoints = value;
                VitalUISEvent(this);
            }
        }

        public int DashAmount
        {
            get
            {
                return dashAmount;
            }

            set
            {
                dashAmount = value;
                VitalUISEvent(this);
            }
        }

        public float TotalHealth
        {
            get
            {
                return totalHealth;
            }
        }

        public float TotalMana
        {
            get
            {
                return totalMana;
            }
        }

        public int Strength
        {
            get { return strength; }
            set
            {
                strength = value;
                if (EventManager.onStatsUpdate != null)
                {
                    EventManager.onStatsUpdate();
                }
            }
        }

        public int Constitution
        {
            get { return constitution; }

            set
            {
                constitution = value;
                if (EventManager.onStatsUpdate != null)
                {
                    EventManager.onStatsUpdate();
                }
            }
        }

        public int Intelligence
        {
            get { return intelligence; }

            set
            {
                intelligence = value;
                if (EventManager.onStatsUpdate != null)
                {
                    EventManager.onStatsUpdate();
                }
            }
        }

        public int Defense
        {
            get { return defense; }
            set
            {
                defense = value;
                if (EventManager.onStatsUpdate != null)
                {
                    EventManager.onStatsUpdate();
                }
            }
        }

        public List<SkillComponent> PlayerSkills
        {
            get
            {
                return playerSkills;
            }
        }

        public void RegisterSkill(SkillComponent skill, AbilityIcon icon)
        {
            skill.RegisterSkill(icon);
            if (playerSkills.Contains(skill))
            {
                return;
            }
            if (PlayerSkills.Count < 6) // #TODO: Make Global somehow (MAX NUMBER OF SKILLS)
            {
                playerSkills.Add(skill);
            }
        }

        public void EnableSkill(int Input, GameObject actor)
        {
            if (actor.GetComponent<ISkillInterface>() == null)
            {
                PlayerSkills[Input].AddComponent(actor);
            }
            PlayerSkills[Input].Enable();
        }

        public void UseSkill(int Input, SkillParam skillParam)
        {
            if (skillParam.Actor.GetComponent<ISkillInterface>() == null)
            {
                PlayerSkills[Input].AddComponent(skillParam.Actor.gameObject);
                PlayerSkills[Input].Use(skillParam);
            }
            else
            {
                if (skillParam.Actor.GetComponent<ISkillInterface>() != PlayerSkills[Input].GetBehaviour())
                {
                    skillParam.Actor.GetComponent<ISkillInterface>().DestroyComponent();
                    PlayerSkills[Input].AddComponent(skillParam.Actor.gameObject);
                    PlayerSkills[Input].Use(skillParam);
                }
                else
                {
                    PlayerSkills[Input].Use(skillParam);
                }
            }
        }

        public int GetPlayerLevel()
        {
            return level;
        }

        public static MainActorStats operator +(MainActorStats lhs, MainActorStats rhs)
        {
            MainActorStats stats = new MainActorStats
            {
                Strength = lhs.Strength + rhs.Strength,
                Constitution = lhs.Constitution + rhs.Constitution,
                Intelligence = lhs.Intelligence + rhs.Intelligence,
                Defense = lhs.Defense + rhs.Defense
            };

            EventManager.onStatsUpdate();

            return stats;
        }
    }
}