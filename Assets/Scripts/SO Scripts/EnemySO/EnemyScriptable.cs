using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeGolem.Actor;

namespace CodeGolem.Enemy
{
    [CreateAssetMenu(fileName = "EnemyScriptable", menuName = "Enemy/Stats", order = 1)]
    public class EnemyScriptable : ActorStats
    {
        public override void TakeDamage(float damage)
        {
            health -= damage;
        }
    }
}
