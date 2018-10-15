using CodeGolem.Actor;
using CodeGolem.Player;
using System;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeGolem.StateController
{
    public enum MovementType
    {
        Civilian,
        Enemy
    }

    public class MoveState<T> : IState where T : ActorStats
    {
        private readonly T _actorStats;

        private readonly Transform _actor;

        private readonly Action<MovementReturn> _movementCallBack;

        private readonly MovementType _movement;

        private readonly Transform[] _idleWaypoints;

        private readonly bool _multipleWaypoints;

        private readonly NavMeshAgent _agent;

        private int _curWaypoint;

        public bool OnAlert;

        public MoveState(Object actorStats, Transform actor, NavMeshAgent agent, Transform[] idleWaypoints, Action<MovementReturn> movementCallBack, MovementType movement)
        {
            _actorStats = actorStats as T;
            Debug.Assert(_actorStats != null, "ActorStats Missing");

            _actor = actor;
            _agent = agent;
            _movementCallBack = movementCallBack;
            _movement = movement;
            _idleWaypoints = idleWaypoints;

            if (_idleWaypoints.Length != 0)
            {
                _multipleWaypoints = true;
            }
        }

        public void Enter()
        {
            _agent.isStopped = false;
            if (!_multipleWaypoints) return;
            var movementReturn = new MovementReturn(_idleWaypoints[_curWaypoint]);
            _movementCallBack(movementReturn);
        }

        public void Execute()
        {
            Debug.Log("Move State");
            if (_movement == MovementType.Enemy)
            {
                var stats = _actorStats as EnemyStats;
                Debug.Assert(stats != null, "Cast to EnemyStats Failed!");
                if (Vector3.Distance(_actor.transform.position, PlayerController.PlayerLocation.position) < stats.SearchRadius)
                {
                    Debug.Log("Move To PLayer");
                    var movementReturn = new MovementReturn(PlayerController.PlayerLocation);

                    OnAlert = true;
                    _movementCallBack(movementReturn);
                    return;
                }
                OnAlert = false;
            }

            MovementReturn movement = null;
            if (_multipleWaypoints)
            {
                movement = SendNextWaypoint();
            }
            else
            {
                Debug.Log("Still To PLayer");
                movement = new MovementReturn(_actor.transform);
            }

            _movementCallBack(movement);
        }

        private MovementReturn SendNextWaypoint()
        {
            if (Vector3.Distance(_actor.transform.position, _idleWaypoints[_curWaypoint].position) <
                _actorStats.ApproachDistance)
            {
                _curWaypoint++;
                _curWaypoint = _curWaypoint >= _idleWaypoints.Length ? 0 : _curWaypoint;
            }

            var movementReturn = new MovementReturn(_idleWaypoints[_curWaypoint]);
            return movementReturn;
        }

        public void Exit()
        {
            _agent.isStopped = true;
            OnAlert = false;
        }
    }

    public class MovementReturn
    {
        public MovementReturn(Transform nextTransform)
        {
            NextTransform = nextTransform;
        }

        public Transform NextTransform { get; private set; }
    }
}