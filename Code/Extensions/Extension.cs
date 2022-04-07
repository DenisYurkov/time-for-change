using TimeForChange.Data;
using TimeForChange.Task;
using UnityEngine;

namespace TimeForChange.Extensions
{
    public static class Extension
    {
        public static bool DistanceBetweenRadius(Vector3 a, Vector3 b, float radius) => Vector3.Distance(a, b) < radius;

        public static void SetTransformWithScale(Transform root, Transform to)
        {
            root.position = to.position;
            root.rotation = to.rotation;
            root.localScale = to.localScale;
        }
        
        public static void AddTask(out GameObject taskPref, QuestData questData, TaskData taskData)
        {
            GameObject task = null;
            
            task = Object.Instantiate(taskData.DropdownPref, 
                taskData.TaskGrid.position, taskData.TaskGrid.rotation, taskData.TaskGrid);

            task.GetComponent<TaskRef>().TMPQuestName.text = questData.QuestPurpose;
            task.GetComponent<TaskRef>().TMPQuestDescription.text = questData.QuestDescription;
            
            taskPref = task;
        }
    }
}
