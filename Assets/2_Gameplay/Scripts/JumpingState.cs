using UnityEngine.InputSystem;
using UnityEngine;

namespace Gameplay
{
    public class JumpingState : IPlayerState
    {
        private PlayerController _controller;

        public JumpingState() {}

        public void Enter(PlayerController controller)
        {
            _controller = controller;
            _controller.RunJumpCoroutine();
        }

        public void Exit() {}

        public void HandleMove(InputAction.CallbackContext ctx)
        {
            var direction = ctx.ReadValue<Vector2>().ToHorizontalPlane();
            direction *= _controller.AirborneSpeedMultiplier;
            _controller.Character.SetDirection(direction);
        }

        public void HandleJump(InputAction.CallbackContext ctx)
        {
            _controller.RunJumpCoroutine();
        }

        public void OnCollisionEnter(Collision collision)
        {
            foreach (var contact in collision.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) < 5)
                {
                    _controller.NotifyLanding();
                    return;
                }
            }
        }
        public void Update() {}
    }
}
