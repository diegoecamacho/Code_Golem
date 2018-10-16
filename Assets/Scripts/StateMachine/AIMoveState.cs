using CodeGolem.Actor;
using CodeGolem.Level;
using System;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeGolem.StateController
{
    public enum UnitType
    {
        Civilian,
        Enemy
    }

    /// <summary>
    /// AI Move State
    /// </summary>
    /// <typeparam name="T">Actor Stats Template</typeparam>
    public class AIMoveState<T> : IState where T : ActorStats
    {
        private readonly T _actorStats;

        private readonly Transform _actor;

        private readonly Action<MovementReturn> _movementCallBack;

        private readonly UnitType unit;

        private readonly Transform[] _idleWaypoints;

        private readonly bool _multipleWaypoints;

        private readonly NavMeshAgent _agent;

        private int _curWaypoint;

        public bool OnAlert;

        public AIMoveState(Object actorStats, Transform actor, NavMeshAgent agent, Transform[] idleWaypoints, Action<MovementReturn> movementCallBack, UnitType unit)
        {
            _actorStats = actorStats as T;
            Debug.Assert(_actorStats != null, "ActorStats Missing");

            _actor = actor;
            _agent = agent;
            _movementCallBack = movementCallBack;
            this.unit = unit;
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
            var movementReturn = new MovementReturn(_idleWaypoints[_curWaypoint].position);
            _movementCallBack(movementReturn);
        }

        public void Execute()
        {
            if (unit == UnitType.Enemy)
            {
                var stats = _actorStats as EnemyStats;
                Debug.Assert(stats != null, "Cast to EnemyStats Failed!");
                if (Vector3.Distance(_actor.transform.position, LevelManager.Player.transform.position) < stats.SearchRadius)
                {
                    var movementReturn = new MovementReturn(LevelManager.Player.transform.position);

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
                movement = new MovementReturn(_actor.transform.position);
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

            var movementReturn = new MovementReturn(_idleWaypoints[_curWaypoint].position);
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
        public MovementReturn(Vector3 nextPosition)
        {
            NextPosition = nextPosition;
        }

        public MovementReturn(Vector3 nextPosition, MovementType movementType)
        {
            NextPosition = nextPosition;
            MovementType = movementType;
        }

        public Vector3 NextPosition { get; private set; }
        public MovementType MovementType { get; private set; }
    }
}