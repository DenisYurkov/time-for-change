using System.Collections.Generic;
using TimeForChange.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TimeForChange.Init
{
    public class DialogueInit : MonoBehaviour
    {
        [SerializeField] private DialogueData _data;
        
        [Header("Objects")] 
        [SerializeField] private GameObject _dialogue, _choiceMenu;
        
        [Header("Buttons")]
        [SerializeField] private List<Button> _buttonsComponents;
        [SerializeField] private List<TextMeshProUGUI> _buttonText;

        [Header("Text")] 
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _dialogueText;

        private void Awake() => _data.Init(_name, _dialogueText, _buttonsComponents, _buttonText, _choiceMenu, _dialogue);
    }
}
