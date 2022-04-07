using System.Collections;
using System.Collections.Generic;
using TimeForChange.Dialogue;
using TimeForChange.Quests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TimeForChange.Data
{
    [CreateAssetMenu(fileName = "DialogueDate", menuName = "Date/UI/DialogueDate", order = 1)]
    public class DialogueData : ScriptableObject
    {
        [Header("Additional data")]
        [SerializeField] private PressData _pressData;
        [SerializeField] private PlayerData _playerData;
        
        private TextMeshProUGUI _name;
        private TextMeshProUGUI _dialogueText;
        private List<Button> _choiceButtons;
        private List<TextMeshProUGUI> _textButtons;
        private GameObject _choiceMenu;
        private GameObject _dialogueObject;

        public void Init(TextMeshProUGUI name, TextMeshProUGUI text, List<Button> choiceButtons,
            List<TextMeshProUGUI> textButtons, GameObject choiceMenu, GameObject dialogue)
        {
            _name = name;
            _dialogueText = text;
            _choiceButtons = choiceButtons;
            _textButtons = textButtons;
            _choiceMenu = choiceMenu;
            _dialogueObject = dialogue;
        }

        private void Dialogue(List<TextData.Dialogue> textDataDialogue, int index)
        {
            _name.text = "" + textDataDialogue[index].Name;
            _name.color = textDataDialogue[index].NameColor;
            _dialogueText.text = textDataDialogue[index].Text;  
        }

        public IEnumerator PlayingDialogue(List<TextData.Dialogue> dialogues, int startIndex, 
            int endIndex, float timeToNextDialogue)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                Dialogue(dialogues, i);
                yield return new WaitForSeconds(timeToNextDialogue);
            }
        }

        public void FirstTalk(string pressText)
        {
            _pressData.ChangeText.Invoke(pressText);
            _pressData.Press.gameObject.SetActive(true);
            _pressData.Knob.gameObject.SetActive(false);
        }
        
        public void Press()
        {
            _pressData.Press.gameObject.SetActive(false);
            _choiceMenu.SetActive(false);
            
            _dialogueObject.SetActive(true);
            _playerData.DisableInput();
        }
        
        public void Exit()
        {
            _pressData.Press.gameObject.SetActive(false);
            _pressData.Knob.gameObject.SetActive(true);
            
            _dialogueObject.SetActive(false);
            _playerData.EnableInput();
        }

        public IEnumerator DialogueWithChoice(TextData textData, DialogueSystem dialogueSystem, Collider npsCollider)
        {
            if (textData.WithChoice)
            {
                _playerData.ChangeCursorState(1);
                _choiceMenu.SetActive(true);

                for (int i = 0; i < _choiceButtons.Count; i++)
                {
                    var clickIndex = i;
                    
                    _textButtons[i].text = "" + textData.ChoiceButton[i].Text;
                    _choiceButtons[i].onClick?.AddListener(() 
                        => dialogueSystem.OnButtonClick(textData.ChoiceButton[clickIndex].IsGoodChoice));
                }
            }
            else
            {
                npsCollider.enabled = false;
                Exit();
            }
            yield break;
        }
        
        public IEnumerator DialogueAfterChoice(TextData textData, int startIndex, int endIndex, float timeToNextDialog, 
            Collider npsCollider, bool goodChoice, QuestBehaviour questBehaviour)
        {
            if (textData.WithAfterChoice)
            {
                yield return PlayingDialogue(textData.DialogueAfterChoice, startIndex, endIndex, timeToNextDialog);

                Exit();
                npsCollider.enabled = false;
                
                if (!goodChoice) yield break;
                questBehaviour.NextState();
            }
        }

        public void RemoveListeners()
        {
            foreach (var button in _choiceButtons)
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }
}