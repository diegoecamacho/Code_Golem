using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeGolem.Actor;

namespace CodeGolem.Enemy
{
    [CreateAssetMenu(fileName = "LiveBullet", menuName = "Enemy/LiveBullet", order = 1)]
    public class EnemyBullet : ActorStats
    {
        public override void TakeDamage(float damage)
        {
            health -= damage;
        }
    }
}
