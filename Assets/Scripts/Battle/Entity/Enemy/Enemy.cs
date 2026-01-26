using UnityEngine;
using System;

public class Enemy : Entity, ISkillusable, IDamagable
{
    public event Action<int> HPChanged;
    //[SerializeField] private EnemySO data;
    private int currentHP;
    private int currentMP;

    public int CurrentHP
    {
        get => currentHP;
        set
        {
            currentHP = Mathf.Clamp(value, 0, Data.MaxHp);
            HPChanged?.Invoke(currentHP);
        }
    }

    public override void EntityActivated()
    {
        base.EntityActivated();

        currentHP = Data.MaxHp;
    }

    public void TakeDamage(int damage)
    {
        
    }

    public void Die()
    {
        
    }

    public void GetMP(int amount)
    {
        
    }    

    public void UseSkill()
    {
        
    }
}
