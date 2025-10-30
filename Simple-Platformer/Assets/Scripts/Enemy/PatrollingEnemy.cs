using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public sealed class PatrollingEnemy : BaseEnemy
{
    [SerializeField]
    private EnemyCollisionChecker checker;

    [SerializeField]
    private LayerMask obstacleLayer;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private EnemyDamagablePart damagablePart;

    private PatrollingEnemyMover enemyMover;
    private PatrollingEnemyAnimator enemyAnimator;

    private bool isMove;

    public override void Initialize(Vector3 spawnPoint, EnemyConfig config)
    {
        base.Initialize(spawnPoint, config);
        enemyAnimator = new(animator, transform);

        boxCollider.enabled = true;

        checker.PlayerDetected += OnPlayerDetected;

        damagablePart.Initialize(this);
        damagablePart.gameObject.SetActive(true);

        enemyMover = new(transform, obstacleLayer, groundLayer);
        isMove = true;
    }

    public override void Deinitialize()
    {
        base.Deinitialize();

        checker.PlayerDetected -= OnPlayerDetected;
    }

    public void ChangeDirection()
    {
        enemyMover.ChangeDirecion();
    }

    private void FixedUpdate()
    {
        if (!isMove)
        {
            return;
        }

        enemyMover.Move();
    }

    protected override async UniTask AnimationDeadAsync(CancellationToken ct)
    {
        isMove = false;

        boxCollider.enabled = false;
        damagablePart.gameObject.SetActive(false);

        await enemyAnimator.DieAsync(ct);
        await base.AnimationDeadAsync(ct);
    }

    private void OnPlayerDetected(Player player)
    {
        CauseDamage(player);
    }
    
    public override void CauseDamage(Player player)
    {
        if (player.transform.position.y > damagablePart.transform.position.y)
        {
            return;
        }

        base.CauseDamage(player);
        ChangeDirection();
    }
}