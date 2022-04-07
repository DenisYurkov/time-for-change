using TimeForChange.EnumHelpers;
using TimeForChange.Player;
using UnityEngine;

namespace TimeForChange.Data
{
    [CreateAssetMenu(fileName = "PlayerDate", menuName = "Date/PlayerDate", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public PlayerMovement PlayerMovement;
        public MouseLook MouseLook;

        public void Init(PlayerMovement playerMovement, MouseLook mouseLook)
        {
            PlayerMovement = playerMovement;
            MouseLook = mouseLook;
        }

        public void EnableInput()
        {
            PlayerMovement.enabled = true;
            MouseLook.IsRotate(true);
            ChangeCursorState(0);
        }

        public void DisableInput()
        {
            PlayerMovement.enabled = false;
            MouseLook.IsRotate(false);
            ChangeCursorState(1);
        }
        
        public void ChangeCursorState(int cursorState)
        {
            switch (cursorState)
            {
                case (int)CursorState.Invisible:
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Confined;
                    break;
                case (int)CursorState.Visible:
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                default:
                    Debug.Log("<color=red><b>Error: Not Correct Cursor State!", this);
                    break;
            }
        }
    }
}