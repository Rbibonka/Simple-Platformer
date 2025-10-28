using UnityEngine;

public sealed class PatrollingEnemyMover
{
    private float speed = 1f;
    private float patrolDistance = 5f;
    private float obstacleDetectionDistance = 1f;
    private float groundCheckDistance = 1f;
    private LayerMask obstacleLayer;
    private LayerMask groundLayer;

    private Transform transform;

    private Vector2 startingPoint;

    public bool movingRight = true;

    private EntityFlipper flipper;

    public PatrollingEnemyMover(Transform transform, LayerMask obstacleLayer, LayerMask groundLayer)
    {
        this.transform = transform;

        startingPoint = this.transform.position;

        this.obstacleLayer = obstacleLayer;
        this.groundLayer = groundLayer;

        flipper = new(transform);
        flipper.SimpleFlip(movingRight);
    }

    public void ChangeDirecion()
    {
        movingRight = !movingRight;
        flipper.SimpleFlip(movingRight);
    }

    public void Move()
    {
        bool obstacleAhead = Physics2D.Raycast(transform.position, movingRight ? Vector2.right : Vector2.left, obstacleDetectionDistance, obstacleLayer);

        Vector2 groundCheckOrigin = new Vector2(transform.position.x + (movingRight ? 0.5f : -0.5f), transform.position.y);
        bool groundAhead = Physics2D.Raycast(groundCheckOrigin, Vector2.down, groundCheckDistance, groundLayer);

        if (obstacleAhead || !groundAhead)
        {
            movingRight = !movingRight;
            flipper.SimpleFlip(movingRight);
        }
        else
        {
            float targetX = movingRight ? startingPoint.x + patrolDistance : startingPoint.x - patrolDistance;
            transform.position = new Vector2(
                Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime),
                transform.position.y
            );

            if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
            {
                movingRight = !movingRight;
                flipper.SimpleFlip(movingRight);
            }
        }
    }
}