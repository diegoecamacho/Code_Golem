using System;

using UnityEngine;
using UnityEngine.AI;
using CodeGolem.Actor;
using CodeGolem.Player;
using CodeGolem.StateController;

namespace CodeGolem.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MeleeEnemy : EnemyBase
    {
        private NavMeshAgent agent;

        [Header("State Variables")]
        [SerializeField] private bool onAlert;

        [Header("Weapon")]
        private float weaponRange = 1f;

        [Header("Debug")]
        public Transform playerTransform;

        public bool Move = false;
        private float deadZone;

        [SerializeField] private Transform[] idleWaypoints;
        public int currIdleWaypoint = 0;

        [SerializeField] private float alertRange = 10.0f;
        private float distanceFromPlayer;

        [Header("Time")]
        [SerializeField] private float waitTime = 2f;

        private float timeElapsed = 0;
        private float timeBetweenAttacks;
        private bool Attacking = false;

        // Use this for initialization
        private void Start()
        {
            stateMachine = gameObject.AddComponent<StateMachine>();
            agent = GetComponent<NavMeshAgent>();
            stateMachine.ChangeState(new MoveState(this, agent, idleWaypoints));
        }

        // Update is called once per frame
        private void Update() {
           
            stateMachine.ExecuteStateUpdate();

            distanceFromPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

            if (distanceFromPlayer < alertRange && !onAlert)
            {
                stateMachine.ChangeState(new MoveState(this, agent, PlayerController.instance.transform));
                onAlert = true;
            }
            if (!Attacking)
            {
                if (onAlert && distanceFromPlayer < approachRange)
                {
                    stateMachine.ChangeState(new AttackState(this.Attack, 2.0f));
                    Attacking = true;
                }
            }
        }

        public override void Attack()
        {
            Debug.Log("Attack");
        }

        public override void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }

        public override void Interact()
        {
            throw new System.NotImplementedException();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, alertRange);
        }
    }
}