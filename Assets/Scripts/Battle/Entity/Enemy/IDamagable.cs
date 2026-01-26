using System;

public interface IDamagable
{
    public event Action<int> HPChanged;
    public void TakeDamage(int damage);
    public void Die();
}
