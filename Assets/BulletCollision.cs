using System.Collections;
using System.Collections.Generic;
using CodeGolem.Actor;
using UnityEngine;

public class BulletCollision : MonoBehaviour {
    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<IDamageable>().TakeDamage(50f);
    }
}
