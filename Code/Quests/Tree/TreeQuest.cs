using System.Collections.Generic;
using TimeForChange.AI;
using TimeForChange.Data;
using TimeForChange.Dialogue;
using TimeForChange.Extensions;
using TimeForChange.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace TimeForChange.Quests
{
    public class TreeQuest : MonoBehaviour, IQuest
    {
        [Header("Dates")] 
        [SerializeField, FormerlySerializedAs("_questDate")] private QuestData _questData;
        [SerializeField, FormerlySerializedAs("_changeDate")] private TextData _changeData;
        [SerializeField, FormerlySerializedAs("_taskDate")] private TaskData _taskData;
        
        [Header("Quest Objects")]
        [SerializeField] private BoxCollider _axeCollider;
        
        [Header("AI")] 
        [SerializeField] private List<WalkToPoint> _manWalkToPoints;
        [SerializeField] private Transform _moveTo;
        
        private QuestBehaviour _questBehaviour;
        private DialogueSystem _dialogueSystem;
        
        private BoxCollider _businessmanCollider;
        private GameObject _taskPref;

        private void Awake()
        {
            _questBehaviour = GetComponentInParent<QuestBehaviour>();
            _dialogueSystem = GetComponent<DialogueSystem>();
            _businessmanCollider = GetComponent<BoxCollider>();
        }

        private void Start() => _questBehaviour.SetQuest(this);

        public void StartQuest()
        {
            _axeCollider.enabled = true;
            Extension.AddTask(out _taskPref, _questData, _taskData);
        }

        public void PassingQuest()
        {
            _dialogueSystem.ChangeTextDate(_changeData);
            _businessmanCollider.enabled = true;
        }

        public void EndQuest()
        {
            Ending.Score += 1;
            Destroy(_taskPref);

            foreach (var man in _manWalkToPoints)
            {
                man.MoveAI(_moveTo);
            }
        }
    }
}