using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public Transform tilePrefab;
	public Vector2 mapSize;

	[Range(0, 1)]
	public float outlinePercent;

	[SerializeField] Transform tileHolder;
	public static MapGenerator instance;

	//public static int TileCount { set; get; }
	void Start()
	{
        if (instance == null)
        {
			instance = this;
        }
	}	
	public void GenerateMap()
	{
		/*
		string holderName = "Generated Map";
		if (transform.Find(holderName))
		{
			DestroyImmediate(transform.Find(holderName).gameObject);
		}
	    mapHolder = new GameObject(holderName).transform;
		mapHolder.parent = transform;*/

		for (int x = 0; x < mapSize.x; x++)
		{
			for (int y = 0; y < mapSize.y; y++)
			{
				Vector3 tilePosition = new Vector3( (x+1), 0, (y+1));
				Transform newTile = Instantiate(tilePrefab, tilePosition,Quaternion.identity) as Transform;
				newTile.localScale = Vector3.one * (1 - outlinePercent);
				newTile.parent = tileHolder;
			}
		}
		
	}

	

}
