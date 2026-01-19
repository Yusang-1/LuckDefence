using UnityEngine;

public class Character : Entity, IAttackable, ISkillusable
{
    //[SerializeField] private CharacterSO data;
    private int currentMP;

    //public CharacterSO Data => data;

    public void Attack(IDamagable target)
    {
        
    }

    public void GetMP(int amount)
    {
        
    }

    public void UseSkill()
    {
        
    }
}
