﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject EnemyBlock { set; get; }
    GameObject blockKeeper;
    private void Start()
    {
        blockKeeper = GameObject.Find("BlockKeeper");
    }

    public void StartSpreadingBlocks()
    {
        //Debug.Log($"{gameObject.name} is started");
        Instantiate(EnemyBlock, transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity,
            blockKeeper.transform);//LevelManager.instance.blockKeeper
    }
}
