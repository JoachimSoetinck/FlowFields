using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Vector2Int gridSize;
    public float cellRadius = 0.5f;
    public FlowField grid;
    public Debugger gridDebug;
    Cell destination;
    public Cell Destination
    {
        get { return destination; }
    }


    private void InitializeFlowField()
    {
        grid = new FlowField(cellRadius, gridSize);
        grid.CreateGrid();
        gridDebug.SetFlowField(grid);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InitializeFlowField();
            grid.CreateCostField();
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            Vector3 worldPosMouse = Camera.main.ScreenToWorldPoint(mousePos);
            destination = grid.GetCellFromWorldPos(worldPosMouse);
            grid.GenerateIntegrationField(destination);
            grid.CreateFlowField();
        }
  
       

    }

}
