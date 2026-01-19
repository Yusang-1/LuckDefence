using UnityEngine;

public class EnemyMover : EntityMover
{
    private int currentBeaconIndex;

    private void Update()
    {
        Move();
    }

    public override void Initialize(Entity entity)
    {
        base.Initialize(entity);

        directionVector = GetDestinationVector();
    }

    protected override Vector3 GetDestinationVector()
    {
        transform.position = BeaconContainer.s_Beacons[currentBeaconIndex].position;

        currentBeaconIndex++;
        if (currentBeaconIndex >= BeaconContainer.s_Beacons.Length)
        {
            currentBeaconIndex = 0;
        }

        return (BeaconContainer.s_Beacons[currentBeaconIndex].position - transform.position).normalized;
    }

    protected override void Move()
    {
        transform.position += directionVector * entity.Data.MoveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, BeaconContainer.s_Beacons[currentBeaconIndex].position) <= Vector3.Distance(transform.position, transform.position + directionVector * entity.Data.MoveSpeed * Time.deltaTime))
        {
            directionVector = GetDestinationVector();
        }
    }
}
