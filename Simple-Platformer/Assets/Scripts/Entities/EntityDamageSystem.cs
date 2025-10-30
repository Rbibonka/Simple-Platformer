public sealed class EntityDamageSystem
{
    private int damage;

    public EntityDamageSystem(int damage)
    {
        this.damage = damage;
    }

    public void CauseDamageTo(IDamagable damagable)
    {
        damagable.TakeDamage(damage);
    }
}