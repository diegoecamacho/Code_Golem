using CodeGolem.Combat;
using CodeGolem.UI;
using UnityEngine;
using UnityEngine.AI;

namespace CodeGolem.Player
{
    public enum PlayerMovementType
    {
        WALK,
        DASH
    }

    public class PlayerController : MonoBehaviour
    {
        

        private enum PlayerState
        {
            MOVE,
            ATTACK,
            PAUSED
        }

        private PlayerState State = PlayerState.MOVE;

        [Header("Character Class")]
        [SerializeField] private MainActorStats actorStats;

        public MainActorStats ActorStats
        {
            get
            {
                return actorStats;
            }
        }

        [Header("Weapon Spawn Point")]
        [SerializeField] public Transform spawnPoint; //Skill Spawn point

        public Transform SpawnPoint
        {
            get
            {
                return spawnPoint;
            }

            set
            {
                spawnPoint = value;
            }
        }

        [Header("Player Movement")]
        [SerializeField] private float baseMovementSpeed;

        [SerializeField] private float baseAcceleration;

        [Header("Dash")]
        [SerializeField] private float dashMovementSpeed;

        [SerializeField] private float dashAcceleration;
        [SerializeField] private float timeBetweenDash;
        public bool dashonCooldown = false;
        private float dashCooldown = 0;

        [Range(0.1f, 10)]
        [SerializeField] private float dashDistance;

        //#TODO: Implement weapon selection system

        [Header("Navigation Mesh Agent")]
        //public NavMeshAgent Agent;
        public PlayerMovement Agent;

        [Header("InputManager")]
        private int inputCache = -1;

        private Vector3 ClickLocation;

        [Header("ActiveCheck")]
        private GameObject pointer;

        private bool UIenabled = false;

        [Header("Debug")]
        [SerializeField] private GameObject pauseMenu; //#TODO: Move to UI Controller

        public AbilityIcon abilityIcon;
        public SkillComponent Skill;

        [Range(0, 100)]
       public float DebugHealth = 100;
        [Range(0, 100)]
        public float DebugMana = 100;

        private void Start()
        {
            dashCooldown = timeBetweenDash;
            actorStats.DashAmount = 4;
            ActorStats.RegisterSkill(Skill, abilityIcon);
        }

        private void Update()
        {
            actorStats.Health = DebugHealth;
            actorStats.ManaPoints = DebugMana;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
                if (State != PlayerState.PAUSED)
                    State = PlayerState.PAUSED;
                else
                    State = PlayerState.MOVE;
            }

            if (dashonCooldown)
            {
                dashCooldown -= Time.deltaTime;
                Debug.Log(dashCooldown);
                if (dashCooldown <= 0)
                {
                    dashonCooldown = false;
                    dashCooldown = timeBetweenDash;
                }
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
                        ActorStats.EnableSkill(inputCache, gameObject);
                        RaycastAttack(inputCache);
                        if (!ActorStats.PlayerSkills[0].GetBehaviour().IsActive())
                        {
                            State = PlayerState.MOVE;
                            if (pointer != null)
                            {
                                Destroy(pointer);
                            }
                        }
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetButtonDown("PlayerActive"))
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "WalkableGround")
                    {
                        SetPlayerSpeed(PlayerMovementType.WALK);
                        ClickLocation = hit.point;
                        Agent.SetDestination(hit.point);
                    }
                }
            }

            if (Input.GetButtonDown("PlayerDash"))
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "WalkableGround")
                    {
                        ActivateDash(hit);
                    }
                }
            }
        }

        //#TODO: FIX Dash.
        //Remove Nav Mesh Agent and implement Own
        private void ActivateDash(RaycastHit hit)
        {
            if (!dashonCooldown && actorStats.DashAmount > 0)
            {
                SetPlayerSpeed(PlayerMovementType.DASH);
                actorStats.DashAmount--;
                dashonCooldown = true;

                Vector3 midPoint = (hit.point + transform.position) / 2;
                Vector3 dir = hit.point - transform.position;

                if (Vector3.Distance(transform.position, hit.point) < 20)
                {
                    Vector3 dashPoint = (midPoint + (dashDistance * dir));
                    Agent.SetDestination(hit.point);
                    ClickLocation = dashPoint;
                }
                else
                {
                    Agent.SetDestination(hit.point);
                    ClickLocation = midPoint;
                }
            }
        }

        /// <summary>
        /// Decides Player speed based on PlayerMovementType enum
        /// </summary>
        /// <param name="movementType"></param>
        public void SetPlayerSpeed(PlayerMovementType movementType)
        {
            switch (movementType)
            {
                case PlayerMovementType.WALK:
                    {
                        Agent.movementSpeed = baseMovementSpeed;
                        Agent.acceleration = baseAcceleration;
                    }
                    break;

                case PlayerMovementType.DASH:
                    {
                        Agent.movementSpeed = dashMovementSpeed;
                        Agent.acceleration = dashAcceleration;
                    }
                    break;
            }
        }

        /// <summary>
        /// Call to enable skills that use the Raycast Pointer system.
        /// </summary>
        /// <param name="input"></param>
        private void RaycastAttack(int input)
        {
            if (pointer == null)
            {
                pointer = Instantiate(Resources.Load("MousePointer")) as GameObject;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                pointer.transform.position = new Vector3(hit.point.x, hit.point.y + 0.02f, hit.point.z);
                if (Input.GetButtonDown("PlayerActive"))
                {
                    SkillParam skillParam = new SkillParam(this, new Vector3(hit.point.x, transform.position.y, hit.point.z));
                    ActorStats.UseSkill(input, skillParam);
                }
            }

            if (ActorStats.PlayerSkills[input].allowMovement)
                PlayerMove();
        }

        //#TODO: Move to global function
        private void PauseGame()
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            UIenabled = !UIenabled;
        }

        /// <summary>
        /// Returns player input that the Skill system can understand
        /// </summary>
        /// <returns></returns>
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