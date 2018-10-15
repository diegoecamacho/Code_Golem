using CodeGolem.Actor;

namespace CodeGolem.Enemy
{
    using CodeGolem.Player;
    using CodeGolem.StateController;
    using System;
    using UnityEngine;
    using UnityEngine.AI;

    /// <inheritdoc />
    /// <summary>
    /// The melee enemy.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class MeleeEnemy : ActorBase<EnemyStats>
    {
        private PlayerController _player;
        private Animator _animator;
        private NavMeshAgent _agent;

        [Header("Idle Waypoints")]
        [SerializeField] private Transform[] _idleWaypoints;

        public override void Interact()
        {
            throw new NotImplementedException();
        }

        public override void TakeDamage(float damage)
        {
            this.ActorStats.Health -= damage;
            if (ActorStats.Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        // Use this for initialization
        private void Start()
        {
            //Initialization
            _player = FindObjectOfType<PlayerController>();
            _animator = GetComponent<Animator>();
            _agent = this.GetComponent<NavMeshAgent>();

            Debug.Assert(ActorStats != null, "Enemy Stats is Null!");
            Debug.Assert(_player != null, "player != null");
            Debug.Assert(_agent != null, "_agent != null");

            //Assignment
            _agent.speed = ActorStats.PatrolSpeed;

            StateMachine = this.gameObject.AddComponent<StateMachine>();
            StateMachine.ChangeState(new MoveState<EnemyStats>(this.ActorStats, this.transform, _agent, _idleWaypoints, this.MovePlayer, MovementType.Enemy));
        }

        // Update is called once per frame
        private void Update()
        {
            StateMachine.ExecuteStateUpdate();

            if (_animator == null) return;

            if (_agent.velocity.magnitude > 0)
            {
                _animator.SetBool("Move", true);
                _animator.SetFloat("Speed", _agent.velocity.magnitude);
                return;
            }
            _animator.SetBool("Move", false);
        }

        private void MovePlayer(MovementReturn movementReturn)
        {
            var moveState = StateMachine.currentState as MoveState<EnemyStats>;

            if (moveState == null || movementReturn == null) return;

            if (transform.position == movementReturn.NextTransform.position) return;

            _agent.speed = moveState.OnAlert ? ActorStats.AlertSpeed : ActorStats.PatrolSpeed;

            if (Vector3.Distance(transform.position, _player.transform.position) <=
                 ActorStats.ApproachDistance)
            {
                StateMachine.ChangeState(new AttackState(Attack, AttackAnim, ActorStats.TimeBetweenAttacks));
                return;
            }

            Debug.Log("Move PLLayer");
            _agent.SetDestination(movementReturn.NextTransform.position);
        }

        public void AttackAnim()
        {
            _animator.SetBool("Attack", true);
        }

        public override void Attack()
        {
            _animator.SetBool("Attack", false);
            if (Vector3.Distance(transform.position, _player.transform.position) >=
                ActorStats.ApproachDistance)
            {
                Debug.Log("State Change");
                StateMachine.ReturnToPreviousState();
                return;
            }

            PlayerController.instance.TakeDamage(ActorStats.BaseDamage);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, this.ActorStats.SearchRadius);
        }
    }
}