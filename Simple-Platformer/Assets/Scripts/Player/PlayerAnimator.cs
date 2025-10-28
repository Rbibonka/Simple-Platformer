using System;
using UnityEngine;

public class PlayerAnimator : IDisposable
{
    private Animator animator;
    private PlayerAnimationEventListenter playerAnimationEventListenter;
    
    public event Action DamageAnimationEnd;
    public event Action DieAnimationEnd;

    private const string AnimatorMoveState = "IsMove";
    private const string AnimatorJumpState = "IsJump";
    private const string AnimatorFallState = "IsFall";
    private const string AnimatorDamageState = "Damaged";
    private const string AnimatorDeadState = "Dead";

    public PlayerAnimator(Animator animator, PlayerAnimationEventListenter playerAnimationEventListenter)
    {
        this.animator = animator;
        this.playerAnimationEventListenter = playerAnimationEventListenter;

        this.playerAnimationEventListenter.DamageEnd += OnDamageClipEnd;
        this.playerAnimationEventListenter.DieEnd += OnDieClipEnd;
    }

    public void SetMoveState(float speed)
    {
        if (speed == 0)
        {
            animator.SetBool(AnimatorMoveState, false);
        }
        else
        {
            animator.SetBool(AnimatorMoveState, true);
        }
    }

    public void Die()
    {
        ResetAnimator();
        animator.SetTrigger(AnimatorDeadState);
    }

    public void EndJump()
    {
        animator.SetBool(AnimatorJumpState, false);
    }

    public void Jump()
    {
        animator.SetBool(AnimatorJumpState, true);
    }

    public void Damaged()
    {
        ResetAnimator();
        animator.SetTrigger(AnimatorDamageState);
    }

    private void OnDamageClipEnd()
    {
        DamageAnimationEnd?.Invoke();
    }

    private void OnDieClipEnd()
    {
        DieAnimationEnd?.Invoke();
    }

    private void ResetAnimator()
    {
        animator.SetBool(AnimatorJumpState, false);
        animator.SetBool(AnimatorMoveState, false);
        animator.SetBool(AnimatorFallState, false);
        animator.ResetTrigger(AnimatorDamageState);
        animator.ResetTrigger(AnimatorDeadState);
    }

    public void Dispose()
    {
        playerAnimationEventListenter.DieEnd -= OnDieClipEnd;
        playerAnimationEventListenter.DieEnd -= OnDamageClipEnd;
    }
}