using System;
using System.Collections.Generic;
using UnityEngine;

namespace TimeForChange.Data
{
    [CreateAssetMenu(fileName = "TextDate", menuName = "Text/DialogueDate")]
    public class TextData : ScriptableObject
    {
        [Serializable]
        public struct Dialogue
        {
            public string Name;
            [TextArea(3, 25)] public string Text;
            public Color NameColor;
        }

        [Serializable]
        public struct Buttons
        {
            [TextArea(3, 25)] public string Text;
            public bool IsGoodChoice;
        }

        [Header("Text: ")] [Space]
        public List<Dialogue> Dialogues;
        public List<Buttons> ChoiceButton;
        public List<Dialogue> DialogueAfterChoice;
        
        [Header("Choice")]
        public bool WithChoice;
        public bool WithAfterChoice;

        [Header("Start Index")] 
        public int GoodIndex;
        public int BadIndex;
    }
}