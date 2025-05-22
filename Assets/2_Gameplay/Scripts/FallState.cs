using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class FallState : IPlayerState
    {
        private PlayerController _controller;

        public void Enter(PlayerController controller)
        {
            _controller = controller;
            Debug.Log("Fall state!");
        }

        public void Exit() {}

        public void HandleMove(InputAction.CallbackContext ctx)
        {
            var dir = ctx.ReadValue<Vector2>().ToHorizontalPlane();
            dir *= _controller.AirborneSpeedMultiplier;
            _controller.Character.SetDirection(dir);
        }

        public void HandleJump(InputAction.CallbackContext ctx) {}

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
