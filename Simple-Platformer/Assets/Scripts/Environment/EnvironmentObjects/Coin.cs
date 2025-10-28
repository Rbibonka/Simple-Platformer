using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : PoolableObject
{
    public event Action<Coin> CoinTaked;

    public void TakeCoin()
    {
        CoinTaked?.Invoke(this);
    }
}