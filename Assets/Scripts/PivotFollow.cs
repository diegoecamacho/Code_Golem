using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotFollow : MonoBehaviour {

    const string HORIZONTAL_AXIS = "Horizontal";
    const string VERTICAL_AXIS = "Vertical";

    


    public Transform followTarget;
    public float movementSensitivity = 1.0f;

    private float xAxis = 0f;
    private float yAxis = 0f;

	
	// Update is called once per frame
	void Update () {

        xAxis = Input.GetAxis(HORIZONTAL_AXIS) * movementSensitivity;
        yAxis = Input.GetAxis(VERTICAL_AXIS) * movementSensitivity;

        transform.position += new Vector3(xAxis, 0, yAxis); 

        






        




        //if (transform.position != followTarget.position)
        //{
        //    transform.position = Vector3.Lerp(transform.position, followTarget.position, movementSensitivity * Time.deltaTime);
        //}
		
	}
}
