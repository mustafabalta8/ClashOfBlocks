using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyBlock;
    [SerializeField] private GameObject blockKeeper;

    public void StartSpreadingBlocks()
    {
        //Debug.Log($"{gameObject.name} is started");
        Instantiate(enemyBlock, transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity,
            blockKeeper.transform);
    }
}
