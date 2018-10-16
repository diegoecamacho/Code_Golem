using CodeGolem.Combat;
using CodeGolem.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = System.Diagnostics.Debug;

namespace CodeGolem.Actor
{
    [CreateAssetMenu(fileName = "PlayerStatsTemplate", menuName = "Character/Stats", order = 1)]
    public class PlayerStats : ActorStats
    {
        public delegate void VitalUiUpdater(PlayerStats actor);

        public static event VitalUiUpdater VitalUiEvent;

        public delegate void StatsUiUpdater(PlayerStats actor);

        public static event StatsUiUpdater StatsUiEvent;

         [Header("Movement Stats")]
        public int PlayerMovementSpeed;

        public int PlayerAcceleration;

        [SerializeField] private int dashAmount;
        public float TimeBetweenDash;
        [Range(0.1f, 10)] public float DashDistance;



        [Header("Player Skills")]
        [SerializeField] private List<SkillComponent> playerSkills = new List<SkillComponent>();

        public new float Health
        {
            get
            {
                return base.Health;
            }

            set
            {
                base.Health = value;
                Debug.Assert(VitalUiEvent != null, "VitalUiEvent != null");
                VitalUiEvent(this);
            }
        }

        public new float ManaPoints
        {
            get
            {
                return base.ManaPoints;
            }

            set
            {
                base.ManaPoints = value;
                Debug.Assert(VitalUiEvent != null, "VitalUiEvent != null");
                VitalUiEvent(this);
            }
        }

        public new float Experience
        {
            get
            {
                return base.Experience;
            }
            set
            {
                base.Experience = value;
                Debug.Assert(VitalUiEvent != null, "VitalUiEvent != null");
                VitalUiEvent(this);
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
                Debug.Assert(VitalUiEvent != null, "VitalUiEvent != null");
                VitalUiEvent(this);
            }
        }

        public new int Strength
        {
            get { return base.Strength; }
            set
            {
                base.Strength = value;
                if (EventManager.onStatsUpdate != null)
                {
                    EventManager.onStatsUpdate();
                }
            }
        }

        public new int Constitution
        {
            get { return base.Constitution; }

            set
            {
                base.Constitution = value;
                if (EventManager.onStatsUpdate != null)
                {
                    EventManager.onStatsUpdate();
                }
            }
        }

        public new int Intelligence
        {
            get { return base.Intelligence; }

            set
            {
                base.Intelligence = value;
                if (EventManager.onStatsUpdate != null)
                {
                    EventManager.onStatsUpdate();
                }
            }
        }

        public new int Defense
        {
            get { return base.Defense; }
            set
            {
                base.Defense = value;
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

        public bool RegisterSkill(SkillComponent skill, AbilityIcon icon)
        {
            if (skill == null || icon == null)
            {
                throw new ArgumentNullException("SkillComponent or AbilityIcon Missing!");
            }

            try
            {
                skill.RegisterSkill(icon);
                if (playerSkills.Contains(skill))
                {
                    return false;
                }
                if (PlayerSkills.Count < 6) // #TODO: Make Global somehow (MAX NUMBER OF SKILLS)
                {
                    playerSkills.Add(skill);
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException("Failed to Register Skill!");
            }

            return false;
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