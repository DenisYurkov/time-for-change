using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace TimeForChange.Editor
{
    #if UNITY_EDITOR
    public class FindInEditor : ScriptableWizard
    {
        [MenuItem("Extensions/Find All Mesh Colliders in scene.")]
        private static void CreateWizard()
        {
            DisplayWizard("Select objects of type", typeof(FindInEditor), "Select");
        }

        private void OnWizardCreate()
        {
            var objects = FindObjectsOfType<MeshCollider>();
            var selection = new ArrayList();

            foreach (var obj in objects)
            {
                selection.Add(obj.gameObject);
            }   
            Selection.objects = selection.ToArray(typeof(GameObject)) as GameObject[];
        }
    }
    #endif
}
