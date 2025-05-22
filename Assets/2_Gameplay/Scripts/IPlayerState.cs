using UnityEngine.InputSystem;
using UnityEngine;

namespace Gameplay
{
    public interface IPlayerState
    {
        void Enter(PlayerController controller);
        void Exit();
        void HandleMove(InputAction.CallbackContext ctx);
        void HandleJump(InputAction.CallbackContext ctx);
        void OnCollisionEnter(Collision collision);
        void Update();
    }
}
