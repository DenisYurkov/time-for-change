using TimeForChange.AI;
using TimeForChange.Data;
using TimeForChange.Dialogue;
using TimeForChange.Extensions;
using TimeForChange.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace TimeForChange.Quests
{
    public class MotherQuest : MonoBehaviour, IQuest
    {
        [Header("Data")] 
        [SerializeField, FormerlySerializedAs("_questDate")] private QuestData _questData;
        [SerializeField, FormerlySerializedAs("_changeDate")] private TextData _changeData;
        [SerializeField, FormerlySerializedAs("_taskDate")] private TaskData _taskData;
        
        [Header("Quest Objects")]
        [SerializeField] private BoxCollider _daughterBoxCollider;
        [SerializeField] private GameObject _aidKitGameObject;

        [Header("AI")] 
        [SerializeField] private WalkToPoint _daughterWalkToPoint;
        [SerializeField] private Transform _daughterMoveTo;
        [SerializeField] private Transform _moveTo;
        
        private QuestBehaviour _questBehaviour;
        private DialogueSystem _dialogueSystem;
        private WalkToPoint _motherWalkToPoint;
        
        private BoxCollider _motherCollider;
        private GameObject _taskPref;

        private void Awake()
        {
            _questBehaviour = GetComponentInParent<QuestBehaviour>();
            _dialogueSystem = GetComponent<DialogueSystem>();
            _motherCollider = GetComponent<BoxCollider>();
            _motherWalkToPoint = GetComponent<WalkToPoint>();
        }

        private void Start() => _questBehaviour.SetQuest(this);

        public void StartQuest()
        {
            _daughterBoxCollider.enabled = true;
            Extension.AddTask(out _taskPref, _questData, _taskData);
        }
        
        public void PassingQuest()
        {
            _daughterWalkToPoint.MoveAI(_daughterMoveTo);
            _dialogueSystem.ChangeTextDate(_changeData);
            _motherCollider.enabled = true;
        }

        public void EndQuest()
        {
            Ending.Score += 1;
            Destroy(_taskPref);
            
            _aidKitGameObject.SetActive(true);
            
            _motherWalkToPoint.MoveAI(_moveTo);
            _daughterWalkToPoint.MoveAI(_moveTo);
        }
    }
}