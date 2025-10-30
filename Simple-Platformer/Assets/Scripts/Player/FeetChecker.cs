using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FeetChecker : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    public bool IsCheck { get; private set; } = false;

    public event Action GroundEnter;
    public event Action GroundExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask.value) != 0)
        {
            if (collision.gameObject.TryGetComponent<Ground>(out Ground ground))
            {
                GroundEnter?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask.value) != 0)
        {
            if (collision.gameObject.TryGetComponent<Ground>(out Ground ground))
            {
                GroundExit.Invoke();
            }
        }
    }
}