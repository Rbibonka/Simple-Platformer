using System;
using UnityEngine;

public class PlayerMoverController : IDisposable
{
    private PlayerInputListenter inputListenter;
    private FeetChecker feetChecker;

    private EntityFlipper flipper;

    private PlayerAnimator animator;

    private PlayerMover mover;

    private bool isMoveLocked = false;
    private bool isJumpAvailable = true;

    private Rigidbody2D rigidbody;

    public PlayerMoverController(Rigidbody2D rigidbody, FeetChecker feetChecker, PlayerAnimator animator, PlayerConfig config)
    {
        this.rigidbody = rigidbody;
        this.feetChecker = feetChecker;
        this.animator = animator;

        flipper = new(rigidbody.transform);

        mover = new(rigidbody, config.PlayerSpeed, config.PlayerSmallJumpForce, config.PlayerJumpForce);
        inputListenter = new();

        inputListenter.InputMoved += OnInputMoved;
        inputListenter.InputMoveEnded += OnMoveEnded;
        inputListenter.InputJumped += OnInputJumped;

        this.feetChecker.GroundEnter += OnGraundEntered;
        this.feetChecker.GroundExit += OnGraundExited;

        isMoveLocked = false;
        isJumpAvailable = true;
        SetPhysicsMaterial(0f);
    }

    public void Dispose()
    {
        inputListenter.InputMoved -= OnInputMoved;
        inputListenter.InputMoveEnded -= OnMoveEnded;
        inputListenter.InputJumped -= OnInputJumped;

        feetChecker.GroundEnter -= OnGraundEntered;
    }

    public void FixedUpdate()
    {
        if (isMoveLocked)
        {
            return;
        }

        mover.Move();
        animator.SetMoveState(mover.VelocityX);
        flipper.PhysicsFlip(mover.VelocityX);
    }

    public void StopMoveControl()
    {
        SetPhysicsMaterial(1f);
        inputListenter.StopListen();

        isJumpAvailable = false;
        isMoveLocked = true;
    }

    public void TakeDamage()
    {
        inputListenter.StopListen();

        isJumpAvailable = false;
        isMoveLocked = true;
        mover.JumpBack();

        SetPhysicsMaterial(1f);

        animator.Damaged();
        animator.DamageAnimationEnd += OnDamageAnimationEnd;
    }

    public void CauseDamage()
    {
        mover.SmallJump();
    }

    private void OnDamageAnimationEnd()
    {
        SetPhysicsMaterial(0f);

        inputListenter.StartListen();
        animator.DamageAnimationEnd -= OnDamageAnimationEnd;
    }

    private void OnInputMoved(float axis)
    {
        isMoveLocked = false;
        mover.SetInputSpeed(axis);
    }

    private void OnMoveEnded()
    {
        isMoveLocked = true;
        mover.SetInputSpeed(0);
    }

    private void OnInputJumped()
    {
        if (!isJumpAvailable)
        {
            return;
        }

        isJumpAvailable = false;

        mover.Jump();
        animator.Jump();
    }

    private void OnGraundExited()
    {
        isJumpAvailable = false;
    }

    private void OnGraundEntered()
    {
        animator.EndJump();
        isJumpAvailable = true;
    }

    private void SetPhysicsMaterial(float value)
    {
        var material = rigidbody.sharedMaterial;
        material.friction = value;
        rigidbody.sharedMaterial = material;
    }
}