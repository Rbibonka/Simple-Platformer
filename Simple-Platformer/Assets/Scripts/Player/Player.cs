using UnityEngine;

public sealed class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody2d;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GroundChecker groundChecker;

    private PlayerAnimator playerAnimator;
    private PlayerInputListenter playerInputListenter;
    private PlayerMover playerMover;
    private ObjectFlipper flipper;

    private void FixedUpdate()
    {
        playerMover.Move();
    }

    public void Initialize()
    {
        playerInputListenter = new();
        playerMover = new(rigidbody2d);
        playerAnimator = new(animator);
        flipper = new(transform);

        playerInputListenter.InputMoved += OnInputMoved;
        playerInputListenter.InputJumped += OnInputJumped;
    }

    private void OnInputJumped()
    {
        if (!groundChecker.IsGround)
        {
            return;
        }

        groundChecker.StartCheck();
        groundChecker.Grounded += OnJumpEnded;

        playerMover.Jump();
        playerAnimator.Jump();
    }

    private void OnJumpEnded()
    {
        groundChecker.Grounded -= OnJumpEnded;
        groundChecker.StopCheck();

        playerAnimator.EndJump();
    }

    private void OnInputMoved(float inputX)
    {
        flipper.Flip(inputX);

        playerAnimator.SetMoveState(inputX);
        playerMover.SetInputSpeed(inputX);
    }
}