using CodeGolem.Actor;
using CodeGolem.Combat;
using CodeGolem.Managers;
using CodeGolem.StateController;
using CodeGolem.UI;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace CodeGolem.Player
{
    public class PlayerController : ActorBase<PlayerStats>
    {
        [Header("Weapon Spawn Point")]
        public Transform SpawnPoint; //Skill Spawn point

        [FormerlySerializedAs("Agent")]
        [Header("Components")]

        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;

        [Header("InputManager")]
        private int inputCache = -1;

        [Header("Debug")]
        public AbilityIcon abilityIcon;

        public SkillComponent Skill;

        private Vector3 hitPosition;

        private void Start()
        {
            try
            {
                StateMachine = gameObject.AddComponent<StateMachine>();
                StateMachine.ChangeState(new MoveState(ActorStats, agent, PlayerMove));
                //ActorStats.RegisterSkill(Skill, abilityIcon);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, this);
            }
        }

        private void Update()
        {
   
            StateMachine.ExecuteStateUpdate();

            if (Input.GetButtonDown("PlayerActive"))
            {
                MoveStateUpdate(MovementType.Walk);
            }

            if (Input.GetButtonDown("PlayerDash"))
            {
                MoveStateUpdate(MovementType.Dash);
            }

            if (animator == null) return;

            if (agent.velocity.magnitude > 0)
            {
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }
                
            

            //    case PlayerState.Attack:
            //        {
            //            Debug.Log("Hello");
            //            ActorStats.EnableSkill(inputCache, gameObject);
            //            RaycastAttack(inputCache);
            //            if (!ActorStats.PlayerSkills[0].GetBehaviour().IsActive())
            //            {
            //                _state = PlayerState.Move;
            //            }
            //        }
            //        break;

            //    case PlayerState.Paused:
            //        break;

            //    default:
            //        break;
            //}
        }

        private void MoveStateUpdate(MovementType type)
        {
            RaycastHit hit;
            RaycastManager.Instance.RaycastHit(out hit);
            var currentState = StateMachine.currentState as MoveState;

            Debug.Assert(currentState != null, "currentState != null");
            currentState.SetDestination(hit.point, type);
        }

        private void PlayerMove(MovementReturn movement)
        {
            Debug.Log(movement.MovementType);
            switch (movement.MovementType)
            {
                case MovementType.Walk:
                    agent.SetDestination(movement.NextPosition);
                    break;

                case MovementType.Dash:
                    {
                        agent.SetDestination(movement.NextPosition);
                        transform.position = movement.NextPosition;
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Call to enable skills that use the Raycast Pointer system.
        /// </summary>
        /// <param name="input"></param>
        //private void RaycastAttack(int input)
        //{
        //    RaycastHit hit;
        //    RaycastManager.Instance.RaycastHit(out hit);

        //    if (Input.GetButtonDown("PlayerActive"))
        //    {
        //        SkillParam skillParam = new SkillParam(this, new Vector3(hit.point.x, transform.position.y, hit.point.z));
        //        ActorStats.UseSkill(input, skillParam);
        //    }

        //    if (ActorStats.PlayerSkills[input].allowMovement)
        //        PlayerMove();
        //}

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

        public override void TakeDamage(float damage)
        {
            ActorStats.Health -= damage;
        }

        public override void Interact()
        {
            throw new NotImplementedException();
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}