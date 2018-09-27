using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : ProjectileBase {

    const float deadZone = 1;

    Rigidbody bulletRb;
    Vector3 destinationVector;
    float damage;

    public override void Initialize(Vector3 targetPosition, float speed, float in_damage)
    {
        bulletRb = gameObject.GetComponent<Rigidbody>();
        destinationVector = targetPosition;
        damage = in_damage;
        Vector3 bulletDir = Vector3.Normalize(targetPosition - transform.position);
        if (bulletRb != null)
        {
            bulletRb.AddForce(bulletDir * speed * Time.deltaTime, ForceMode.Impulse);
        }
    }

	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(destinationVector, transform.position) < deadZone)
        {
            Destroy(gameObject);
        }

		
	}
}
