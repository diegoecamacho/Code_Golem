using UnityEngine;
using UnityEngine.AI;
using CodeGolem_WeaponSystem;


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

    PlayerState playerState = PlayerState.MOVE;

    [SerializeField] private CharacterStats characterStats;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject spawnPoint; //Skill Spawn point

    //#TODO: Implement weapon selection system

    private bool UIenabled = false;

    public NavMeshAgent Agent;

    private Transform _target;

    private bool skillActive;

    //Weapon System
    //#TODO Make it for each key
    [SerializeField]AbilityBase skillSlot1;

    public AbilityBase SkillSlot1
    {
        get
        {
            return skillSlot1;
        }

        set
        {
            skillSlot1 = value;
            skillsUIUpdate(value, spawnPoint);
        }
    }



    //#Remove Debug Variable
    [SerializeField] GameObject PointerArrow;
    Vector3 gizmoPosition;

    GameObject pointer;
    private bool pauseEnabled = false;

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
        skillsUIUpdate(skillSlot1, spawnPoint);
    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            if (playerState != PlayerState.PAUSED)
                playerState = PlayerState.PAUSED;
            else
                playerState = PlayerState.MOVE;
        }

        switch (playerState)
        {   
            case PlayerState.MOVE:
                {
                    PlayerMove();
                }
                break;
            case PlayerState.ATTACK:
                //Attack();
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
        pauseEnabled = true;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        UIenabled = !UIenabled;
    }
}