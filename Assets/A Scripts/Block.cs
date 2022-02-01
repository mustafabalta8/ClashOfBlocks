using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private float raycastDistance = 1f;

    private GameObject blockKeeper;
    private void Start()
    {
        transform.localScale = new Vector3(0.97f, 0.97f, 0.97f);

        blockKeeper = GameObject.Find("BlockKeeper");

        Invoke("BlockCreator", 0.2f);
        
    }   
    private void BlockCreator()
    {
        RaycastHit other;

        if(Physics.Raycast(transform.position,Vector3.right,out other, raycastDistance))
        {
            //if (other.collider.gameObject.tag == "Enemy" || other.collider.gameObject.tag == "Block") {  }
            if (other.collider.gameObject.tag == "Tile")
            {
                Instantiate(block, transform.position + Vector3.right, Quaternion.identity, blockKeeper.transform);
            }
        }
        if (Physics.Raycast(transform.position, Vector3.left, out other, raycastDistance))
        {
            //if (other.collider.gameObject.tag == "Enemy" || other.collider.gameObject.tag == "Block") {  }
            if (other.collider.gameObject.tag == "Tile")
            {
                Instantiate(block, transform.position + Vector3.left, Quaternion.identity, blockKeeper.transform);
            }
        }
        if (Physics.Raycast(transform.position, Vector3.forward, out other, raycastDistance))
        {
            //if (other.collider.gameObject.tag == "Enemy" || other.collider.gameObject.tag == "Block") { }
            if (other.collider.gameObject.tag == "Tile")
            {
                Instantiate(block, transform.position + Vector3.forward, Quaternion.identity, blockKeeper.transform);
            }
        }
        if (Physics.Raycast(transform.position, Vector3.back, out other, raycastDistance))
        {
            //if (other.collider.gameObject.tag == "Enemy" || other.collider.gameObject.tag == "Block") { }
             if (other.collider.gameObject.tag == "Tile")
            {
                Instantiate(block, transform.position + Vector3.back, Quaternion.identity, blockKeeper.transform);
            }
        }


    }/*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.right);
        Gizmos.DrawRay(transform.position, Vector3.forward);
        Gizmos.DrawRay(transform.position, Vector3.left);
        Gizmos.DrawRay(transform.position, Vector3.back);
    }*/

    /*
    private void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }      
    
    }*/






}
