using CodeGolem.Actor;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace CodeGolem.StateController
{
    public enum MovementType
    {
        Walk,
        Dash
    }

    public class MoveState : IState
    {
        private readonly PlayerStats player;
        private readonly NavMeshAgent agent;
        private readonly Action<MovementReturn> moveCallAction;

        private Vector3 destinationVector;
        private MovementType movement;
        private float timeElapsed = 0f;
        private bool dashActive;

        private bool active;

        public MoveState(PlayerStats player, NavMeshAgent agent, System.Action<MovementReturn> moveCallAction)
        {
            this.player = player;
            this.agent = agent;
            this.moveCallAction = moveCallAction;
        }

        public void Enter()
        {
            agent.isStopped = false;
        }

        public void SetDestination(Vector3 moveLocation, MovementType type)
        {
            destinationVector = moveLocation;
            movement = type;
            active = true;
        }

        public void Execute()
        {
            if (dashActive)
            {
                timeElapsed += Time.deltaTime;
                if (!(timeElapsed >= player.TimeBetweenDash)) return; // Return if time is less than dash time

                timeElapsed = 0;
                dashActive = false;
            }

            if (!active) return; // Return if no Destination set.

            switch (movement)
            {
                case MovementType.Walk:
                    {
                        moveCallAction(new MovementReturn(destinationVector, movement));
                        break;
                    }
                case MovementType.Dash:
                    player.DashAmount--;
                    dashActive = !dashActive;
                    var dir = destinationVector - agent.transform.position;
                    var dashPoint = agent.transform.position + (dir * player.DashDistance);
                    moveCallAction(new MovementReturn(dashPoint, movement));

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            active = false;
        }

        public void Exit()
        {
            agent.isStopped = true;
        }
    }
}