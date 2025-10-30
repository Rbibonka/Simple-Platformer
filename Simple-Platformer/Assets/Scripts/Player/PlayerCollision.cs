using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    public event Action Trapped;
    public event Action<Coin> CoinTook;
    public event Action<IGetterDamagable> DamageCaused;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask.value) != 0)
        {
            if (collision.gameObject.TryGetComponent<Trap>(out Trap trap))
            {
                Trapped?.Invoke();
            }
            else if (collision.gameObject.TryGetComponent<IGetterDamagable>(out IGetterDamagable damagableGetter))
            {
                DamageCaused?.Invoke(damagableGetter);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask.value) != 0)
        {
            if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
            {
                CoinTook?.Invoke(coin);
            }
        }
    }
}