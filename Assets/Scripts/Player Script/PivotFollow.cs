using UnityEngine;

public class PivotFollow : MonoBehaviour
{
    private const int WORLD_HIGH_POINT = 15;
    private const int WORLD_LOW_POINT = -1;
    private const int SLOW_POSITION = 1;

    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    [Header("Camera Sentitivity")]
    /// <summary>
    /// How fast it takes to reach the Y Boundaries of the game world
    /// </summary>
    [SerializeField] private float YBoundaryDampening = 0.2f;

    public float movementSensitivity = 1.0f;
    public float returnSensitivity = 1.0f;

    [Header("Camera Focus")]
    public Transform followTarget;

    public bool returningCamera = false;
    private float xAxis = 0f;
    private float yAxis = 0f;

    public bool focusOnTarget = true;

    private CameraScript mainCam;

    // Update is called once per frame

    private void Start()
    {
        mainCam = transform.GetChild(0).GetComponent<CameraScript>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            focusOnTarget = true;
            returningCamera = true;
            mainCam.ReturnCamera();
        }

        if (transform.position != followTarget.position && focusOnTarget == true)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, returnSensitivity * Time.deltaTime);
            if (Vector3.Distance(transform.position, followTarget.position) < SLOW_POSITION)
            {
                returningCamera = false;
            }
        }

        // Debug.Log(Vector3.Distance(transform.position, followTarget.position));

        if (!returningCamera)
        {
            xAxis = Input.GetAxis(HORIZONTAL_AXIS) * movementSensitivity;
            yAxis = Input.GetAxis(VERTICAL_AXIS) * movementSensitivity;

            if (xAxis != 0 || yAxis != 0)
            {
                focusOnTarget = false;
            }

            if (transform.position.y >= SLOW_POSITION)
            {
                transform.Translate(new Vector3(xAxis, 0, yAxis));
            }
            else
            {
                transform.Translate(new Vector3(xAxis * YBoundaryDampening, 0, yAxis * YBoundaryDampening));
            }

            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, WORLD_LOW_POINT, WORLD_HIGH_POINT), transform.position.z);
        }
    }
}