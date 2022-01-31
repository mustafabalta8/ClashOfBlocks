using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "LevelConfiguration")]
public class Level : ScriptableObject
{
    public GameObject levelWalls;

    [Header("Statistics")]
    public Vector2 mapSize;
    public int totalTileCount;
    public int enemyCount;

    [Header("Enemies")]
    public Vector2 enemyPosition1;
    public Vector2 enemyPosition2;
    public Vector2 enemyPosition3;
    public Vector2 enemyPosition4;


}
