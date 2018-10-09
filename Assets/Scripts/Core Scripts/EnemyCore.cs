using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeGolem.Actor;
namespace CodeGolem.Enemy
{
    public struct EnemyParams
    {
        public Transform destination;

        EnemyParams(Transform destPos)
        {
            destination = destPos;
        }
    }

        /// <summary>
        /// Enemy State Machine enum's
        /// </summary>
        public enum EnemyStates
        {
            Idle,
            Patrol,
            Alarm,
            Move,
            Attack,
            Dead
        }

        /// <summary>
        /// Enemy w/ State Machine base implementation.
        /// </summary>
        /// <param name="enemyState">Holds the enemies current state</param>
        public abstract class EnemyBase : ActorBase
        {
            protected EnemyStates enemyState;
        }
    }
