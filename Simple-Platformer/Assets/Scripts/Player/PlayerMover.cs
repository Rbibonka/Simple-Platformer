using UnityEngine;

public sealed class PlayerMover
{
    private float moveSpeedModifier;
    private Rigidbody2D rigidbody;

    private float inputSpeed;

    private float smallJumpModifier;
    private float jumpModifier;

    public float VelocityX => rigidbody.velocity.x;

    public PlayerMover(Rigidbody2D rigidbody, float moveSpeedModifier, float smallJumpModifier, float jumpModifier)
    {
        this.moveSpeedModifier = moveSpeedModifier;
        this.smallJumpModifier = smallJumpModifier;
        this.jumpModifier = jumpModifier;

        this.rigidbody = rigidbody;
    }

    public void SetInputSpeed(float inputSpeed)
    {
        this.inputSpeed = inputSpeed;
    }

    public void Move()
    {
        rigidbody.velocity = new Vector2(inputSpeed * moveSpeedModifier, rigidbody.velocity.y);
    }

    public void Jump()
    {
        AddForce(Vector2.up * jumpModifier);
    }

    public void JumpBack()
    {
        AddForce(-rigidbody.transform.right * moveSpeedModifier + Vector3.up * smallJumpModifier);
    }

    public void SmallJump()
    {
        AddForce(Vector2.up * smallJumpModifier);
    }

    private void AddForce(Vector2 force)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = 0;
        rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}