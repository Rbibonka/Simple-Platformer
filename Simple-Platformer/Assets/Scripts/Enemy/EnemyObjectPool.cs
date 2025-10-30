using UnityEngine;

public class EnemyObjectPool : BaseObjectPool<BaseEnemy>
{
    public EnemyObjectPool(BaseEnemy prefab, Transform objectsParent = null) : base(prefab, objectsParent) { }
}