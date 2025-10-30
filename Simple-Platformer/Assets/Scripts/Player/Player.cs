using System;
using Unity.VisualScripting;
using UnityEngine;

public sealed class Player : MonoBehaviour, IDamagable
{
    [SerializeField]
    private Rigidbody2D rigidbody2d;

    [SerializeField]
    private FeetChecker feetChecker;

    [SerializeField]
    private PlayerCollision collisionChecker;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private PlayerAnimationEventListenter playerAnimationEventListenter;

    private PlayerMoverController moverController;
    private PlayerAnimator playerAnimator;
    private EntityDamageSystem damageSystem;
    private EntityHealthSystem healthSystem;
    private PlayerWallet wallet;

    private bool isInitialize;

    public event Action<int> HealthChanged;
    public event Action<int> CoinsChanged;

    public bool IsDead { get; private set; }

    public int PlayerHealth => healthSystem.Health;

    public int PlayerCoins => wallet.CoinsQuantity;

    public Vector3 FeetPosition => feetChecker.transform.position;

    private string coinsKey = "coins";

    private void FixedUpdate()
    {
        if (!isInitialize)
        {
            return;
        }

        moverController.FixedUpdate();
    }

    public void Initialize(PlayerConfig config)
    {
        wallet = new(PlayerPrefs.GetInt(coinsKey));
        damageSystem = new(config.PlayerDamage);
        healthSystem = new(config.PlayerHealth);
        playerAnimator = new(animator, playerAnimationEventListenter);
        moverController = new(rigidbody2d, feetChecker, playerAnimator, config);

        collisionChecker.Trapped += Trapped;
        collisionChecker.DamageCaused += OnDamageCaused;
        collisionChecker.CoinTook += OnCoinTook;

        healthSystem.Dead += OnDead;
        healthSystem.HealthChanged += OnHealthChanged;

        wallet.CoinsChanged += OnCoinsChanged;
        playerAnimator.DieAnimationEnd += OnDieAnimationEnd;

        isInitialize = true;
        IsDead = false;

        rigidbody2d.velocity = Vector3.zero;
    }

    public void Deinitialize()
    {
        collisionChecker.Trapped -= Trapped;
        collisionChecker.DamageCaused -= OnDamageCaused;
        collisionChecker.CoinTook -= OnCoinTook;

        healthSystem.Dead -= OnDead;
        healthSystem.HealthChanged -= OnHealthChanged;

        wallet.CoinsChanged -= OnCoinsChanged;
        playerAnimator.DieAnimationEnd -= OnDieAnimationEnd;
        isInitialize = false;

        moverController.Dispose();
        playerAnimator.Dispose();
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt(coinsKey, wallet.CoinsQuantity);
    }

    public void ResetCoins()
    {
        PlayerPrefs.SetInt(coinsKey, 0);
    }

    private void Trapped()
    {
        moverController.StopMoveControl();
        healthSystem.Die();
    }

    public void TakeDamage(int damage)
    {
        if (healthSystem.TryTakeDamage(damage))
        {
            if (healthSystem.Health == 0)
            {
                OnDead();
            }
            else
            {
                moverController.TakeDamage();
            }
        }
    }

    private void OnDamageCaused(IGetterDamagable damagableGetter)
    {
        EnemyDamagablePart part = (EnemyDamagablePart)damagableGetter;

        if (FeetPosition.y < part.transform.position.y)
        {
            return;
        }

        moverController.CauseDamage();
        damageSystem.CauseDamageTo(part.GetDamagable());
    }

    private void OnCoinTook(Coin coin)
    {
        coin.TakeCoin();
        wallet.AddCoin();
    }

    private void OnCoinsChanged(int value)
    {
        CoinsChanged?.Invoke(value);
    }

    private void OnHealthChanged(int value)
    {
        HealthChanged?.Invoke(value);
    }

    private void OnDead()
    {
        moverController.StopMoveControl();

        playerAnimator.Die();
        playerAnimator.DieAnimationEnd += OnDieAnimationEnd;
    }

    private void OnDieAnimationEnd()
    {
        playerAnimator.DieAnimationEnd -= OnDieAnimationEnd;
        IsDead = true;
    }
}