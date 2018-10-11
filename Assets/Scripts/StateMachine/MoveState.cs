using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CodeGolem.Enemy;
using CodeGolem.Actor;
using System;

namespace CodeGolem.StateController
{

    public class MoveState : IState
    {
        ActorBase actor;
        NavMeshAgent navMeshAgentComp;

        Transform MoveWaypoint = null;
        Transform[] IdleWaypoints = null;

        int currWaypoint = 0;
        bool multipleWaypoints = false;

       public MoveState(ActorBase actortoMove, NavMeshAgent actorNavmesh, Transform[] wayPoints)
        {
            actor = actortoMove;
            navMeshAgentComp = actorNavmesh;
            IdleWaypoints = wayPoints;
        }

        public MoveState(ActorBase actortoMove, NavMeshAgent actorNavmesh, Transform wayPoint)
        {
            actor = actortoMove;
            navMeshAgentComp = actorNavmesh;
            MoveWaypoint = wayPoint;
        }

        public void Enter()
        {
            navMeshAgentComp.isStopped = false;
            if (MoveWaypoint != null)
            {
                navMeshAgentComp.SetDestination(MoveWaypoint.position);
            }
            else
            {
                if (IdleWaypoints != null)
                {
                    navMeshAgentComp.SetDestination(IdleWaypoints[currWaypoint].position);
                    multipleWaypoints = true;
                }
                else
                {
                    throw new ArgumentNullException("Missing Waypoints in Move State");
                }
            }
        }

        public void Execute()
        {
            if (multipleWaypoints)
            {
                if (Vector3.Distance(actor.transform.position, IdleWaypoints[currWaypoint].position) < ActorBase.approachRange)
                {
                    currWaypoint++;
                    currWaypoint = currWaypoint >= IdleWaypoints.Length ? 0 : currWaypoint;
                    navMeshAgentComp.SetDestination(IdleWaypoints[currWaypoint].position);
                }
            }
        }

        public void Exit()
        {
            navMeshAgentComp.isStopped = true;
            return;
        }
    }


}
