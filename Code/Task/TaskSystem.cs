using TimeForChange.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace TimeForChange.Task
{
    public class TaskSystem : MonoBehaviour
    {
        [Header("Data")] 
        [SerializeField, FormerlySerializedAs("_playerDate")] private PlayerData _playerData;
        [SerializeField, FormerlySerializedAs("_taskDate")] private TaskData _taskData;
    
        private InputAction _taskAction;

        private void Awake() => _taskAction = new PlayerAction().Player.TaskMenu;

        private void OnEnable()
        {
            _taskAction.Enable();
            _taskAction.performed += TaskState;
        }

        private void TaskState(InputAction.CallbackContext obj)
        {
            if (_taskData.TaskUI.activeSelf == false)
            {
                _taskData.TaskUI.SetActive(true);
                _playerData.DisableInput();
            }
            else
            {
                _taskData.TaskUI.SetActive(false);
                _playerData.EnableInput();
            }
        }

        private void OnDisable()
        {
            _taskAction.performed -= TaskState;
            _taskAction.Disable();
        }
    }
}
