using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
	public GridController gridController;
	public GameObject agent;

	[SerializeField]
	private int AmountAgents;

	[SerializeField]
	private float moveSpeed;

	private List<GameObject> agents;

	private void Awake()
	{
		agents = new List<GameObject>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			SpawnAgents();
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			DestroyAgents();
		}

	}

	private void FixedUpdate()
	{
		
		foreach (GameObject agent in agents)
		{
			//getting the cell where the agent is standing
			Cell cell = gridController.grid.GetCellFromWorldPos(agent.transform.position);
			
			Vector3 moveDirection = new Vector3(cell.bestDirection.x, 0, cell.bestDirection.y);
			
			Rigidbody unitRB = agent.GetComponent<Rigidbody>();
			unitRB.velocity = moveDirection * moveSpeed;
		}
	}

	private void SpawnAgents()
	{
		//Spawning the Agents in the grid
		Vector2Int gridSize = gridController.gridSize;
		float nodeRadius = gridController.cellRadius;

		//getting the max coordinates a unit can spawn
		Vector2 maxSpawnPos = new Vector2(gridSize.x * nodeRadius * 2 + nodeRadius, gridSize.y * nodeRadius * 2 + nodeRadius);


		int colMask = LayerMask.GetMask("Wall", "Agent");
		Vector3 newPos;
		for (int i = 0; i < AmountAgents; i++)
		{
			GameObject newUnit = Instantiate(agent);
			newUnit.transform.position = transform.position;
			agents.Add(newUnit);
			do
			{
				newPos = new Vector3(Random.Range(0, maxSpawnPos.x), 0, Random.Range(0, maxSpawnPos.y));
				newUnit.transform.position = newPos;
			}
			while (Physics.OverlapSphere(newPos, 0.25f, colMask).Length > 0); //checking collision so that units dont spawn in a wal
		}
	}

	private void DestroyAgents()
	{
		foreach (GameObject agent in agents)
		{
			Destroy(agent);
		}
		agents.Clear();
	}
}