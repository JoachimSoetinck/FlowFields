using UnityEngine;

public class Cell
{
	public Vector3 worldPos;
	public Vector2Int gridIndex;
	public byte cost;
	private byte _bestCost = byte.MaxValue;
	public Vector2Int bestDirection;

	public byte BestCost
	{
		set { _bestCost = value; }
		get { return _bestCost; }
	}

	public Vector2Int GridIndex
	{
		get { return gridIndex; }
	}
	public Vector2Int BestDirection
	{
		get { return bestDirection; }
		set { bestDirection = value; }
	}

	public byte Cost
	{
		set { cost = value; }
		get { return cost; }
	}

	public Cell(Vector3 _worldPos, Vector2Int _gridIndex)
	{
		worldPos = _worldPos;
		gridIndex = _gridIndex;
		cost = 1;
	}

	public void IncreaseCost(int newCost)
    {
		if (cost == byte.MaxValue) return;
		if(newCost + cost >= 255 )
        {
			cost = byte.MaxValue;
        }
        else
        {
			cost = (byte)newCost;
        }
    }

}