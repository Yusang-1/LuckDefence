using UnityEngine;
using System;

public class Enemy : Entity, ISkillusable, IDamagable
{
    public event Action<int> HPChanged;
    public event Action<int> MPChanged;
    public event Action EnemyDied;
    
    private int m_CurrentHP;
    private int m_CurrentMP;

    private bool isDied;

    public int CurrentHP
    {
        get => m_CurrentHP;
        set
        {
            m_CurrentHP = Mathf.Clamp(value, 0, Data.MaxHp);
            HPChanged?.Invoke(m_CurrentHP);

            if(m_CurrentHP <= 0)
            {
                Die();
                isDied = true;
            }
        }
    }

    public int CurrentMP
    {
        get => m_CurrentMP;
        set
        {
            m_CurrentMP = Mathf.Clamp(value, 0, Data.MaxMp);
            MPChanged?.Invoke(m_CurrentMP);

            if(m_CurrentMP >= Data.MaxMp)
            {
                //UseSkill();
            }
        }
    }

    public override void EntityActivated()
    {
        base.EntityActivated();

        EnemyList.Activated(this);

        CurrentHP = Data.MaxHp;

        isDied = false;
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;

        GetMP(Data.GetMPPoint);
    }

    public void Die()
    {
        if(isDied)
        {
            return;
        }

        EnemyList.Deactivated(this);
        EnemyDied?.Invoke();
        gameObject.SetActive(false);
    }

    public void GetMP(int amount)
    {
        CurrentMP += amount;
    }    

    public void UseSkill(IDamagable target)
    {
        CurrentMP = 0;

        Debug.Log($"Use Skill : {gameObject.name}");
    }
}
