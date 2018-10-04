using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeGolem.Enemy
{
    struct EnemyParams
    {
        Vector3 destination;

        EnemyParams(Vector3 destPos)
        {
            destination = destPos;
        }
    }
}
