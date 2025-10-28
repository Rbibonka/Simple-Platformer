using System;

public class PlayerWallet
{
    private int coinsQuantity;

    public event Action<int> CoinsChanged;

    public int CoinsQuantity => coinsQuantity;

    public PlayerWallet(int startCoinsQuantity)
    {
        coinsQuantity = startCoinsQuantity;
    }

    public void AddCoin()
    {
        coinsQuantity++;
        CoinsChanged?.Invoke(coinsQuantity);
    }
}