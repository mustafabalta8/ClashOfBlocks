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



        /*
        Color gridColor = Color.cyan;
        Color borderColor = Color.black;
        Collider floorCollider = floor.GetComponent<Collider>();
        Vector3 foorSize = new Vector3(floorCollider.bounds.size.x, floorCollider.bounds.size.z);
        for (int x = 0; x < gridImage.width; x++)
        {
            for (int y = 0; y < gridImage.height; y++)
            {
                if (x < borderSize || x > gridImage.width - borderSize || y < borderSize || y > gridImage.height - borderSize)
                {
                    gridImage.SetPixel(x, y, new Color(borderColor.r, borderColor.g, borderColor.b, 50));
                }
                else gridImage.SetPixel(x, y, new Color(gridColor.r, gridColor.g, gridColor.b, 50));
            }
            gridImage.Apply();
        }
        */

    }
}
