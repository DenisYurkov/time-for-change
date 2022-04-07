using UnityEngine;

namespace TimeForChange.Data
{
    [CreateAssetMenu(fileName = "TaskDate", menuName = "Date/UI/TaskDate", order = 0)]
    public class TaskData : ScriptableObject
    {
        public GameObject TaskUI, DropdownPref;
        public RectTransform TaskGrid;

        public void Init(GameObject taskUI, GameObject dropdownPref, RectTransform taskGrid)
        {
            TaskUI = taskUI;
            DropdownPref = dropdownPref;
            TaskGrid = taskGrid;
        }
    }
}