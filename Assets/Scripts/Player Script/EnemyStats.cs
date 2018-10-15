using CodeGolem.Actor;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTemplate", menuName = "Enemy/Stats", order = 1)]
public class EnemyStats : ActorStats
{
    public float SearchRadius;

    public float WeaponRange;

    public float TimeBetweenAttacks;

    public float PatrolSpeed;

    public float AlertSpeed;

    public override void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}