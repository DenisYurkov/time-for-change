using TimeForChange.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace TimeForChange.UI
{
    public class EscMenu : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField, FormerlySerializedAs("_playerDate")] private PlayerData _playerData;
        [SerializeField, FormerlySerializedAs("_escDate")] private EscData _escData;

        private InputAction _escAction;
    
        private void Awake()
        {
            _escAction = new PlayerAction().Player.EscMenu;

            _escData.PlayButton.onClick.AddListener(Hide);
            _escData.MenuButton.onClick.AddListener(MainMenu);
        }

        private void OnEnable()
        {
            _escAction.Enable();
            _escAction.performed += ControlState;
        }

        private void Update()
        {
            AudioListener.volume = _escData.SliderAudio.value;
            _playerData.MouseLook.MouseSpeed = _escData.SliderSensitivity.value;
        }

        private void ControlState(InputAction.CallbackContext obj)
        {
            if (_escData.EscMenu.activeSelf == false) Show();
            else Hide();
        }

        private void Show()
        {
            _escData.EscMenu.SetActive(true);
            _playerData.DisableInput();
        }

        private void Hide()
        {
            _escData.EscMenu.SetActive(false);
            _playerData.EnableInput();
        }

        public void MainMenu() => SceneManager.LoadScene(_escData.MainMenuIndex);

        private void OnDisable()
        {
            _escAction.performed -= ControlState;
            _escAction.Disable();
        }

        private void OnDestroy()
        {
            _escData.PlayButton.onClick.RemoveListener(Hide);
            _escData.MenuButton.onClick.RemoveListener(MainMenu);
        }
    }
}
