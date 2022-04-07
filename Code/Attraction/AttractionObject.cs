using System.Collections;
using JetBrains.Annotations;
using TimeForChange.Extensions;
using TimeForChange.Quests;
using UnityEngine;

namespace TimeForChange.Attraction
{
    [RequireComponent(typeof(Rigidbody))]
    public class AttractionObject : MonoBehaviour
    {
        [SerializeField] private bool _isActivate;
        
        [CanBeNull] public PutOnGround GroundRadius;
        public LayerMask AttractionType;
        public int Index;

        private MeshCollider _meshCollider;

        private void Awake() => _meshCollider = GetComponent<MeshCollider>();

        public bool IsActivate
        {
            get => _isActivate;
            set => _isActivate = value;
        }
    
        public IEnumerator CheckRadius(AttractionSystem attractionSystem)
        {
            if (Extension.DistanceBetweenRadius(transform.position, GroundRadius.transform.position, GroundRadius.CheckRadius))
            {
                StartCoroutine(GroundRadius.PutInPos(attractionSystem, this));
                Destroy(_meshCollider);
            }
            yield return null;
        }
    }
}
