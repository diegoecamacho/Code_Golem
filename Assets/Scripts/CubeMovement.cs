using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour {
    const string MOVEMENT_HORIZONTAL = "Horizontal";
    const string MOVEMENT_VERTICAL = "Vertical";

   public float movementSpeed = 1.0f;

    Vector3 hitLocation = new Vector3(0f,1.5F,0f);

	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log("Hit");
                    hitLocation = new Vector3(hit.point.x,1.5F, hit.point.z);

                }

            }

        }
        if (hitLocation != null && transform.position != hitLocation)
        {
            transform.position = Vector3.Lerp(transform.position, hitLocation, Time.deltaTime * movementSpeed);
        }
	}
}
