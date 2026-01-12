using UnityEngine;
using System.Collections.Generic;

public class FactoryCharRare : FactoryChar
{
    //public override void Initialize(Dictionary<int, Entity> entityDict)
    //{
    //    base.Initialize(entityDict);
    //}

    //public override void ActiveEntity(int code, Platform platform)
    //{
    //    base.ActiveEntity(code, platform);
    //}   

    //public override void DeactiveEntity(int index)
    //{
    //    base.DeactiveEntity(index);
    //}




    public override void Initialize(Dictionary<int, Entity> entityDict)
    {        
        base.Initialize(entityDict);        
    }

    public override void ActiveEntity()
    {
        base.ActiveEntity();        
    }

    public override void DeactiveEntity(int code)
    {
        base.DeactiveEntity(code);
    }

    public override int DetermineEntityCode()
    {
        return base.DetermineEntityCode();        
    }

    public override Platform SearchPlatform(int code)
    {
        return base.SearchPlatform(code);
    }
}
