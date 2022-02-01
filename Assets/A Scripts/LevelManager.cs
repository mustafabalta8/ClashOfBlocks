using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{  
    private MapGenerator mapGenerator;
    public static LevelManager instance;

    [SerializeField] private Transform mainCamera;

    [Header("UI")]
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private Text levelText;

    [Header("Level Environment")]
    [SerializeField] private GameObject environment;
    [SerializeField] private GameObject tileHolder;
    [SerializeField] private GameObject blockKeeper;

    [Header("Enemies")]
    [SerializeField] private Enemy firstEnemy;
    [SerializeField] private GameObject secondEnemy;
    [SerializeField] private GameObject thirdEnemy;
    [SerializeField] private GameObject fourthEnemy;
    [Header("Percentage UIs")]
    [SerializeField] private GameObject playerPercentUI;
    [SerializeField] private GameObject firstEnemyUI;
    [SerializeField] private GameObject secondEnemyUI;
    [SerializeField] private GameObject thirdEnemyUI;
    [SerializeField] private GameObject fourthEnemyUI;

    [Header("Levels Array")]
    [SerializeField] private Level[] levels;


    private int levelIndex = 0;
    private Vector3 playerUIOffset = new Vector3(0, 1.35f, 1.2f);
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


    public void RestartLevel()
    {
        DeletePreviousLevel();
        CreateLevel(levelIndex);
        loseUI.SetActive(false);
    }

    public void OpenWinningUI()
    {
        winUI.SetActive(true);
    }
    public void OpenLoseUI()
    {
        loseUI.SetActive(true);
    }
    public void OpenNextLevel()
    {
        winUI.SetActive(false);
        loseUI.SetActive(false);
        DeletePreviousLevel();

        //create next level
        levelIndex++;
        CreateLevel(levelIndex);
    }
    private void CreateLevel(int index)
    {
        levelText.text = "Level " + (levelIndex + 1);
        Tile.TotalTileCount = levels[index].totalTileCount;

        //set camera position
        //  (x/2 +0.5), x*2
        mainCamera.position = new Vector3((levels[index].mapSize.x/2+0.5f), levels[index].mapSize.x*2, 0);


        //instantiate walls
        GameObject levelEnviroment = Instantiate(levels[index].levelWalls, new Vector3(0, 0.4f, 0), Quaternion.identity);
        levelEnviroment.transform.SetParent(environment.transform);         

        //Generate Tiles
        mapGenerator.mapSize = levels[index].mapSize;
        mapGenerator.GenerateMap();

        //Set enemy positions 
        firstEnemy.transform.position = new Vector3(levels[index].enemyPosition1.x, 0, levels[index].enemyPosition1.y);
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

    
    public void ShowPercentsUI(int firstEnemyPercent, int secondEnemyPercent, int playerPercent, Vector3 playerUI_Pos )
    {
        firstEnemyUI.SetActive(true);
        firstEnemyUI.transform.GetChild(1).GetComponent<Text>().text = firstEnemyPercent + "%";

        secondEnemyUI.SetActive(true);
        secondEnemyUI.transform.GetChild(1).GetComponent<Text>().text = secondEnemyPercent + "%";

        playerPercentUI.transform.position = playerUI_Pos + playerUIOffset;
        playerPercentUI.SetActive(true);
        playerPercentUI.transform.GetChild(1).GetComponent<Text>().text = playerPercent + "%";

    }    
    private void DeletePreviousLevel()
    {
        firstEnemyUI.SetActive(false);
        secondEnemyUI.SetActive(false);
        thirdEnemyUI.SetActive(false); 
        fourthEnemyUI.SetActive(false);
        playerPercentUI.SetActive(false);


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
