using UnityEngine;

public class PlayerMover
{
    private float moveSpeedModifier = 5f;
    private Rigidbody2D rigidbody;

    private float inputSpeed;

    public PlayerMover(Rigidbody2D rigidbody)
    {
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
        rigidbody.AddForce(Vector2.up * 70, ForceMode2D.Impulse);
    }
}