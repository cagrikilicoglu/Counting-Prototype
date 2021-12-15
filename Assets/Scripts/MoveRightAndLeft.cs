using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightAndLeft : MonoBehaviour
{
    private float moveSpeed = 6;
    private float boundX;
    private SpawnManager spawnManagerScript;
    [SerializeField] bool moveRight;
    [SerializeField] bool moveTheBox;


    void Start()
    {
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        boundX = spawnManagerScript.boundX;
        moveRight = true;
        moveTheBox = true;
    }


    void Update()
    {

        // move the box right and left in a certain range
        if (moveTheBox)
        {
            if (moveRight)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);

                if (transform.position.x >= boundX)
                {
                    moveRight = false;
                }
            }
            else
            {

                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
                if (transform.position.x <= -boundX)
                {
                    moveRight = true;
                }
            }
        }


    }

    // if gun hit the box, stop the box from moving until it is destroyed 
    private void OnTriggerEnter(Collider other)
    {
        moveTheBox = false;
    }
}
