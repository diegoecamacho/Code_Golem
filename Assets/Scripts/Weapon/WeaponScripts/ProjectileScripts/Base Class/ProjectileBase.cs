using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ProjectileBase : MonoBehaviour {

    public abstract void Initialize(Vector3 targetPosition, float projectileSpeed, float Damage);

}
