using System.Collections;
using DG.Tweening;
using TimeForChange.Data;
using TimeForChange.Extensions;
using TimeForChange.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TimeForChange.Booth
{
    [RequireComponent(typeof(BoxCollider))]
    public class PhoneBooth : MonoBehaviour
    {
        [Header("Data")] 
        [SerializeField] private DialogueData _dialogueData;
        [SerializeField] private PressData _pressData;

        [Header("Door Animation: ")]
        [SerializeField] private Transform _doorPivot;

        [Header("Position")]
        [SerializeField] private float _x, _z, _doorDuration;
        [SerializeField] private Transform _mainParent;
    
        [Header("Check if player inside Booth: ")]
        [SerializeField] private Transform _boothPos;
        [SerializeField] private float _boothRadius;

        [Header("Teleport Settings: ")]
        [SerializeField] private Transform _teleportPos;
        [SerializeField] private bool _endBooth;
        [SerializeField] private int _goodScene, _badScene;

        private InputAction _interactAction;
        private Collider[] _getPlayers;
        private Vector3 _endValue;
        
        private readonly LayerMask _playerLayer = 1 << 6;
        private bool _isPress, _insideBooth, _inTrigger;

        private void Awake()
        {
            _interactAction = new PlayerAction().Player.InteractButton;
            _endValue = new Vector3(_x, _mainParent.eulerAngles.y, _z);
        }

        private void OnEnable()
        {
            _interactAction.Enable();
            _interactAction.performed += ButtonPress;
        }
        
        private void ButtonPress(InputAction.CallbackContext obj)
        {
            if (_inTrigger && !_isPress)
            {
                _isPress = true;
                _pressData.Press.gameObject.SetActive(true);
            
                RotateDoor(Quaternion.Euler(_endValue), _doorDuration);
                _pressData.Press.gameObject.SetActive(false);
            }
        }
        
        private void FixedUpdate()
        {
            _insideBooth = Physics.CheckSphere(_boothPos.position, _boothRadius, _playerLayer);
            if (_insideBooth) StartCoroutine(TeleportPlayer());
        }

        private IEnumerator TeleportPlayer()
        {
            _getPlayers = Physics.OverlapSphere(_boothPos.position, _boothRadius, _playerLayer);
            foreach (var player in _getPlayers)
            {
                if (_endBooth)
                {
                    Ending.ChoiceEnd(_goodScene, _badScene);
                }
                else
                {
                    player.transform.position = _teleportPos.position;
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    
        private void RotateDoor(Quaternion endValue, float duration) => _doorPivot.DORotateQuaternion(endValue, duration);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement _) && !_isPress)
            {
                _dialogueData.FirstTalk("Press E to open door");
                _inTrigger = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement _))
            {
                _dialogueData.Exit();
                _inTrigger = false;
            }
        }

        private void OnDisable()
        {
            _interactAction.performed -= ButtonPress;
            _interactAction.Disable();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_boothPos.position, _boothRadius);
        }
    }
}