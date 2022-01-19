using UnityEditor;
using UnityEngine;


public enum FlowFieldDisplayType { None, AllIcons, DestinationIcon, CostField, IntegrationField };

public class Debugger : MonoBehaviour
{
    public GridController gridController;
    public bool displayGrid;

    public FlowFieldDisplayType curDisplayType;

    private Vector2Int gridSize;
    private float cellRadius;
    private FlowField curFlowField;

    private Sprite[] ffIcons;

    private void Start()
    {
        ffIcons = Resources.LoadAll<Sprite>("Sprites/FFicons");
    }

    public void SetFlowField(FlowField newFlowField)
    {
        curFlowField = newFlowField;
        cellRadius = newFlowField.cellRadius;
        gridSize = newFlowField.gridSize;
    }

    public void ClearCellDisplay()
    {
        foreach (Transform t in transform)
        {
            GameObject.Destroy(t.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (displayGrid)
        {
            if (curFlowField == null)
            {
                DrawGrid(gridController.gridSize, Color.yellow, gridController.cellRadius);
            }
            else
            {
                DrawGrid(gridSize, Color.green, cellRadius);
            }
        }

        if (curFlowField == null) { return; }

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;

        switch (curDisplayType)
        {
            case FlowFieldDisplayType.CostField:

                foreach (Cell curCell in curFlowField.grid)
                {
                    Handles.Label(curCell.worldPos, curCell.cost.ToString(), style);
                }
                break;

            case FlowFieldDisplayType.IntegrationField:

                foreach (Cell curCell in curFlowField.grid)
                {
                    if(curCell.BestCost < byte.MaxValue)
                    Handles.Label(curCell.worldPos, curCell.BestCost.ToString(), style);
                    else
                        Handles.Label(curCell.worldPos, "X", style);
                }
                break;
            case FlowFieldDisplayType.AllIcons:

                foreach (Cell curCell in curFlowField.grid)
                {
                    if(curCell.BestDirection.x == 0 && curCell.BestDirection.y == 1)
                       Handles.Label(curCell.worldPos, "N", style);

                    if (curCell.BestDirection.x == 1 && curCell.BestDirection.y == 1)
                        Handles.Label(curCell.worldPos, "NE", style);

                    if (curCell.BestDirection.x == 1 && curCell.BestDirection.y == 0)
                    {
                        Handles.Label(curCell.worldPos, "E", style);
              
                    }


                    if (curCell.BestDirection.x == 1 && curCell.BestDirection.y == -1)
                        Handles.Label(curCell.worldPos, "SE", style);

                    if (curCell.BestDirection.x == 0 && curCell.BestDirection.y == -1)
                        Handles.Label(curCell.worldPos, "S", style);

                    if (curCell.BestDirection.x == -1 && curCell.BestDirection.y == -1)
                        Handles.Label(curCell.worldPos, "SW", style);

                    if (curCell.BestDirection.x == -1 && curCell.BestDirection.y == 0)
                        Handles.Label(curCell.worldPos, "W", style);
                    if (curCell.BestDirection.x == -1 && curCell.BestDirection.y == 1)
                        Handles.Label(curCell.worldPos, "NW", style);

                    if (curCell.BestDirection.x == 0 && curCell.BestDirection.y == 0)
                        Handles.Label(curCell.worldPos, "None", style);
                  
                    if(!curCell.BestDirection.Equals((0,0)))
                    {
                        Gizmos.color = Color.black;
                        Gizmos.DrawLine(curCell.worldPos, new Vector3(curCell.worldPos.x + curCell.BestDirection.x, curCell.worldPos.y, curCell.worldPos.z + curCell.BestDirection.y));

                    }
                      
                }
                break;
            default:
                break;
        }

    }

    private void DrawGrid(Vector2Int drawGridSize, Color drawColor, float drawCellRadius)
    {
        Gizmos.color = drawColor;
        for (int x = 0; x < drawGridSize.x; x++)
        {
            for (int y = 0; y < drawGridSize.y; y++)
            {
                Vector3 center = new Vector3(drawCellRadius * 2 * x + drawCellRadius, 0, drawCellRadius * 2 * y + drawCellRadius);
                Vector3 size = Vector3.one * drawCellRadius * 2;
                Gizmos.DrawWireCube(center, size);
            }
        }
    }
}