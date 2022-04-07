using System.Collections;
using JetBrains.Annotations;
using TimeForChange.Data;
using TimeForChange.EnumHelpers;
using TimeForChange.Quests;
using UnityEngine;

namespace TimeForChange.Dialogue
{
    [RequireComponent(typeof(NpsTrigger))]
    public class DialogueSystem : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private TextData _textData;
        [SerializeField] private DialogueData _dialogueData;

        [Header("Dialogue Settings: ")] 
        [SerializeField] private float _timeToNextDialog = 1f;
        [SerializeField, CanBeNull] private QuestBehaviour _questBehaviour;
        
        private BoxCollider _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            if (_questBehaviour == null) _questBehaviour = GetComponentInParent<QuestBehaviour>();
        }

        public void ChangeTextDate(TextData changeData) => _textData = changeData;

        public IEnumerator InvokeDialogue()
        {
            yield return _dialogueData.PlayingDialogue(_textData.Dialogues,0, 
                _textData.Dialogues.Count, _timeToNextDialog);

            if (_questBehaviour is { } && _questBehaviour.GetState() != QuestState.StartQuest)
                _questBehaviour.NextState();

            yield return _dialogueData.DialogueWithChoice(_textData, this, _collider);
        }
        
        public void OnButtonClick(bool goodChoice)
        {
            _dialogueData.RemoveListeners();
            _collider.enabled = false;
            
            if (goodChoice)
            {
                StartCoroutine(_dialogueData.DialogueAfterChoice(_textData, _textData.GoodIndex, 
                    _textData.BadIndex, _timeToNextDialog, _collider, true, _questBehaviour));
            }
            else
            {
                StartCoroutine(_dialogueData.DialogueAfterChoice(_textData, _textData.BadIndex, 
                    _textData.DialogueAfterChoice.Count, _timeToNextDialog, _collider, false, _questBehaviour));
            }
        }
    }
}