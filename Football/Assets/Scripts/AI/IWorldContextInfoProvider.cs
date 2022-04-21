using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorldContextInfoProvider
{
	IAgent[] getObstaclesForSector(Vector3 agentPosition);
	IAgent getPlayerAgent();
	IAgent[] getNonPlayingAgents();
}

