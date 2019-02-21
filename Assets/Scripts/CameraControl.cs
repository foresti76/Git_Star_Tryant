using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour
{
    public GameObject playerShip;
   
    private Vector3 offset;


    void Start()
    {
        offset = transform.position - playerShip.transform.position;
    }

    void Update()
    {
        if (playerShip)
        { 
        transform.position =new Vector3(playerShip.transform.position.x, offset.y, playerShip.transform.position.z);
        }
        else
        {
            Invoke("FindPlayer", 1.0f);
        }
    }

    public void FindPlayer()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
    }
}
