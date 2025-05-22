using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveInput;
        [SerializeField] private InputActionReference jumpInput;
        [SerializeField] private float airborneSpeedMultiplier = .5f;
        //TODO: This booleans are not flexible enough. If we want to have a third jump or other things, it will become a hazzle.
        //private bool _isJumping;
        //private bool _isDoubleJumping;
        private Character _character;
        private Coroutine _jumpCoroutine;
        private StateMachine _stateMachine;
        private bool _jumpRequested;
        public Character Character => _character;
        public float AirborneSpeedMultiplier => airborneSpeedMultiplier;
        public bool JumpRequested { get; private set; }
        public void RequestJump() => JumpRequested = true;
        public void ConsumeJump() => JumpRequested = false;

        private void Awake()
        {
            _character = GetComponent<Character>();
            _stateMachine = new StateMachine(this);
            _stateMachine.Initialize(new GroundedState());
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void OnEnable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.started += HandleMoveInput;
                moveInput.action.performed += HandleMoveInput;
                moveInput.action.canceled += HandleMoveInput;
            }
            if (jumpInput?.action != null)
                jumpInput.action.performed += HandleJumpInput;
        }

        private void OnDisable()
        {
            if (moveInput?.action != null)
            {
                //added started -= HandleMoveInput i dont know if its neccessary
                moveInput.action.started -= HandleMoveInput;
                moveInput.action.performed -= HandleMoveInput;
                moveInput.action.canceled -= HandleMoveInput;
            }
            if (jumpInput?.action != null)
                jumpInput.action.performed -= HandleJumpInput;
        }

        private void HandleMoveInput(InputAction.CallbackContext ctx)
            => _stateMachine.HandleMove(ctx);

        private void HandleJumpInput(InputAction.CallbackContext ctx)
            => _stateMachine.HandleJump(ctx);

        public void RunJumpCoroutine()
        {
            if (_jumpCoroutine != null)
                StopCoroutine(_jumpCoroutine);
            _jumpCoroutine = StartCoroutine(_character.Jump());
        }

        private void OnCollisionEnter(Collision collision)
            => _stateMachine.OnCollisionEnter(collision);

        public void RequestGrounded()
            => _stateMachine.TransitionTo(new GroundedState());

        public void NotifyLanding()
        {
            _stateMachine.OnLanding();
        }
    }
}