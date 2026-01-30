using System;

public interface ISkillusable
{
    public event Action<int> MPChanged;
    public void GetMP(int amount);
    public void UseSkill(IDamagable target);
}
