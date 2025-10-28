using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenuUI : BaseUI
{
    [SerializeField]
    private Button btn_Start;

    [SerializeField]
    private ButtonEventSender buttonEventSender;

    public event Action ButtonStartClicked;

    public void Initialize()
    {
        buttonEventSender.StartEventListen();
        buttonEventSender.ButtonClicked += OnButtonStartClicked;
    }

    public void Deinitialize()
    {
        buttonEventSender.StopEventListen();
        buttonEventSender.ButtonClicked -= OnButtonStartClicked;
    }

    public void DisableButtonsUI()
    {
        btn_Start.interactable = false;
    }

    public void EnableButtonsUI()
    {
        btn_Start.interactable = true;
    }

    private void OnButtonStartClicked()
    {
        ButtonStartClicked?.Invoke();
    }
}