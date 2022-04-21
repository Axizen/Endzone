using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class SteeringMovementModule : MovementModule
{
    private SteeringAgent steeringOwner;
    private Dictionary<SteeringBehaviorId, SteeringBehavior> steeringBehaviors;
    private SteeringBehavior currentBehaviour;
    private SteeringBehavior avoidanceBehaviour;
    private static Dictionary<SteeringBehaviorId, System.Type> behaviorsMappings;

    static SteeringMovementModule()
    {
        behaviorsMappings = new Dictionary<SteeringBehaviorId, Type>();

        behaviorsMappings[SteeringBehaviorId.Idle] = typeof(Idle);
        behaviorsMappings[SteeringBehaviorId.Follow] = typeof(Follow);
        behaviorsMappings[SteeringBehaviorId.Pursue] = typeof(Pursue);
        behaviorsMappings[SteeringBehaviorId.Seek] = typeof(Seek);

    }

    public SteeringMovementModule(IAgent agent)
    : base(agent)
    {
        steeringOwner = agent as SteeringAgent;

        steeringBehaviors = new Dictionary<SteeringBehaviorId, SteeringBehavior>();

        avoidanceBehaviour = steeringBehaviors[SteeringBehaviorId.Avoidance] = new Avoidance(steeringOwner);
    }

    public void setBehaviour<T>() where T : SteeringBehavior
    {
        var type = typeof(T);
        var typeBehaviourId = behaviorsMappings.FirstOrDefault(x => x.Value == type).Key;
        if (steeringBehaviors.ContainsKey(typeBehaviourId))
        {
            currentBehaviour = steeringBehaviors[typeBehaviourId];
        }
        else
        {
            currentBehaviour = (T)Activator.CreateInstance(type, owner);
            steeringBehaviors[typeBehaviourId] = currentBehaviour;
        }
    }

    public void setBehavior(SteeringBehaviorId id)
    {
        if (steeringBehaviors.ContainsKey(id))
        {
            currentBehaviour = steeringBehaviors[id];
        }
        else
        {
            Type type;
            bool found = behaviorsMappings.TryGetValue(id, out type);
            if (found)
            {
                currentBehaviour = (SteeringBehavior)Activator.CreateInstance(type, owner);
                steeringBehaviors[id] = currentBehaviour;
            }
            else
            {
                throw new System.ArgumentException("Parameter behavior id not recognised.", id.ToString());
            }
        }
    }

    public override Vector3 getStep(float dt)
    {
        IAgent target = steeringOwner.target;

        if (target == null || currentBehaviour == null)
        {
            return Vector3.zero;
        }

        Vector3 steering = currentBehaviour.step();

        if (steeringOwner.avoidObstacles)
        {
            steering += avoidanceBehaviour.step();
        }

        steering = Vector3.ClampMagnitude(steering, owner.getMaxForce());

        float mass = Mathf.Max(owner.getMass(), 0.1f);
        Vector3 acceleration = steering / mass;
        velocity += acceleration * dt;
        velocity = Vector3.ClampMagnitude(velocity, owner.getMaxSpeed());

        if (velocity.magnitude >= steeringOwner.minSpeed)
        {
            Vector3 step = velocity * dt;
            return step;
        }
        else
        {
            return Vector3.zero;
        }
    }

}

