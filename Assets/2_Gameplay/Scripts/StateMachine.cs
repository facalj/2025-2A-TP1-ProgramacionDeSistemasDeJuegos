using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class StateMachine
    {
        private IPlayerState _currentState;
        private PlayerController _controller;

        public StateMachine(PlayerController controller)
        {
            _controller = controller;
        }

        public void Initialize(IPlayerState initialState)
        {
            _currentState = initialState;
            _currentState.Enter(_controller);
        }

        public void TransitionTo(IPlayerState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter(_controller);
        }

        public void Update()
        {
            _currentState?.Update();

            if (_controller.JumpRequested)
            {
                Debug.Log("JumpRequested == true");

                if (_currentState is GroundedState)
                {
                    Debug.Log("to JumpingState from Grounded");
                    TransitionTo(new JumpingState());
                }
                else if (_currentState is FallState)
                {
                    Debug.Log("to DoubleJumpState from Fall");
                    TransitionTo(new DoubleJumpState());
                }
                _controller.ConsumeJump();
            }

            if (_controller.Character.GetVelocity().y < -0.001f && !(_currentState is FallState) && !(_currentState is DoubleJumpState))
            {
                TransitionTo(new FallState());
            }
        }

        public void HandleMove(InputAction.CallbackContext ctx)
            => _currentState?.HandleMove(ctx);

        public void HandleJump(InputAction.CallbackContext ctx)
            => _controller.RequestJump();

        public void OnCollisionEnter(Collision collision)
            => _currentState?.OnCollisionEnter(collision);

        public void OnLanding()
        {
            //the state machine will choose which state will continue after landing
            if (_currentState is JumpingState || _currentState is FallState || _currentState is DoubleJumpState)
            {
                TransitionTo(new GroundedState());
            }
        }
    }
}
