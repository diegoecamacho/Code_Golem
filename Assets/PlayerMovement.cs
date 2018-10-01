using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed;
    public float acceleration;
    public float deadZone;



    private bool MoveActive = false;
    private Vector3 destination;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (MoveActive)
        {
            transform.position = Vector3.Slerp(transform.position, destination, movementSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position , destination) < deadZone)
            {
                MoveActive = false;
            }

        }
		
	}

    public void SetDestination(Vector3 destPoint)
    {
        MoveActive = true;
        destination = new Vector3(destPoint.x, destPoint.y + 0.9f ,destPoint.z);
    }
}
