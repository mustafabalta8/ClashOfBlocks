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

    private void Start()
    {
        blockKeeper = GameObject.Find("BlockKeeper");
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Tile is triggerred by tag = {other.gameObject.tag} ");
        //Debug.Log("isFilled: "+isFilled);
        if (other.gameObject.tag == "Wall")
        {
            print("Tile is destroyed by wall");
            Destroy(gameObject);
        }

        if (isFilled == false)
        {
            if(other.gameObject.tag == "Block" || other.gameObject.tag == "Enemy")
            {
                Debug.Log("Block isFilled = true");
                isFilled = true;
                FilledTileCount+=1;
                //Debug.Log("FilledTileCount: "+ FilledTileCount);

                if(FilledTileCount >= TotalTileCount)
                {
                    Debug.LogWarning($"level ended\nFilledTileCount: {FilledTileCount}");
                    LevelManager.instance.OpenWinningUI();
                    FilledTileCount = 0;
                }
            }

        }
        else if (isFilled && other.gameObject.tag == "Block")
        {
            Destroy(other.gameObject);
            Debug.Log($"{other.gameObject.name} is destroyed");
        }
    }   
    
    private void OnMouseDown()
    {
        //Start spreading player blocks
        Instantiate(block, transform.position + new Vector3(0,0.25f,0), Quaternion.identity, blockKeeper.transform);

        //Start spreading enemy blocks
        LevelManager.instance.StartSpreadingEnemyBlocks();
    }
}
