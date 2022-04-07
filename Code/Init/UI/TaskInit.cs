using TimeForChange.Data;
using UnityEngine;

namespace TimeForChange.Init
{
    public class TaskInit : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private TaskData _taskData;
        
        [Header("UI")]
        [SerializeField] private GameObject _taskUI, _dropdownPref;
        [SerializeField] private RectTransform _taskGrid;
        
        private void Awake() => _taskData.Init(_taskUI, _dropdownPref, _taskGrid);
    }
}