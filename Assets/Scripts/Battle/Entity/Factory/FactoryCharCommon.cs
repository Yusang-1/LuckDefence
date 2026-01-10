using UnityEngine;

public class FactoryCharCommon : FactoryChar
{
    public override void Initialize(Entity[] entities)
    {
        base.Initialize(entities);
    }

    public override void ActiveEntity(Entity entity, Platform platform)
    {
        base.ActiveEntity(entity, platform);
    }

    public override void DeactiveEntity(int index)
    {
        base.DeactiveEntity(index);
    }
}
