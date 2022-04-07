using UnityEngine;
using UnityEngine.InputSystem;

namespace TimeForChange.Player
{
    public class MouseLook : MonoBehaviour
    {
        public float MouseSpeed;
        
        private float _xRotation;
        private bool _isCameraRotate = true;
        private InputAction _mouseAction;

        private void Awake() => _mouseAction = new PlayerAction().Player.Mouse;

        private void OnEnable() => _mouseAction.Enable();

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (_isCameraRotate)
            {
                CameraRotate(_mouseAction.ReadValue<Vector2>().x, _mouseAction.ReadValue<Vector2>().y);
            }
        }

        private void CameraRotate(float x, float z)
        {
            float mouseX = x * MouseSpeed * Time.deltaTime;
            float mouseY = z * MouseSpeed * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            transform.parent.Rotate(Vector3.up * mouseX);
        }

        public void IsRotate(bool isCameraRotate) => _isCameraRotate = isCameraRotate;

        private void OnDisable() => _mouseAction.Disable();
    }
}
