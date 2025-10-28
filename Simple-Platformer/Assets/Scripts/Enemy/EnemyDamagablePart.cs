using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyDamagablePart : MonoBehaviour, IGetterDamagable
{
    private IDamagable damagablePart;

    public void Initialize(IDamagable damagablePart)
    {
        this.damagablePart = damagablePart;
    }

    public IDamagable GetDamagable()
    {
        return damagablePart;
    }
}