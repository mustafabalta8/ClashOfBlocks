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
    private Vector3 playerUI_Offset = new Vector3(0, 1.35f, 1.2f);
    private void Awake()
    {
        mapGenerator = GetComponent<MapGenerator>();

        if (instance == null) { instance = this; }

        CreateLevel();
    }
    

    public void StartSpreadingEnemyBlocks()
    {
        firstEnemy.StartSpreadingBlocks();

        if (levels[levelIndex].enemyCount == 2)
        {
            StartSpreading(secondEnemy);
        }
        else if (levels[levelIndex].enemyCount == 3)
        {
            StartSpreading(secondEnemy);
            StartSpreading(thirdEnemy);
        }
        else if (levels[levelIndex].enemyCount == 4)
        {
            StartSpreading(secondEnemy);
            StartSpreading(thirdEnemy);
            StartSpreading(fourthEnemy);
        }

    }
    private void StartSpreading(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().StartSpreadingBlocks();
    }


    public void RestartLevel()
    {
        DeletePreviousLevel();
        CreateLevel();
        loseUI.SetActive(false);
        Tile.FilledTileCount = 0;
        Tile.PlayerBlockCount = 0;
        Tile.RedEnemyBlockCount = 0;
        Tile.YellowEnemyBlockCount = 0;
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
        if (levelIndex == 9) { levelIndex = -1; }
        levelIndex++;      
        CreateLevel();
    }
    private void CreateLevel()
    {
        levelText.text = "Level " + (levelIndex + 1);
        Tile.TotalTileCount = levels[levelIndex].totalTileCount;

        //set camera position
        //  (x/2 +0.5), x*2
        mainCamera.position = new Vector3((levels[levelIndex].mapSize.x/2+0.5f), levels[levelIndex].mapSize.x*2, 0);

        //instantiate walls
        GameObject levelEnviroment = Instantiate(levels[levelIndex].levelWalls, new Vector3(0, 0.4f, 0), Quaternion.identity);
        levelEnviroment.transform.SetParent(environment.transform);         

        //Generate Tiles
        mapGenerator.MapSize = levels[levelIndex].mapSize;
        mapGenerator.GenerateTiles();

        //Set enemy positions 
        SetEnemyPositions();

    }
    private void SetEnemyPositions()
    {
        firstEnemy.transform.position = new Vector3(levels[levelIndex].enemyPosition1.x, 0, levels[levelIndex].enemyPosition1.y);
        switch (levels[levelIndex].enemyCount)
        {
            case 1:                
                secondEnemy.SetActive(false);
                thirdEnemy.SetActive(false);
                fourthEnemy.SetActive(false);
                break;
            case 2:
                secondEnemy.SetActive(true);
                thirdEnemy.SetActive(false);
                fourthEnemy.SetActive(false);
                SetEnemyPosition(secondEnemy, levels[levelIndex].enemyPosition2);
                break;
            case 3:
                secondEnemy.SetActive(true);
                thirdEnemy.SetActive(true);
                fourthEnemy.SetActive(false);
                SetEnemyPosition(secondEnemy, levels[levelIndex].enemyPosition2);
                SetEnemyPosition(thirdEnemy, levels[levelIndex].enemyPosition3);
                break;
            case 4:
                secondEnemy.SetActive(true);
                thirdEnemy.SetActive(true);
                fourthEnemy.SetActive(true);
                SetEnemyPosition(secondEnemy, levels[levelIndex].enemyPosition2);
                SetEnemyPosition(thirdEnemy, levels[levelIndex].enemyPosition3);
                SetEnemyPosition(fourthEnemy, levels[levelIndex].enemyPosition4);
                break;
        }
    }
    private void SetEnemyPosition(GameObject enemy, Vector2 enemyPositon)
    {
        enemy.transform.position= new Vector3(enemyPositon.x, 0, enemyPositon.y);
    }

    
    public void ShowPercentsUI(int firstEnemyPercent, int secondEnemyPercent, int playerPercent, Vector3 playerUI_Pos )
    {
        ShowPercentUI(firstEnemyUI, firstEnemyPercent);
        ShowPercentUI(secondEnemyUI, secondEnemyPercent);
        playerPercentUI.transform.position = playerUI_Pos + playerUI_Offset;
        ShowPercentUI(playerPercentUI, playerPercent);

    }
    private void ShowPercentUI(GameObject blockUI, int percent)
    {
        blockUI.SetActive(true);
        blockUI.transform.GetChild(1).GetComponent<Text>().text = percent + "%";
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
