using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    public float backgroundScrollSpeed = .000001f;  // Todo tune this value.

    private GameObject playerShip;
    private Renderer backgroundRenderer;
    private float xTextureCoord = 0; // Todo make this relative to the center of the zone when you start.
    private float yTextureCoord = 0; // Todo make this relative to the center of the zone when you start.
    private Rigidbody myRigidbody;
//    private ShipMovement shipMovement;
    
    // Use this for initialization
    void Start () {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        myRigidbody = playerShip.GetComponent<Rigidbody>();
 //       shipMovement = playerShip.GetComponent<ShipMovement>();
        backgroundRenderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (myRigidbody)
        {
            xTextureCoord += -myRigidbody.velocity.x * backgroundScrollSpeed;
            yTextureCoord += -myRigidbody.velocity.z * backgroundScrollSpeed;
        }
        //Debug.Log("texture coord x + y = " + xTextureCoord + " : " + yTextureCoord);
        backgroundRenderer.material.SetTextureOffset("_MainTex", new Vector2(xTextureCoord, yTextureCoord));

        if (playerShip)
        {
            this.transform.position = new Vector3(playerShip.transform.position.x,-2 , playerShip.transform.position.z); // Keep the background with the players ship
        }
    }
}
