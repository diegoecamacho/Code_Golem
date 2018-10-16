using UnityEngine;
using UnityEngine.Serialization;
using Debug = System.Diagnostics.Debug;

namespace CodeGolem.Managers
{
    /// <summary>
    /// Raycast Manager
    /// Manages all Raycast coming from the Camera to mouse position.
    /// </summary>
    public class RaycastManager : Singleton<RaycastManager>
    {
        public GameObject UiPointerPrefab;

        [SerializeField] private Camera _cam;

        private Ray _ray;
        private RaycastHit _hit;
        private bool _pointerActive;
        private GameObject _pointer;

        [Header("Debug Gizmos")]
        public bool GizmoEnabled = true;

        // Use this for initialization
        private void Start()
        {
            _cam = Camera.main;
            Debug.Assert(_cam != null, "Camera.main != null");

            _pointer = Instantiate(UiPointerPrefab);
            EnablePointer(true);
        }

        // Update is called once per frame
        private void Update()
        {
            _ray = _cam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(_ray, out _hit);

            if (!_pointerActive) return;
            _pointer.transform.position = new Vector3(_hit.point.x, _hit.point.y + 0.02f, _hit.point.z);
        }

        /// <summary>
        /// Enable Pointer
        /// </summary>
        /// <param name="active"></param>
        public void EnablePointer(bool active)
        {
            _pointer.SetActive(active);
            _pointerActive = active;
        }

        /// <summary>
        /// Casts a raycast from the Camera to a MousePosition
        /// </summary>
        /// <param name="hitPoint">out's hit point</param>
        /// <returns>True if collided</returns>
        public bool RaycastHit(out RaycastHit hitPoint)
        {
            hitPoint = _hit;
            return Physics.Raycast(_ray, out _hit);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if (GizmoEnabled) Gizmos.DrawLine(_cam.transform.position, _hit.point);
        }
    }
}