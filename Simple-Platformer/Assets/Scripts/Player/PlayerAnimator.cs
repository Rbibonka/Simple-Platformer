using UnityEngine;

public class PlayerAnimator
{
    private Animator animator;

    public PlayerAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public void SetMoveState(float speed)
    {
        if (speed == 0)
        {
            animator.SetBool("IsMove", false);
        }
        else
        {
            animator.SetBool("IsMove", true);
        }
    }

    public void EndJump()
    {
        animator.SetBool("IsJump", false);
    }

    public void Jump()
    {
        animator.SetBool("IsJump", true);
    }
}