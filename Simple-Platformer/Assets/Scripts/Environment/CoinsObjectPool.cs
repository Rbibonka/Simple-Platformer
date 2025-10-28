using UnityEngine;

public class CoinsObjectPool : BaseObjectPool<Coin>
{
    public CoinsObjectPool(Coin prefab, Transform objectsParent = null) : base(prefab, objectsParent) { }
}