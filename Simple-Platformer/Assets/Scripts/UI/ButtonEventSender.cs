using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventSender : MonoBehaviour
{
    [SerializeField]
    private Button btn_Button;

    public event Action ButtonClicked;

    public void StartEventListen()
    {
        btn_Button.onClick.AddListener(OnButtonClick);
    }

    public void StopEventListen()
    {
        btn_Button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke();
    }
}