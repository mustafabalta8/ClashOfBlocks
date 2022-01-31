using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{  
    private MapGenerator mapGenerator;
    public static LevelManager instance;

    [SerializeField] private GameObject winnigUI;

    [Header("Level Environment")]
    [SerializeField] private GameObject environment;
    [SerializeField] private GameObject tileHolder;
    [SerializeField] private GameObject blockKeeper;

    [Header("Enemies")]
    [SerializeField] private Enemy firstEnemy;
    [SerializeField] private GameObject secondEnemy;
    [SerializeField] private GameObject thirdEnemy;
    [SerializeField] private GameObject fourthEnemy;


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
            secondEnemy.GetComponent<Enemy>().StartSpreadingBlocks();
        }
        else if (levels[levelIndex].enemyCount == 3)
        {
            secondEnemy.GetComponent<Enemy>().StartSpreadingBlocks();
            thirdEnemy.GetComponent<Enemy>().StartSpreadingBlocks();
        }
        else if (levels[levelIndex].enemyCount == 4)
        {
            secondEnemy.GetComponent<Enemy>().StartSpreadingBlocks();
            thirdEnemy.GetComponent<Enemy>().StartSpreadingBlocks();
            fourthEnemy.GetComponent<Enemy>().StartSpreadingBlocks();
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
        if (levels[index].enemyCount == 1)
        {
            secondEnemy.SetActive(false);
            thirdEnemy.SetActive(false);
            fourthEnemy.SetActive(false);
        }
        else if (levels[index].enemyCount == 2)
        {
            secondEnemy.SetActive(true);
            thirdEnemy.SetActive(false);
            fourthEnemy.SetActive(false);
            secondEnemy.transform.position = new Vector3(levels[index].enemyPosition2.x, 0, levels[index].enemyPosition2.y);

        }
        else if (levels[index].enemyCount == 3)
        {
            secondEnemy.SetActive(true);
            thirdEnemy.SetActive(true);
            fourthEnemy.SetActive(false);
            secondEnemy.transform.position = new Vector3(levels[index].enemyPosition2.x, 0, levels[index].enemyPosition2.y);
            thirdEnemy.transform.position= new Vector3(levels[index].enemyPosition3.x, 0, levels[index].enemyPosition3.y);
        }
        else if (levels[index].enemyCount == 4)
        {
            secondEnemy.SetActive(true);
            thirdEnemy.SetActive(true);
            fourthEnemy.SetActive(true);
            secondEnemy.transform.position = new Vector3(levels[index].enemyPosition2.x, 0, levels[index].enemyPosition2.y);
            thirdEnemy.transform.position = new Vector3(levels[index].enemyPosition3.x, 0, levels[index].enemyPosition3.y);
            fourthEnemy.transform.position = new Vector3(levels[index].enemyPosition4.x, 0, levels[index].enemyPosition4.y);
        }

    }
    public void OpenWinningUI()
    {
        winnigUI.SetActive(true);
    }

    public void OpenNextLevel()
    {
        winnigUI.SetActive(false);
        DeletePreviousLevel();

        //create next one
        levelIndex++;
        CreateLevel(levelIndex);
    }

    private void DeletePreviousLevel()
    {
        foreach (Transform child in environment.transform)
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

    }
}
