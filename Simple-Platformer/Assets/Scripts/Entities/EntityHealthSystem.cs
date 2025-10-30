using System;

public sealed class EntityHealthSystem
{
    private int health;

    public event Action Dead;
    public event Action<int> HealthChanged;
    public int Health => health;

    public EntityHealthSystem(int health)
    {
        this.health = health;
    }

    public void Die()
    {
        health = 0;
        HealthChanged?.Invoke(health);
        Dead?.Invoke();
    }

    public bool TryTakeDamage(int damage)
    {
        if (damage < 0)
        {
            return false;
        }

        health -= damage;

        if (health <= 0)
        {
            Die();

            return true;
        }

        HealthChanged?.Invoke(health);

        return true;
    }
}