using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CodeGolem.Combat;
using CodeGolem.UI;

namespace CodeGolem.Player
{

    public class PlayerController : MonoBehaviour
    {
        public delegate void UpdateSkillsUI(AbilityBase ability, GameObject spawn);

        public static UpdateSkillsUI skillsUIUpdate;

        private enum PlayerState
        {
            MOVE,
            ATTACK,
            PAUSED
        }

        private PlayerState State = PlayerState.MOVE;
        [SerializeField] MainActorStats actorStats;

        public MainActorStats ActorStats
        {
            get
            {
                return actorStats;
            }
        }



        [SerializeField] private GameObject pauseMenu; //#TODO: Move to UI Controller

        [SerializeField] private Transform spawnPoint; //Skill Spawn point

        //#TODO: Implement weapon selection system

        private bool UIenabled = false;

        public NavMeshAgent Agent;

        [Header("Test Variables")]

        public AbilityIcon abilityIcon;

        public SkillComponent Skill;


        //Weapon System
        //#TODO Make it for each key
       // [SerializeField] private List<AbilityBase> abilities;
       //
       //[SerializeField] private SkillComponent[] m_skill;
       //[SerializeField] private AbilityIcon[] m_SkillIcons;

        private int inputCache = -1;

        public bool allowMovement;

       

        private void Start()
        {
            ActorStats.RegisterSkill(Skill, abilityIcon);
        }

        // Update is called once per frame
        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
                if (State != PlayerState.PAUSED)
                    State = PlayerState.PAUSED;
                else
                    State = PlayerState.MOVE;
            }

            switch (State)
            {
                case PlayerState.MOVE:
                    {
                        PlayerMove();
                        inputCache = PlayerAttackInput();
                        if (inputCache != -1)
                        {
                            State = PlayerState.ATTACK;
                        }
                    }
                    break;

                case PlayerState.ATTACK:
                    {
                        ActorStats.UseSkill(0, gameObject);
                       // if (abilities.Count == 0)
                       // {
                       //     Debug.LogError("No Player Abilities");
                       //     State = PlayerState.MOVE;
                       //     return;
                       // }
                       // m_skill[0].Use();
                       // State = PlayerState.MOVE;
                    }
                    break;

                case PlayerState.PAUSED:
                    break;

                default:
                    break;
            }
        }

        private void PlayerMove()
        {
            if (Input.GetAxis("PlayerActive") == 1)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        Agent.SetDestination(hit.point);
                    }
                }
            }
        }

        //#TODO: Move to global function
        private void PauseGame()
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            UIenabled = !UIenabled;
        }

        private int PlayerAttackInput()
        {
            if (Input.GetButtonDown("SkillSlot1"))
            {
                return 0;
            }
            if (Input.GetButtonDown("SkillSlot2"))
            {
                return 1;
            }
            if (Input.GetButtonDown("SkillSlot3"))
            {
                return 2;
            }
            if (Input.GetButtonDown("SkillSlot4"))
            {
                return 3;
            }
            if (Input.GetButtonDown("SkillSlot5"))
            {
                return 4;
            }
            if (Input.GetButtonDown("SkillSlot6"))
            {
                return 5;
            }
            return -1;
        }
    }
}