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
        transform.position = playerShip.transform.position + offset;
    }
}
