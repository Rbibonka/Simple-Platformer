using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;

public class PatrollingEnemyAnimator
{
    private Animator animator;
    private Transform transform;

    private const float DeadTimeExitTime = 0.63f;
    private const string AnimationTriggerDead = "Dead";

    public PatrollingEnemyAnimator(Animator animator, Transform transform)
    {
        this.animator = animator;
        this.transform = transform;
    }

    public async UniTask DieAsync(CancellationToken ct)
    {
        animator.SetTrigger(AnimationTriggerDead);

        transform.DOScale(Vector3.zero, DeadTimeExitTime);

        await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate, cancellationToken: ct);

        await UniTask.WaitUntil(() => animator?.GetCurrentAnimatorStateInfo(0).normalizedTime >= DeadTimeExitTime, cancellationToken: ct);
    }
}