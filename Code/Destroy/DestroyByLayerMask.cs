using UnityEngine;

namespace TimeForChange.Destroy
{
    public class DestroyByLayerMask : MonoBehaviour
    {
        [Header("Settings: ")] [SerializeField]
        private LayerMask _layerMask;
    
        private void OnTriggerEnter(Collider other)
        {
            if (1 << other.gameObject.layer == _layerMask)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
