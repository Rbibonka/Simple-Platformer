using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour
{
    [SerializeField]
    private Image img_Background;

    private float fadeTime = 1f;

    public void FadeInImmediately()
    {
        var color = img_Background.color;
        color.a = 1;
        img_Background.color = color;
    }

    public void FadeOutImmediately()
    {
        var color = img_Background.color;
        color.a = 0;
        img_Background.color = color;
    }

    public async UniTask FadeIn(CancellationToken ct)
    {
        await img_Background.DOFade(1, fadeTime);
    }

    public async UniTask FadeOut(CancellationToken ct)
    {
        await img_Background.DOFade(0, fadeTime);
    }
}