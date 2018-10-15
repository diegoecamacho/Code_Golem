using UnityEngine;
using UnityEngine.Serialization;
using Debug = System.Diagnostics.Debug;

namespace CodeGolem.Managers
{
    public class RaycastManager : Singleton<RaycastManager>
    {
        public GameObject UiPointerPrefab;

        private Ray _ray;
        private RaycastHit _hit;
        private bool _pointerActive;
        private Camera _cam;
        private GameObject _pointer;

        [Header("Debug Gizmos")]
        public bool _gizmoEnabled = true;

        // Use this for initialization
        private void Start()
        {
            _cam = Camera.main;
            _pointer = Instantiate(UiPointerPrefab);
            EnablePointer(true);

        }

        // Update is called once per frame
        private void Update()
        {
            Debug.Assert(_cam != null, "Camera.main != null");
            _ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (!_pointerActive) return;
            Physics.Raycast(_ray, out _hit);
            _pointer.transform.position = new Vector3(_hit.point.x, _hit.point.y + 0.02f, _hit.point.z);
        }

        public void EnablePointer(bool active)
        {
            _pointer.SetActive(active);
            _pointerActive = active;
        }

        public bool RayHit(out RaycastHit hitPoint)
        {
            hitPoint = _hit;
            return Physics.Raycast(_ray, out _hit);
        }

        private void OnDrawGizmos()
        {
            if (_gizmoEnabled) Gizmos.DrawLine(_cam.transform.position, _hit.point);
        }
    }
}