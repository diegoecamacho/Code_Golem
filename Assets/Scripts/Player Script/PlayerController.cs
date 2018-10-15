using CodeGolem.Actor;
using CodeGolem.Combat;
using CodeGolem.UI;
using System;
using CodeGolem.Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace CodeGolem.Player
{
    public enum PlayerMovementType
    {
        WALK,
        DASH
    }

    public class PlayerController : MonoBehaviour, IDamageable
    {
        public static PlayerController instance;

        public static Transform PlayerLocation;

        private enum PlayerState
        {
            Move,
            Attack,
            Paused
        }

        private PlayerState _state = PlayerState.Move;

        [Header("Character Class")]
        [SerializeField] private PlayerStats _actorStats;

        public PlayerStats ActorStats
        {
            get
            {
                return _actorStats;
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
        [SerializeField] private float _baseMovementSpeed;

        [SerializeField] private float _baseAcceleration;

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
        public NavMeshAgent Agent;

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

        private Vector3 hitPosition;

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }

            if (PlayerLocation == null)
            {
                PlayerLocation = transform;
            }

            try
            {
                dashCooldown = timeBetweenDash;
                ActorStats.RegisterSkill(Skill, abilityIcon);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, this);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
                if (_state != PlayerState.Paused)
                    _state = PlayerState.Paused;
                else
                    _state = PlayerState.Move;
            }

            if (dashonCooldown)
            {
                dashCooldown -= Time.deltaTime;

                if (dashCooldown <= 0)
                {
                    dashonCooldown = false;
                    dashCooldown = timeBetweenDash;
                }
            }

            switch (_state)
            {
                case PlayerState.Move:
                    {
                        PlayerMove();
                        inputCache = PlayerAttackInput();
                        if (inputCache != -1)
                        {
                            _state = PlayerState.Attack;
                        }
                    }
                    break;

                case PlayerState.Attack:
                    {
                        Debug.Log("Hello");
                        ActorStats.EnableSkill(inputCache, gameObject);
                        RaycastAttack(inputCache);
                        if (!ActorStats.PlayerSkills[0].GetBehaviour().IsActive())
                        {
                            _state = PlayerState.Move;
                            if (pointer != null)
                            {
                                Destroy(pointer);
                            }
                        }
                    }
                    break;

                case PlayerState.Paused:
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
                    SetPlayerSpeed(PlayerMovementType.WALK);
                    ClickLocation = hit.point;

                    float y = hit.collider.transform.position.y + 1;

                    Vector3 hitLocMod = new Vector3(hit.point.x, y, hit.point.z);
                    Agent.SetDestination(hitLocMod);
                }
            }

            if (Input.GetButtonDown("PlayerDash"))
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    ActivateDash(hit);
                }
            }
        }

        private void ActivateDash(RaycastHit hit)
        {
            if (!dashonCooldown && _actorStats.DashAmount > 0)
            {
                SetPlayerSpeed(PlayerMovementType.DASH);
                _actorStats.DashAmount--;
                dashonCooldown = true;

                Vector3 dir = hit.point - transform.position;
                Vector3 DashPoint = transform.position + (dir * dashDistance);
                Agent.transform.position = DashPoint;
                Agent.SetDestination(Agent.transform.position);
                ClickLocation = DashPoint;
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
                        Agent.speed = _baseMovementSpeed;
                        Agent.acceleration = _baseAcceleration;
                    }
                    break;

                case PlayerMovementType.DASH:
                    {
                        Agent.speed = dashMovementSpeed;
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
   
            RaycastHit hit;
            RaycastManager.Instance.RayHit(out hit);

            if (Input.GetButtonDown("PlayerActive"))
            {
                SkillParam skillParam = new SkillParam(this, new Vector3(hit.point.x, transform.position.y, hit.point.z));
                ActorStats.UseSkill(input, skillParam);
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

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(ClickLocation, 0.1f);
        }

        public void TakeDamage(float damage)
        {
            
            ActorStats.Health -= damage;
            
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}