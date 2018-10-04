using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeGolem.Actor
{
    interface IDamageable
    {
        void Attack();
        void TakeDamage(float damage);
    }

}

