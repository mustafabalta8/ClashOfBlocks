using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyBlock;
    private GameObject blockKeeper;
    private void Awake()
    {
        blockKeeper = GameObject.Find("BlockKeeper");
    }
    private void Start()
    {
        
    }

    public void StartSpreadingBlocks()
    {
        Debug.Log($"{gameObject.name} is started");
        Instantiate(enemyBlock, transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity,
            blockKeeper.transform);//LevelManager.instance.blockKeeper
    }
}
