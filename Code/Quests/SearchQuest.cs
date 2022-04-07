using System.Collections.Generic;
using JetBrains.Annotations;
using TimeForChange.AI;
using TimeForChange.Attraction;
using TimeForChange.Data;
using TimeForChange.Dialogue;
using TimeForChange.Extensions;
using TimeForChange.Interfaces;
using UnityEngine;

namespace TimeForChange.Quests
{
    public class SearchQuest : MonoBehaviour, IQuest
    {
        [Header("Data")] 
        [SerializeField] private QuestData _questData;
        [SerializeField] private TextData _changeData;
        [SerializeField] private TaskData _taskData;
        
        [Header("Quest Objects")]
        [SerializeField] private List<AttractionObject> _attractionObjects;
        [SerializeField, CanBeNull] private GameObject _vfxObject;
        
        [Header("AI")]
        [SerializeField] private Transform _moveTo;
        
        private QuestBehaviour _questBehaviour;
        private DialogueSystem _dialogueSystem;
        private WalkToPoint _npsWalkToPoint;
        
        private BoxCollider _npsCollider;
        private GameObject _taskPref;
        private bool _isVfxNotNull;

        private void Awake()
        {
            _questBehaviour = GetComponentInParent<QuestBehaviour>();
            _dialogueSystem = GetComponent<DialogueSystem>();
            _npsCollider = GetComponent<BoxCollider>();
            _npsWalkToPoint = GetComponent<WalkToPoint>();
        }

        private void Start()
        {
            _isVfxNotNull = _vfxObject != null;
            _questBehaviour.SetQuest(this);
        }

        public void StartQuest()
        {
            Extension.AddTask(out _taskPref, _questData, _taskData);
            foreach (var vaAttractionObject in _attractionObjects)
            {
                vaAttractionObject.IsActivate = true;
            }
        }

        public void PassingQuest()
        {
            _dialogueSystem.ChangeTextDate(_changeData);
            _npsCollider.enabled = true;

            if (_isVfxNotNull)
            {
                Destroy(_vfxObject);
            }
        }

        public void EndQuest()
        {
            Ending.Score += 1;
            Destroy(_taskPref);
            
            _npsWalkToPoint.MoveAI(_moveTo);
        }
    }
}