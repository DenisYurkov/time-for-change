using UnityEngine;

namespace TimeForChange.FPS
{
    public class FpsSettings : MonoBehaviour
    { 
        private void Start()
        {
            Application.targetFrameRate = 70;
            QualitySettings.vSyncCount = 1;
        }
    }
}