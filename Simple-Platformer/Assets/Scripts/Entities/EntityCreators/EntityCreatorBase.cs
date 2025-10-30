using System.Collections.Generic;

public class EntityCreatorBase<T> where T : PoolableObject
{
    private BaseObjectPool<T> entitesObjectPool;

    private T entity;

    private List<T> entites;

    public EntityCreatorBase(T entity)
    {
        this.entity = entity;

        entitesObjectPool = new(this.entity);
    }

    public List<T> CreateEntities(int count)
    {
        entites = new List<T>(count);

        for (int i = 0; i < count; i++)
        {
            var entity = entitesObjectPool.GetFromPool();
            entites.Add(entity);
        }

        return entites;
    }

    public void DeleteEnemies()
    {
        foreach (var entity in entites)
        {
            entitesObjectPool.SetToPool(entity);
        }

        entites.Clear();
    }

    public void DeleteEntity(T entity)
    {
        entitesObjectPool.SetToPool(entity);
        entites.Remove(entity);
    }
}