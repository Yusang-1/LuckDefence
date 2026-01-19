using UnityEngine;

public class Enemy : Entity, ISkillusable
{
    //[SerializeField] private EnemySO data;
    private int currentHP;
    private int currentMP;

    public override void EntityActivated()
    {
        base.EntityActivated();

        currentHP = Data.MaxHp;
        Debug.Log("enemy activated");
    }

    public void GetMP(int amount)
    {
        
    }

    public void UseSkill()
    {
        
    }
}
