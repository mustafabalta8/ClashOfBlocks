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

    public static int PlayerBlockCount { set; get; }
    public static int RedEnemyBlockCount { set; get; }
    public static int YellowEnemyBlockCount { set; get; }

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
            //Debug.Log("Block isFilled = true"); // -> important to count TotalTileCount 
            isFilled = true;
            FilledTileCount += 1;
            //Debug.Log("other.gameObject.tag: "+ other.gameObject.tag);
            if (other.gameObject.tag == "Block")
            {
                PlayerBlockCount++;
            }
            else if (other.gameObject.tag == "RedEnemy")
            {
                RedEnemyBlockCount++;
            }
            else if (other.gameObject.tag == "YellowEnemy")
            {
                YellowEnemyBlockCount++;
            }


            if (FilledTileCount >= TotalTileCount)
            {
                CompleteTheLevel();
            }

        }
    }
    private void CompleteTheLevel()
    {
        int playerPercent = 100 * PlayerBlockCount / TotalTileCount;
        int redEnemyPercent = 100 * RedEnemyBlockCount / TotalTileCount;
        int yellowEnemyPercent = 100 * YellowEnemyBlockCount / TotalTileCount;
        // Debug.Log("playerPercent" + playerPercent);
        // print("redEnemyPercent" + redEnemyPercent);

        LevelManager.instance.ShowPercentsUI(redEnemyPercent, yellowEnemyPercent,playerPercent,playerStartingPosition);


        //Debug.LogWarning($"level ended\nFilledTileCount: {FilledTileCount}");

        if(playerPercent > redEnemyPercent && playerPercent > yellowEnemyPercent)
        {
            Invoke("OpenWinUI", 0.3f);

        }
        else
        {
            Invoke("OpenLoseUI", 0.3f);
        }
        
        FilledTileCount = 0;
        PlayerBlockCount = 0;
        RedEnemyBlockCount = 0;
        YellowEnemyBlockCount = 0;
    }

    
    
    private void OpenWinUI()
    {
        LevelManager.instance.OpenWinningUI();
    }
    private void OpenLoseUI()
    {
        LevelManager.instance.OpenLoseUI();
    }

}
