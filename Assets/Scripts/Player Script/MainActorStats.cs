using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeGolem.Combat;
using CodeGolem.UI;

namespace CodeGolem.Player
{
    [System.Serializable]
    public class MainActorStats
    {
        [SerializeField]CharacterStats characterStats;
        [SerializeField]List<SkillComponent> playerSkills = new List<SkillComponent>();

        public CharacterStats CharacterStats
        {
            get
            {
                return characterStats;
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

        public void UseSkill(int Input, GameObject Actor)
        {
            if (Actor.GetComponent<ISkillInterface>() == null)
            {
                PlayerSkills[Input].AddComponent(Actor);
                PlayerSkills[Input].Use();
            }
            else
            {
                if (Actor.GetComponent<ISkillInterface>() != PlayerSkills[Input].GetBehaviour())
                {
                    Actor.GetComponent<ISkillInterface>().DestroyComponent();
                    PlayerSkills[Input].AddComponent(Actor);
                    PlayerSkills[Input].Use();
                }
                else
                {
                    PlayerSkills[Input].Use();
                }

            }
   

        }
    }
}
