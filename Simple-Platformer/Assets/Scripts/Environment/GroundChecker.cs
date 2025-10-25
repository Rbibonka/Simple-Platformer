using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    public bool IsGround { get; private set; } = true;

    public event Action Grounded;

    public void StartCheck()
    {
       IsGround = false;
    }

    public void StopCheck()
    {
        IsGround = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsGround)
        {
            return;
        }

        if (((1 << collision.gameObject.layer) & layerMask.value) != 0)
        {
            Grounded?.Invoke();
        }
    }
}