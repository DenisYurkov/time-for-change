using UnityEngine;

namespace TimeForChange.Player
{
    public class Climbing : MonoBehaviour
    {
        public bool IsClimbing; 

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovement>()) 
                IsClimbing = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerMovement>()) 
                IsClimbing = false;
        }
    }
}
