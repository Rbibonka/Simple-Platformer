using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyCollisionChecker : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    public event Action<Player> PlayerDetected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask.value) != 0)
        {
            if (collision.gameObject.TryGetComponent<Player>(out Player player))
            {
                PlayerDetected?.Invoke(player);
            }
        }
    }
}