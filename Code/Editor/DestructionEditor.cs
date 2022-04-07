using UnityEditor;
using UnityEngine;

namespace TimeForChange.Editor
{
    public enum TimeOptions
    {
        Continue = 0,
        Stop = 1,
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(Destruction.Destruction))]
    public class DestructionEditor : UnityEditor.Editor
    {
        private TimeOptions _timeOptions;
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        
            var destruction = (Destruction.Destruction) target;
            if (GUILayout.Button("Destruction Object💥")) destruction.DestructionObject();
            if (GUILayout.Button("Physics Simulation")) destruction.PhysicsSimulation();

            EditorGUI.BeginChangeCheck();
            _timeOptions = (TimeOptions)EditorGUILayout.EnumPopup("Time: ", _timeOptions);

            if (EditorGUI.EndChangeCheck()) TimeControl(_timeOptions);
            if (GUILayout.Button("All Object Fall")) destruction.AllObjectFall();
            if (GUILayout.Button("Reset")) destruction.Reset();
        }

        private void TimeControl(TimeOptions op)
        {
            if (Application.isPlaying)
            {
                switch (op)
                {
                    case TimeOptions.Continue:
                        Physics.autoSimulation = true;
                        break;
                    case TimeOptions.Stop:
                        Physics.autoSimulation = false;
                        break;
                }
            }
            else Debug.LogWarning("<b><color>It's not the Play Mode, it's Editor!</color></b>");
        }
    }
    #endif
}