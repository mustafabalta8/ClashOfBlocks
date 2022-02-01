using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int witdh, height;

    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform tileParentObject;

    [SerializeField] GameObject floor;

    private void Start()
    {
        GenerateGrid();
    }
    private void GenerateGrid()
    {
        for(int x = 0; x < witdh; x++)
        {
            for(int z = 0; z < height; z++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, 0, z), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {z}";
                spawnedTile.transform.SetParent(tileParentObject);
            }
        }



     

    }
}
