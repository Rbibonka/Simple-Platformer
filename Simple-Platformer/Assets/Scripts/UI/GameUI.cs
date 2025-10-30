using TMPro;
using UnityEngine;

public sealed class GameUI : BaseUI
{
    [SerializeField]
    private TMP_Text txt_Health;

    [SerializeField]
    private TMP_Text txt_Coins;

    public void ChangeHealthValue(int value)
    {
        txt_Health.text = value.ToString();
    }

    public void ChangeCoinsValue(int value)
    {
        txt_Coins.text = value.ToString();
    }
}