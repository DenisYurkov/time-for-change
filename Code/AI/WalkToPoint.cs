using UnityEngine;
using UnityEngine.AI;

namespace TimeForChange.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class WalkToPoint : MonoBehaviour
    {
        [Header("Ignore Collision")] [SerializeField]
        private Collider _ignoreCollider;
        private MeshCollider _aiCollider;

        private NavMeshAgent _navMeshAgent;
        private Animator _npsAnimator;
        private static readonly int IsWalk = Animator.StringToHash(WalkTrigger);
        private const string WalkTrigger = "isWalk";

        private bool CheckAIDistance => _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _npsAnimator = GetComponent<Animator>();
            _aiCollider = GetComponent<MeshCollider>();
        }

        public void MoveAI(Transform point) => _navMeshAgent.SetDestination(point.transform.position);

        private void Update()
        {
            Physics.IgnoreCollision(_ignoreCollider, _aiCollider, !CheckAIDistance);
            _npsAnimator.SetBool(IsWalk, !CheckAIDistance);
        }
    }
}
