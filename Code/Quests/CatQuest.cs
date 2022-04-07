using TimeForChange.AI;
using TimeForChange.Attraction;
using TimeForChange.Data;
using TimeForChange.Dialogue;
using TimeForChange.Interfaces;
using TimeForChange.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace TimeForChange.Quests
{
    public class CatQuest : MonoBehaviour, IQuest
    {
        [Header("Data")] 
        [SerializeField, FormerlySerializedAs("_questDate")] private QuestData _questData;
        [SerializeField, FormerlySerializedAs("_changeDate")] private TextData _changeData;
        [SerializeField, FormerlySerializedAs("_taskDate")] private TaskData _taskData;
        
        [Header("Quest Objects")]
        [SerializeField] private AttractionObject _catObject;
        [SerializeField] private BoxCollider _ladderTrigger;
        
        [Header("AI")]
        [SerializeField] private Transform _moveTo;

        private QuestBehaviour _questBehaviour;
        private DialogueSystem _dialogueSystem;
        private WalkToPoint _policemanPoint;
        
        private BoxCollider _policemanCollider;
        private GameObject _taskPref;

        private void Awake()
        {
            _questBehaviour = GetComponentInParent<QuestBehaviour>();
            _dialogueSystem = GetComponent<DialogueSystem>();
            _policemanCollider = GetComponent<BoxCollider>();
            _policemanPoint = GetComponent<WalkToPoint>();
        }

        private void Start() => _questBehaviour.SetQuest(this);

        public void StartQuest()
        {
            _ladderTrigger.enabled = true;
            _catObject.IsActivate = true;
            
            Extension.AddTask(out _taskPref, _questData, _taskData);
        }

        public void PassingQuest()
        {
            _dialogueSystem.ChangeTextDate(_changeData);
            _policemanCollider.enabled = true;
        }

        public void EndQuest()
        {
            Ending.Score += 1;
            Destroy(_taskPref);

            _policemanPoint.MoveAI(_moveTo);
        }
    }
}