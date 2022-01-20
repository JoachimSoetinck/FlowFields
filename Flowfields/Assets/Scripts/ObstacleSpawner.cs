using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GridController gridController;
    public GameObject walPrefab;
    public GameObject GrassPrefab;
    bool CanSpawnGrass = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CanSpawnGrass = !CanSpawnGrass;
        }

            if (Input.GetMouseButtonDown(1))
        {
            if (CanSpawnGrass == false)
            {
                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
                Vector3 worldPosMouse = Camera.main.ScreenToWorldPoint(mousePos);
                SpawnWall(worldPosMouse);
                gridController.grid.GenerateIntegrationField(gridController.Destination);
            }
            else
            {
                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
                Vector3 worldPosMouse = Camera.main.ScreenToWorldPoint(mousePos);
                SpawnGrass(worldPosMouse);
                gridController.grid.GenerateIntegrationField(gridController.Destination);
            }
        }
    }


    private void SpawnWall(Vector3 wordlPos)
    {
        Vector2Int gridSize = gridController.gridSize;
        float nodeRadius = gridController.cellRadius;
        Vector2 maxSpawnPos = new Vector2(gridSize.x * nodeRadius * 2 + nodeRadius, gridSize.y * nodeRadius * 2 + nodeRadius);
      
        GameObject newUnit = Instantiate(walPrefab, wordlPos, Quaternion.identity);
        newUnit.transform.parent = transform;

  
    }

    private void SpawnGrass(Vector3 wordlPos)
    {
        Vector2Int gridSize = gridController.gridSize;
        float nodeRadius = gridController.cellRadius;

        Vector2 maxSpawnPos = new Vector2(gridSize.x * nodeRadius + nodeRadius, gridSize.y * nodeRadius + nodeRadius);
        GameObject newUnit = Instantiate(GrassPrefab, wordlPos, Quaternion.identity);
        newUnit.transform.parent = transform;
    }
}
