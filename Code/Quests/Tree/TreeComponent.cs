using System.Collections;
using System.Collections.Generic;
using TimeForChange.Attraction;
using TimeForChange.Data;
using TimeForChange.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace TimeForChange.Quests
{
    public class TreeComponent : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private DialogueData _dialogueData;
        [SerializeField, FormerlySerializedAs("_playerDate")] private PlayerData _playerData;
        
        [Header("Objects")]
        [SerializeField] private List<AttractionObject> _attractionObjects;
        [SerializeField] private List<Collider> _treeColliders;
        
        [Header("Player Settings ")]
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Collider _playerCollider;
        [SerializeField] private GameObject _axe, _virtualAxeCamera;

        private InputAction _interactAction;
        private BoxCollider _collider;

        private bool _isTrigger; 
        private const string HitAnimation = "Hit";
    
        private void Awake()
        {
            _interactAction = new PlayerAction().Player.InteractButton;
            _collider = GetComponent<BoxCollider>();
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
                StartCoroutine(Hit());
            }
        }
        
        private IEnumerator Hit()
        {
            _virtualAxeCamera.SetActive(true);

            _playerData.DisableInput();
            _playerAnimator.Play(HitAnimation);
            yield return new WaitForSeconds(1.5f);

            for (int i = 0; i < _attractionObjects.Count; i++)
            {
                Physics.IgnoreCollision(_treeColliders[i], _playerCollider);
                
                _attractionObjects[i].GetComponent<Rigidbody>().isKinematic = false;
                _attractionObjects[i].IsActivate = true;
            }

            _playerData.EnableInput();
            yield return new WaitForSeconds(1f);
            _virtualAxeCamera.SetActive(false);
        
            Destroy(_collider);
            Destroy(_axe, 1);
            yield return null;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement _))
            {
                _dialogueData.FirstTalk("Press E to cut the tree");
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
