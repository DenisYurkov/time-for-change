using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TimeForChange.Destruction
{
    public class Destruction : MonoBehaviour
    {
        public GameObject PrefabObject;
        public Vector3 Force;
    
        [SerializeField] private ForceMode _forceMode;
        
        private GameObject _cloneObject;
        private Rigidbody[] _rigidbodyComponent = Array.Empty<Rigidbody>();
       
        private readonly List<GameObject> _cloneList = new List<GameObject>();
        private readonly List<Rigidbody> _allRigidbody = new List<Rigidbody>();
    
        public void DestructionObject()
        {
            gameObject.SetActive(false);
            _cloneObject = Instantiate(PrefabObject, transform.position, Quaternion.identity);
            _cloneList.Add(_cloneObject);

            if (Application.isPlaying)
            {
                AddPhysics(_cloneObject);
            }
        }
    
        public void PhysicsSimulation()
        {
            if (!Application.isPlaying)
            {
                Physics.autoSimulation = false;
                Physics.Simulate(Time.fixedDeltaTime);

                AddPhysics(_cloneObject);
            }
            else
            {
                Debug.LogWarning("<b><color>It's not the Editor, it's Play mode!</color></b>");
            }
        }

        public void AllObjectFall()
        {
            _allRigidbody.All(r => r.useGravity = true);
        }

        private void AddPhysics(GameObject clonePref)
        {
            Physics.autoSimulation = true;
        
            _rigidbodyComponent = clonePref.GetComponentsInChildren<Rigidbody>();
            foreach (var rigidbody1 in _rigidbodyComponent.ToList().Where(rb => !_allRigidbody.Contains(rb)))
            {
                _allRigidbody.Add(rigidbody1);
            }

            foreach (var r in  _allRigidbody)
            {
                r.AddForce(Force, _forceMode);
            }
        }

        public void Reset()
        {
            if (_cloneList.Count > 0)
            {
                gameObject.SetActive(true);
                foreach (var prefClone in _cloneList)
                {
                    if (Application.isEditor)
                    {
                        DestroyImmediate(prefClone);
                    }
                    else
                    {
                        Destroy(prefClone);
                    }
                }
                _allRigidbody.Clear();
                _rigidbodyComponent = Array.Empty<Rigidbody>();
                _cloneList.Clear();
            }
        }
    }
}