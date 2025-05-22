using UnityEngine.InputSystem;
using UnityEngine;

namespace Gameplay
{
    public class GroundedState : IPlayerState
    {
        private PlayerController _controller;

        public void Enter(PlayerController controller)
        {
            _controller = controller;
            Debug.Log("Grounded state!");
        }

        public void Exit() {}

        public void HandleMove(InputAction.CallbackContext ctx)
        {
            var direction = ctx.ReadValue<Vector2>().ToHorizontalPlane();
            _controller.Character.SetDirection(direction);
        }

        public void HandleJump(InputAction.CallbackContext ctx)
        {
            _controller.RunJumpCoroutine();
        }

        public void OnCollisionEnter(Collision collision) {}

        public void Update() {}
    }
}
