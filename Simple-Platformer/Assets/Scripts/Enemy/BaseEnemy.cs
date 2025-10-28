using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class BaseEnemy : PoolableObject, IDamagable
{
    protected EntityHealthSystem healthSystem;

    public event Action<BaseEnemy> Dead;

    private EntityDamageSystem damageSystem;
    private CancellationTokenSource cts;

    public virtual void Initialize(Vector3 spawnPoint, EnemyConfig config)
    {
        transform.localScale = Vector3.one;
        transform.position = spawnPoint;

        damageSystem = new(config.Damage);

        healthSystem = new(config.Health);
        healthSystem.Dead += OnDead;
    }

    public virtual void Deinitialize()
    {
        healthSystem.Dead -= OnDead;

        cts?.Cancel();
        cts?.Dispose();
        cts = null;
    }

    public virtual void CauseDamage(Player player)
    {
        damageSystem.CauseDamageTo(player);
    }

    public void TakeDamage(int damage)
    {
        healthSystem.TryTakeDamage(damage);
    }

    protected async virtual UniTask AnimationDeadAsync(CancellationToken ct)
    {
        Dead?.Invoke(this);
    }

    private void OnDead()
    {
        cts = new();

        AnimationDeadAsync(cts.Token).Forget();
    }
}