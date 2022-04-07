using System;
using TMPro;
using UnityEngine;

namespace TimeForChange.Data
{
    [CreateAssetMenu(fileName = "PressDate", menuName = "Date/UI/PressDate", order = 0)]
    public class PressData : ScriptableObject
    {
        [Header("Object")] 
        public GameObject Knob;
        public TextMeshProUGUI Press;
        public Action<string> ChangeText;

        public void Init(TextMeshProUGUI press, GameObject knob)
        {
            Press = press;
            Knob = knob;
            
            ChangeText = delegate(string changeText) 
            {
                Press.text = changeText;
            };
        }
    }
}