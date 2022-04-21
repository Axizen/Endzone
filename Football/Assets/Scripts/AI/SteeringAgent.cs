public class SteeringAgent : Agent
{
	public SteeringBehaviorId initialBehavior;
	public bool avoidObstacles = true;
	public IAgent target { get; set; }
	private SteeringMovementModule steeringMovementModule;

	protected override void setupMovementModule()
	{
		steeringMovementModule = new SteeringMovementModule(this);
		movementModule = steeringMovementModule;

		if (initialBehavior != SteeringBehaviorId.None)
		{
			steeringMovementModule.setBehavior(initialBehavior);
		}
	}

	public void setBehaviour(SteeringBehaviorId behavior)
	{
		steeringMovementModule.setBehavior(behavior);
	}

}
