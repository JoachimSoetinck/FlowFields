using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlowField
{

	public readonly Vector2Int Vector;


	private static readonly Vector2Int None = new Vector2Int(0, 0);
	private static readonly Vector2Int North = new Vector2Int(0, 1);
	private static readonly Vector2Int NorthEast = new Vector2Int(1, 1);
	private static readonly Vector2Int East = new Vector2Int(1, 0);
	private static readonly Vector2Int SouthEast = new Vector2Int(1, -1);
	private static readonly Vector2Int South = new Vector2Int(0, -1);
	private static readonly Vector2Int SouthWest = new Vector2Int(-1, -1);
	private static readonly Vector2Int West = new Vector2Int(-1, 0);
	private static readonly Vector2Int NorthWest = new Vector2Int(-1, 1);


	public static readonly List<Vector2Int> AllDirections = new List<Vector2Int>
	{
		None,
		North,
		NorthEast,
		East,
		SouthEast,
		South,
		SouthWest,
		West,
		NorthWest
	};

	public static Vector2Int GetDirection(Vector2Int value)
	{

		foreach (Vector2Int direction in AllDirections)
        {
			if (direction == value)
				return direction;
		
        }
			return AllDirections[0];
	}
	public Cell[,] grid { get; private set; }
	public Vector2Int gridSize { get; private set; }
	public float cellRadius { get; private set; }

	private Cell _destinationCell = null;

	private float cellDiameter;

	public FlowField(float _cellRadius, Vector2Int _gridSize)
	{
		cellRadius = _cellRadius;
		cellDiameter = cellRadius * 2f;
		gridSize = _gridSize;
	}

	public void CreateGrid()
	{
		grid = new Cell[gridSize.x, gridSize.y];

		for (int x = 0; x < gridSize.x; x++)
		{
			for (int y = 0; y < gridSize.y; y++)
			{
				Vector3 worldPos = new Vector3(cellDiameter * x + cellRadius, 0, cellDiameter * y + cellRadius);
				grid[x, y] = new Cell(worldPos, new Vector2Int(x, y));
			}
		}
	}

	public void CreateCostField()
    {

		Vector3 cellHalfExtents = Vector3.one * cellRadius;
		int terrainMask = LayerMask.GetMask("Wall", "Grass");
		foreach (Cell curCell in grid)
		{
			Collider[] obstacles = Physics.OverlapBox(curCell.worldPos, cellHalfExtents, Quaternion.identity, terrainMask);

			foreach (Collider col in obstacles)
			{
				
				if (col.gameObject.layer == 7)
				{
					curCell.IncreaseCost(255);
					continue;
				}
				else if ( col.gameObject.layer == 8)
				{
					curCell.IncreaseCost(3);
				}
			}
		}

	}

	public void GenerateIntegrationField(Cell destinationCell)
	{
		_destinationCell = destinationCell;

		destinationCell.cost = 0;
		destinationCell.BestCost = 0;

		Queue<Cell> cellsToCheck = new Queue<Cell>();

		cellsToCheck.Enqueue(destinationCell);

		while (cellsToCheck.Count > 0)
		{
			Cell curCell = cellsToCheck.Dequeue();
			List<Cell> curNeighbors = GetNeighborCells(curCell.gridIndex);
			foreach (Cell curNeighbor in curNeighbors)
			{
				if (curNeighbor.cost == byte.MaxValue) { continue; }
				if (curNeighbor.cost + curCell.BestCost < curNeighbor.BestCost)
				{
					curNeighbor.BestCost = (byte)(curNeighbor.cost + curCell.BestCost);
					cellsToCheck.Enqueue(curNeighbor);
				}
			}
		}

		//Source: https://leifnode.com/2013/12/flow-field-pathfinding/

	}

	public void CreateFlowField()
	{
		//checking each cell in the grid
		foreach (Cell cell in grid)
		{
			//only need to check the neigbors of the cell not of the whole grid to determine direction
			List<Cell> Neigbors = GetNeighborCells(cell.gridIndex);

			int bestCost = cell.BestCost;

			foreach (Cell curNeighbor in Neigbors)
			{
				if (curNeighbor.BestCost < bestCost)
				{
					bestCost = curNeighbor.BestCost;
					cell.BestDirection = GetDirection(curNeighbor.gridIndex - cell.gridIndex);
				}
				
			}
		}
	}


	private List<Cell> GetNeighborCells(Vector2Int nodeIndex)
	{
		List<Cell> neighbors = new List<Cell>();

		foreach (Vector2Int movementDirection in AllDirections)
		{
			Cell newNeighbor = GetCellAtLocation(nodeIndex, movementDirection);
			if (newNeighbor != null)
			{
				neighbors.Add(newNeighbor);
			}
		}
		return neighbors;
	}

	private Cell GetCellAtLocation(Vector2Int orignPos, Vector2Int newPos)
	{
		//adding movement Vector to pos
		Vector2Int finalPos = orignPos + newPos;

		if (finalPos.x < 0 || finalPos.x >= gridSize.x || finalPos.y < 0 || finalPos.y >= gridSize.y)
		{
			return null;
		}

		else { return grid[finalPos.x, finalPos.y]; }
	}


	public Cell GetCellFromWorldPos(Vector3 worldPos)
	{
		float percentX = worldPos.x / (gridSize.x * cellDiameter);
		float percentY = worldPos.z / (gridSize.y * cellDiameter);

		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.Clamp(Mathf.FloorToInt((gridSize.x) * percentX), 0, gridSize.x - 1);
		int y = Mathf.Clamp(Mathf.FloorToInt((gridSize.y) * percentY), 0, gridSize.y - 1);
		return grid[x, y];
	}
}
