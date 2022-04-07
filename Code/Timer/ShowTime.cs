using TimeForChange.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace TimeForChange.Timer
{
    public class ShowTime : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("_playerDate")] private PlayerData _playerData;
        [SerializeField] private GameObject _cameraTimer;

        private InputAction _timeAction;

        private void Awake() => _timeAction = new PlayerAction().Player.Time;

        private void OnEnable()
        {
            _timeAction.Enable();
            _timeAction.performed += TimeState;
        }
        
        private void TimeState(InputAction.CallbackContext obj)
        {
            if (_cameraTimer.activeSelf == false)
            {
                _cameraTimer.SetActive(true);
                _playerData.DisableInput();
            }
            else
            {
                _cameraTimer.SetActive(false);
                _playerData.EnableInput();
            }
        }

        private void OnDisable()
        {
            _timeAction.performed -= TimeState;
            _timeAction.Disable();
        }
    }
}
