using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	
	public Vector2 MapSize { set; get; }

	[Header("Tile Info")]
	[Range(0, 1)] [SerializeField] private float outlinePercent;
	[SerializeField] private Transform tilePrefab;
	[SerializeField] private Transform tileHolder;

	public static MapGenerator instance;

	void Start()
	{
        if (instance == null)
        {
			instance = this;
        }
	}	
	public void GenerateTiles()
	{

		for (int x = 0; x < MapSize.x; x++)
		{
			for (int y = 0; y < MapSize.y; y++)
			{
				Vector3 tilePosition = new Vector3( (x+1), 0, (y+1));
				Transform newTile = Instantiate(tilePrefab, tilePosition,Quaternion.identity) as Transform;
				newTile.localScale = Vector3.one * (1 - outlinePercent);
				newTile.parent = tileHolder;
			}
		}
		
	}

	

}
