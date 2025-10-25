using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BaseUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    private const float MaxCanvasAlpha = 1.0f;
    private const float MinCanvasAlpha = 0.0f;

    public void Show()
    {
        canvasGroup.interactable = true;
        canvasGroup.alpha = MaxCanvasAlpha;
    }

    public void Hide()
    {
        canvasGroup.interactable = false;
        canvasGroup.alpha = MinCanvasAlpha;
    }
}