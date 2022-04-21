using UnityEngine;

public class Idle : SteeringBehavior
{
    public Idle(SteeringAgent owner)
    : base(owner)
    {
    }

    public override Vector3 step()
    {
        return Vector3.zero;
    }
}
