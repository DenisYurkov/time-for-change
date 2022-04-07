using TimeForChange.Data;
using TimeForChange.Extensions;
using TimeForChange.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace TimeForChange.Quests
{
    public class Axe : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("_dialogueDate")] private DialogueData _dialogueData;
        [SerializeField] private Transform _transformToTeleport;
        [SerializeField] private BoxCollider _treeTrigger;
        [SerializeField] private Transform _parent;
    
        private BoxCollider _collider;
        private InputAction _interactAction;
        private bool _isTrigger;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _interactAction = new PlayerAction().Player.InteractButton;
        }

        private void OnEnable()
        {
            _interactAction.Enable();
            _interactAction.performed += ButtonPress;
        }

        private void ButtonPress(InputAction.CallbackContext obj)
        {
            if (_isTrigger)
            {
                _dialogueData.Exit();
                Interact();
            
                _treeTrigger.enabled = true;
                Destroy(_collider);
            }
        }
        private void Interact()
        {
            transform.SetParent(_parent);
            Extension.SetTransformWithScale(transform, _transformToTeleport);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement _))
            {
                _dialogueData.FirstTalk("Press E to take axe");
                _isTrigger = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _dialogueData.Exit();
            _isTrigger = false;
        }
        
        private void OnDisable()
        {
            _interactAction.performed -= ButtonPress;
            _interactAction.Disable();
        }
    }
}
