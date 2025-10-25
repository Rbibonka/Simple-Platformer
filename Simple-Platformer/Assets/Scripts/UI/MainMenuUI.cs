using System;
using UnityEngine;

public sealed class MainMenuUI : BaseUI
{
    [SerializeField]
    private ButtonEventSender btn_Start;

    public event Action ButtonStartClicked;

    public void Initialize()
    {
        btn_Start.StartEventListen();
        btn_Start.ButtonClicked += OnButtonStartClicked;
    }

    public void Deinitialize()
    {
        btn_Start.StopEventListen();
        btn_Start.ButtonClicked -= OnButtonStartClicked;
    }

    private void OnButtonStartClicked()
    {
        ButtonStartClicked?.Invoke();
    }
}