using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationEventListenter : MonoBehaviour
{
    public event Action DamageEnd;
    public event Action DieEnd;

    public void OnDamageAnimationEnd()
    {
        DamageEnd?.Invoke();
    }

    public void OnDieAnimatioEnd()
    {
        DieEnd?.Invoke();
    }
}