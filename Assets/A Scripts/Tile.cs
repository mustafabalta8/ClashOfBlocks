using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool isFilled = false;

    [SerializeField] private GameObject block;
    public static int FilledTileCount{ set; get; }
    public static int TotalTileCount { set; get; }

    private GameObject blockKeeper;

    private static int playerBlockCount=0;
    private static int redEnemyBlockCount=0;
    private static int yellowEnemyBlockCount = 0;

    private static Vector3 playerStartingPosition;
    private void Start()
    {
        blockKeeper = GameObject.Find("BlockKeeper");
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Tile is triggerred by tag = {other.gameObject.tag} ");
        if (other.gameObject.tag == "Wall")
        {
            print("Tile is destroyed by wall");
            Destroy(gameObject);
        }

        if (isFilled == false)
        {
            FillTheEmptyTile(other);
        }
        else if (isFilled && other.gameObject.tag == "Block")
        {
            Destroy(other.gameObject);
            Debug.LogError($"{other.gameObject.name} is destroyed");
        }
    }   
    
    private void OnMouseDown()
    {
        //Start spreading player blocks
        playerStartingPosition = transform.position + new Vector3(0, 0.25f, 0);
        Instantiate(block, playerStartingPosition, Quaternion.identity, blockKeeper.transform);

        //Start spreading enemy blocks
        LevelManager.instance.StartSpreadingEnemyBlocks();
    }
    private void FillTheEmptyTile(Collider other)
    {
        if (other.gameObject.tag == "Block" || other.gameObject.tag == "RedEnemy" || other.gameObject.tag == "YellowEnemy")
        {
            Debug.Log("Block isFilled = true");
            isFilled = true;
            FilledTileCount += 1;
            Debug.Log("other.gameObject.tag: "+ other.gameObject.tag);
            if (other.gameObject.tag == "Block")
            {
                playerBlockCount++;
            }
            else if (other.gameObject.tag == "RedEnemy")
            {
                redEnemyBlockCount++;
            }
            else if (other.gameObject.tag == "YellowEnemy")
            {
                yellowEnemyBlockCount++;
            }


            if (FilledTileCount >= TotalTileCount)
            {
                CompleteTheLevel();
            }

        }
    }
    private void CompleteTheLevel()
    {
        int playerPercent = 100 * playerBlockCount / TotalTileCount;
        int redEnemyPercent = 100 * redEnemyBlockCount / TotalTileCount;
        int yellowEnemyPercent = 100 * yellowEnemyBlockCount / TotalTileCount;

        Debug.Log("playerBlockCount" + playerBlockCount);
       // Debug.Log("enemyBlockCount" + redEnemyBlockCount);
        Debug.Log("playerPercent" + playerPercent);
       // print("enemyPercent" + redEnemyPercent);
        LevelManager.instance.ShowPercentsUI(redEnemyPercent, yellowEnemyPercent,playerPercent,playerStartingPosition);


        Debug.LogWarning($"level ended\nFilledTileCount: {FilledTileCount}");
        LevelManager.instance.OpenWinningUI();
        FilledTileCount = 0;
        playerBlockCount = 0;
        redEnemyBlockCount = 0;
        yellowEnemyBlockCount = 0;
    }
}
