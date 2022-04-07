using TimeForChange.Data;
using TimeForChange.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TimeForChange.Dialogue
{
    [RequireComponent(typeof(DialogueSystem), typeof(BoxCollider))]
    public class NpsTrigger : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private DialogueData _dialogueData;

        [SerializeField, Header("Icon")] 
        private GameObject _icon;
        
        private DialogueSystem _dialogueSystem;
        private InputAction _talkAction;
        private bool _inTrigger;
        
        private void Awake()
        {
            _talkAction = new PlayerAction().Player.TalkButton;
            _dialogueSystem = GetComponent<DialogueSystem>();
        }

        private void OnEnable()
        {
            _talkAction.Enable();
            _talkAction.performed += ButtonPress;
        }

        private void ButtonPress(InputAction.CallbackContext obj)
        {
            if (_inTrigger)
            {
                _dialogueData.Press();
                _inTrigger = false;
                StartCoroutine(_dialogueSystem.InvokeDialogue());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement _))
            {
                _dialogueData.FirstTalk("Press F to talk");
                _icon.SetActive(false);
                _inTrigger = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement _))
            {
                _dialogueData.Exit();
                _icon.SetActive(true);
                _inTrigger = false;
            }
        }

        private void OnDisable()
        {
            _talkAction.performed -= ButtonPress;
            _talkAction.Disable();
        }
    }
}