using CodeGolem.Combat;
using CodeGolem.UI;
using System.Collections.Generic;
using UnityEngine;

namespace CodeGolem.Actor
{
    [CreateAssetMenu(fileName = "PlayerStatsTemplate", menuName = "Character/Stats", order = 1)]
    public class PlayerStats : ActorStats
    {
        public delegate void VitalUIUpdater(PlayerStats actor);
        public static event VitalUIUpdater VitalUISEvent;

        public delegate void StatsUIUpdater(PlayerStats actor);
        public static event StatsUIUpdater StatsUIEvent;

        [Header("Player Only Stats")]
        [SerializeField] protected int dashAmount = 4;

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

        public float Experience
        {
            get
            {
                return experience;
            }
            set
            {
                experience = value;
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

        public float ExperiencetoNextLevel
        {
            get
            {
                return experienceToNextLevel;

            }
            set
            {
                experienceToNextLevel = value;
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

        public override void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}