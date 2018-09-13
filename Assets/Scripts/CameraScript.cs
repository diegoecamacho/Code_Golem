using UnityEngine;

public class CameraScript : MonoBehaviour
{
    const string MOUSE_X = "Mouse X";
    const string MOUSE_Y = "Mouse Y";
    const string MOUSE_SCROLL = "Mouse ScrollWheel";

    [Header("Sensativity Settings")]
    public float MouseSensitivity = 1.0f;
    public float scrollSentitivity = 1.0f;
    public float orbitDampening = 10.0f;
    public float scrollDampening = 5.0f;

    public bool enableCamera = false;

    private Transform cameraTransform;

    private Transform parentTransform;

    private Vector3 localRotation;

    private float cameraDist = 2.0f; // Camera Distance

 

    [Header("Camera Y")]
    public float Ydist = 2f; // Distance from the ground;

    [Header("Camera  Y Rotation Limits")]
    
    [SerializeField] float minPithAngle = 0.0f; // Minimun Pitch Angle;
    [SerializeField] float maxPitchAngle = 90.0f; //Maximum Pitch Angle;

    [Header("Distance to Target")]
    [SerializeField] float MIN_CAM_DIST = 3f;
    [SerializeField] float MAX_CAM_DIST = 80f;


    private void Start()
    {
        cameraDist = MIN_CAM_DIST;
        cameraTransform = this.transform;
        parentTransform = this.transform.parent;

    }

    private void LateUpdate()
    {
        enableCamera = Input.GetMouseButton(2) ? true : false;

        if (enableCamera)
        {
            if (Input.GetAxis(MOUSE_X) != 0 && Input.GetAxis(MOUSE_Y) != 0)
            {
                localRotation.x += Input.GetAxis(MOUSE_X) * MouseSensitivity;
                localRotation.y -= Input.GetAxis(MOUSE_Y) * MouseSensitivity; // -= TO INVERT???

                //Clamp Camera Y
                localRotation.y = Mathf.Clamp(localRotation.y, minPithAngle, maxPitchAngle);
            }

        }

        //Zooming Input from mouse
        if (Input.GetAxis(MOUSE_SCROLL) != 0)
            {
                float scrollAmount = Input.GetAxis(MOUSE_SCROLL) * scrollSentitivity;

                scrollAmount *= (cameraDist * 0.3f); // grabs 30% of the camera distance gives it slow effect when getting close

                cameraDist += scrollAmount * -1;

                cameraDist = Mathf.Clamp(cameraDist, MIN_CAM_DIST, MAX_CAM_DIST); // how close the camera can get to parent.

        }

        Quaternion QT = Quaternion.Euler(localRotation.y, localRotation.x, 0);
        parentTransform.rotation = Quaternion.Lerp(parentTransform.rotation, QT, Time.deltaTime * orbitDampening);

        if (cameraTransform.localPosition.z != cameraDist * -1f)
        {
            {
                cameraTransform.localPosition =
                          new Vector3(0f, Ydist, Mathf.Lerp(cameraTransform.localPosition.z, cameraDist *-1, Time.deltaTime * scrollDampening));
               
            }

        }
    }

    public void ReturnCamera()
    {
        cameraDist = (MAX_CAM_DIST / 2);

    }
}
