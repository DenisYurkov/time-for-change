using TimeForChange.Data;
using TMPro;
using UnityEngine;

namespace TimeForChange.Init
{
    public class PressInit : MonoBehaviour
    {
        [SerializeField] private PressData _pressData;
        [SerializeField] private GameObject _knob;
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        
        private void Awake() => _pressData.Init(_textMeshProUGUI, _knob);
    }
}
