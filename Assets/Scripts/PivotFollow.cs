using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotFollow : MonoBehaviour {

    public Transform followTarget;
    public float followDampening = 1.0f;

	
	// Update is called once per frame
	void Update () {


        if (transform.position != followTarget.position)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followDampening * Time.deltaTime);
        }
		
	}
}
