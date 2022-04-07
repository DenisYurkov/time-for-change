using TimeForChange.Data;
using TimeForChange.Player;
using UnityEngine;

namespace TimeForChange.Init
{
    public class PlayerInit : MonoBehaviour
    { 
        [SerializeField] private PlayerData _data;

        [Header("Input")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private MouseLook _mouseLook;

        private void Awake() => _data.Init(_playerMovement, _mouseLook);
    }
}
