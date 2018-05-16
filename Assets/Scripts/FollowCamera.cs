using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public Transform playerShip;
	// Use this for initialization
	void Start () {
        playerShip = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 camPosition = new Vector3(playerShip.position.x, 50.0f, playerShip.position.z);
        transform.position = camPosition;
	}
}
