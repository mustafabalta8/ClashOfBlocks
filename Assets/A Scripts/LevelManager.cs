using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Enemy firstEnemy;
    private MapGenerator mapGenerator;
    public static LevelManager instance;

    [SerializeField] private GameObject winnigUI;

    [Header("Level Environment")]
    [SerializeField] private GameObject environment;
    [SerializeField] private GameObject tileHolder;
    public GameObject blockKeeper;
    [SerializeField] private GameObject secondEnemy;

    [Header("Levels Array")]
    [SerializeField] private Level[] levels;

    

    private int levelIndex = 0;
    private void Awake()
    {
        mapGenerator = GetComponent<MapGenerator>();

        if (instance == null) { instance = this; }

        CreateLevel(0);
    }
    public void StartSpreadingEnemyBlocks()
    {
        firstEnemy.StartSpreadingBlocks();
        if (levels[levelIndex].enemyCount == 2)
        {
            Debug.Log("enemy count 2");
            secondEnemy.GetComponent<Enemy>().StartSpreadingBlocks();
        }

    }
    private void CreateLevel(int index)
    {
        Tile.TotalTileCount = levels[index].totalTileCount;
        GameObject levelEnviroment = Instantiate(levels[index].levelWalls, new Vector3(0, 0.4f, 0), Quaternion.identity);
        levelEnviroment.transform.SetParent(environment.transform);


        firstEnemy.transform.position = new Vector3(levels[index].enemyPosition1.x, 0, levels[index].enemyPosition1.y);
        mapGenerator.mapSize = levels[index].mapSize;
        mapGenerator.GenerateMap();

        if (levels[index].enemyCount == 2)
        {
            Instantiate(secondEnemy, new Vector3(levels[index].enemyPosition2.x, 0, levels[index].enemyPosition2.y),
                Quaternion.identity, tileHolder.transform);
        }
    
    }
    public void OpenWinningUI()
    {
        winnigUI.SetActive(true);
    }
    public void OpenNextLevel()
    {
        winnigUI.SetActive(false);
        Debug.Log("button clicked");
        //delete previous level
        foreach(Transform child in environment.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in tileHolder.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in blockKeeper.transform)
        {
            Destroy(child.gameObject);
        }

        //create next one
        levelIndex++;
        CreateLevel(levelIndex);
    }
}
