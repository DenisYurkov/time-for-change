using System.Collections;
using System.Collections.Generic;
using TimeForChange.Attraction;
using UnityEngine;

namespace TimeForChange.Quests
{
    public class PutOnGround : MonoBehaviour
    {
        [Range(1, 30)] public float CheckRadius = 3f;
        
        [Header("Quest: ")] 
        [SerializeField] private QuestBehaviour _questBehaviour;
        
        [Header("Settings")] [SerializeField] 
        private List<Transform> _putPos;
       
        [SerializeField] private LayerMask _checkType;
        [SerializeField] private bool _destroyObject;
        [SerializeField] private int _mustObjectCanBe;
        [SerializeField] private Vector3 _rotationValue;

        private int _index;

        private void IsPutAll()
        {
            _index++;
            if (_mustObjectCanBe == _index) _questBehaviour.NextState();
        }

        public IEnumerator PutInPos(AttractionSystem attractionSystem, AttractionObject attractionObject)
        {
            if (attractionSystem.CurrentObj != null && attractionObject.AttractionType == _checkType)
            {
                if (_destroyObject)
                {
                    Destroy(attractionSystem.CurrentObj);
                }
                else
                {
                    attractionSystem.CurrentObj.transform.position = _putPos[attractionObject.Index].position;
                    attractionSystem.CurrentObj.transform.rotation = Quaternion.Euler(_rotationValue);
                    Destroy(attractionSystem.CurrentObj.GetComponent<AttractionObject>());
                    Destroy(attractionSystem.CurrentObj.GetComponent<Rigidbody>());
                }
                IsPutAll();
            }
            yield return null;
        }

        private void OnDrawGizmos()
        {
            Color color = Color.yellow;
            color.a = 0.3f;
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, CheckRadius);
        }
    }
}