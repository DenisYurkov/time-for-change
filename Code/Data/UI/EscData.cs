using UnityEngine;
using UnityEngine.UI;

namespace TimeForChange.Data
{
    [CreateAssetMenu(fileName = "EscDate", menuName = "Date/UI/EscDate", order = 0)]
    public class EscData : ScriptableObject
    {
        [Header("UI and Buttons")]
        public GameObject EscMenu;
        public Button PlayButton;
        public Button MenuButton;
        
        [Header("Sliders")]
        public Slider SliderAudio;
        public Slider SliderSensitivity;

        [Header("Scene settings")]
        public int MainMenuIndex;

        public void Init(GameObject escMenu, Button playButton, Button menuButton, Slider sliderAudio, Slider sliderSensitivity)
        {
            EscMenu = escMenu;
            PlayButton = playButton;
            MenuButton = menuButton;
            SliderAudio = sliderAudio;
            SliderSensitivity = sliderSensitivity;
        }
    }
}