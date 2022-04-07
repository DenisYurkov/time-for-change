using TimeForChange.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TimeForChange.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Player Settings")]
        [SerializeField] private float _moveSpeed = 8f;
        [SerializeField] private Climbing _climbing;

        private InputAction _moveAction;
        private CharacterController _playerController;
        
        private Animator _playerAnimator;
        private const string WalkTrigger = "isWalk";
        private static readonly int IsWalk = Animator.StringToHash(WalkTrigger);
    
        private void Awake()
        {
            _playerController = GetComponent<CharacterController>();
            _playerAnimator = GetComponent<Animator>();
            _moveAction = new PlayerAction().Player.Move;
        }

        private void OnEnable() => _moveAction.Enable();

        private void Start() => Ending.Score = 0;

        private void Update()
        {
            Move(_moveAction.ReadValue<Vector2>().x, _moveAction.ReadValue<Vector2>().y, _climbing.IsClimbing);
        }

        private void Move(float x, float z, bool isClimbing)
        {
            Vector3 velocity = isClimbing
                ? (transform.right * x + Vector3.up * z) * _moveSpeed 
                : (transform.right * x + transform.forward * z) * _moveSpeed + Physics.gravity;

            _playerController.Move(velocity * Time.deltaTime);
            _playerAnimator.SetBool(IsWalk, _playerController.velocity.magnitude > 0);
        }
        
        private void OnDisable() => _moveAction.Disable();
    }
}