using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "LevelConfiguration")]
public class Level : ScriptableObject
{
    public GameObject levelWalls;

    public Vector2 mapSize;

    public Vector2 enemyPosition1;
    public Vector2 enemyPosition2;

    public int totalTileCount;
    public int enemyCount;

    public GameObject firstEnemyBlock;
    public GameObject secondEnemyBlock;

}
