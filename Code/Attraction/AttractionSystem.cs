using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TimeForChange.Attraction
{
    public class AttractionSystem : MonoBehaviour
    {
        [Range(0,33), SerializeField] private float _maxDistance = 5f;
        [SerializeField] private Camera _fpsCamera;
    
        [Range(1, 25), SerializeField] private float _powerOfGrab = 3f;
        [SerializeField] private GameObject _offset;
        [SerializeField] private Transform _rayOrigin;

        private InputAction _mouseAction;
        private readonly LayerMask _attractionLayer = 1 << 8;
        private RaycastHit _raycastHit;

        private bool IsGrab { get; set;}
        public GameObject CurrentObj { get; private set; }
        
        private void Awake() => _mouseAction = new PlayerAction().Player.LMB;

        private void OnEnable()
        {
            _mouseAction.Enable();
            _mouseAction.performed += LeftButtonDown;
            _mouseAction.canceled += LeftButtonUp;
        }
        
        private IEnumerator InvokeCheckRadius()
        {
            IsGrab = false;
            if (CurrentObj.GetComponent<AttractionObject>().GroundRadius != null)
            {
                yield return StartCoroutine(CurrentObj.GetComponent<AttractionObject>().CheckRadius(this));
            }
            CurrentObj = null;
        }
        
        private void LeftButtonDown(InputAction.CallbackContext obj)
        {
            if (Physics.Raycast(_rayOrigin.position, _fpsCamera.transform.forward, out _raycastHit, _maxDistance, 
                    _attractionLayer) && IsGrab == false)
            {
                if (_raycastHit.collider != null && _raycastHit.collider.GetComponent<AttractionObject>().IsActivate)
                {
                    IsGrab = true;
                    CurrentObj = _raycastHit.collider.gameObject;
                    CurrentObj.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
        
        private void LeftButtonUp(InputAction.CallbackContext obj)
        {
            if (CurrentObj != null)
            {
                StartCoroutine(InvokeCheckRadius());
            }
        }
        
        private void FixedUpdate()
        {
            if (!IsGrab) return;
        
            var grab = (_offset.transform.position - (_raycastHit.transform.position + _raycastHit.rigidbody.centerOfMass)) * _powerOfGrab;
            _raycastHit.rigidbody.velocity = grab;
        }
        
        private void OnDisable()
        {
            _mouseAction.performed -= LeftButtonDown;
            _mouseAction.canceled -= LeftButtonUp;
            _mouseAction.Disable();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(_rayOrigin.position, _fpsCamera.transform.forward * _maxDistance);
        }
    }
}