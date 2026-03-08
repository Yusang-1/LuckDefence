using UnityEngine;
using System;

public class Character : Entity, IAttackable, ISkillusable
{
    public event Action<int> MPChanged;

    private int m_CurrentMP;
    private bool isAttackable;
    private bool isManaFull;

    public IDamagable AttackTarget;

    public Platform platform;
    public CharacterStateMachine stateMachine;
    public TargetSearcher TargetSearcher;
    public Attacker Attacker;

    public bool IsAttackable
    {
        get => isAttackable;
        set
        {
            isAttackable = value;
        }
    }

    public bool IsManaFull => isManaFull;

    public int CurrentMP
    {
        get => m_CurrentMP;
        set
        {
            m_CurrentMP = Mathf.Clamp(value, 0, Data.MaxMp);
            MPChanged?.Invoke(m_CurrentMP);

            if (m_CurrentMP >= Data.MaxMp)
            {
                isManaFull = true;
            }
        }
    }

    private void Update()
    {
        stateMachine?.StateUpdate();
    }

    public override void EntityActivated()
    {
        base.EntityActivated();

        if (stateMachine == null)
        {
            stateMachine = new CharacterStateMachine(this);
        }

        stateMachine.Initialize(stateMachine.IdleState);
    }

    public void GetPlatform(Platform platform)
    {
        this.platform = platform;
    }    

    public void Attack(IDamagable target)
    {
        //Debug.Log("Attack!");
        target.TakeDamage(Data.AttackPoint);

        GetMP(Data.GetMPPoint);
    }

    public void GetMP(int amount)
    {
        CurrentMP += amount;
    }

    public void UseSkill(IDamagable target)
    {
        CurrentMP = 0;

        isManaFull = false;

        Debug.Log($"Use Skill : {gameObject.name}");
    }
}
