using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using CodeGolem_WeaponSystem;
using CodeGolem_UI;
using System;
using CodeGolem.Combat;

public class PlayerController : MonoBehaviour
{
    public delegate void UpdateSkillsUI(AbilityBase ability, GameObject spawn);
    public static UpdateSkillsUI skillsUIUpdate;

    enum PlayerState
    {
        MOVE,
        ATTACK,
        PAUSED
    }

    PlayerState State = PlayerState.MOVE;

    [SerializeField] UIController IController;
    
    [SerializeField] private CharacterStats characterStats;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private Transform spawnPoint; //Skill Spawn point

    //#TODO: Implement weapon selection system

    private bool UIenabled = false;

    public NavMeshAgent Agent;

    private Transform _target;

    private bool skillActive;

    //Weapon System
    //#TODO Make it for each key
    [SerializeField] List<AbilityBase> abilities;

    [SerializeField] SkillComponent skill;
    [SerializeField] AbilityIcon skillIcon;

    int inputCache = -1;

    public bool allowMovement;
    /// <summary>
    /// Gets the current Characters stats card.
    /// </summary>
    /// <returns>CharacterStats</returns>
    ///
    public CharacterStats GetStatsCard()
    {
        return characterStats;
    }

    private void Start()
    {
        //PoolManager.Instance.CreatePool(weaponPrefab, 3);
        //currWeapon.Init();
        //skillsUIUpdate(skillSlot1, spawnPoint);
        if (IController == null) { Debug.LogError("Missing UI Controller"); return; }
        if (abilities.Count > 0) {
            for (var i = 0; i < abilities.Count; i++)
            { 
                abilities[i].Initialize(spawnPoint);
            }
        }

        IController.InitializePlayerUI(abilities);

        skill.AddComponent(gameObject);
        skill.RegisterSkill(skillIcon);
    
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
                    if (abilities.Count == 0)
                    {
                        Debug.LogError("No Player Abilities");
                        State = PlayerState.MOVE;
                        return;
                    }
                    skill.Use();
                    State = PlayerState.MOVE;
                    //if (skillActive == false)
                    //{
                    //    abilities[inputCache].ActivateSkill();
                    //    IController.ActivateSkillUI(abilities[inputCache]);
                    //    allowMovement = abilities[inputCache].allowMovement;
                    //    skillActive = true;
                    //}
                    //if (allowMovement)
                    //{
                    //    PlayerMove();
                    //}
                    //if (abilities[inputCache].GetActive() == false)
                    //{
                    //    IController.CoolDownSkillUI(abilities[inputCache]);
                    //    State = PlayerState.MOVE;
                    //    skillActive = false;
                    //}
                }
                break;
            case PlayerState.PAUSED:
                break;
            default:
                break;
        }
    }

    void PlayerMove()
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
    void PauseGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        UIenabled = !UIenabled;
    }

    int PlayerAttackInput()
    {
        if (Input.GetButtonDown("SkillSlot1"))
        {
            return 0;

        }
        return -1;
    }
}