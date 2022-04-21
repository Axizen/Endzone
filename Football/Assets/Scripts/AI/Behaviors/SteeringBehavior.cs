using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehavior 
{
    public SteeringAgent owner { get; protected set; }
    public SteeringBehavior(SteeringAgent owner)
    {
        this.owner = owner;
    }
    public abstract Vector3 step();
}