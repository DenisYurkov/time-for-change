using TimeForChange.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TimeForChange.Init
{
    public class EscInit : MonoBehaviour
    {
        [Header("Data")] 
        [SerializeField] private EscData _escData;
        
        [Header("UI and Buttons")]
        [SerializeField] private GameObject _escMenu;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _menuButton;
        
        [Header("Sliders")]
        [SerializeField] private Slider _sliderAudio;
        [SerializeField] private Slider _sliderSensitivity;

        private void Awake() => _escData.Init(_escMenu, _playButton, _menuButton, _sliderAudio, _sliderSensitivity);
    }
}