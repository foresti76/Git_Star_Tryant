using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    GameObject playerShip;
    ShipMovement shipMovement;
    WeaponController[] myWeaponControllers;
	// Use this for initialization
	void Start () {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        shipMovement = playerShip.GetComponent<ShipMovement>();
        // todo set up fire groups
        myWeaponControllers = playerShip.GetComponentsInChildren<WeaponController>();
    }
	
	// Update is called once per frame
	void Update () {

        // movement controls
        if(Input.GetKey(KeyCode.UpArrow))
        {
            shipMovement.acclerating = true;
        }
        else if (shipMovement.acclerating)
        {
            shipMovement.acclerating = false;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            shipMovement.decelerating = true;
        }
        else if (shipMovement.decelerating)
        {
            shipMovement.decelerating = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            shipMovement.turningRight = true;
        }
        else if (shipMovement.turningRight)
        {
            shipMovement.turningRight = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            shipMovement.turningLeft = true;
        }
        else if (shipMovement.turningLeft)
        {
            shipMovement.turningLeft = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            shipMovement.stopping = true;
        }
        else if (shipMovement.stopping)
        {
            shipMovement.stopping = false;
        }

        if (Input.GetButton("Fire1"))
        {
            foreach (WeaponController weaponController in myWeaponControllers)
            {
                weaponController.firing = true;
            }
        } else
        {
            foreach (WeaponController weaponController in myWeaponControllers)
            {
                weaponController.firing = false;
            }
        }

        foreach (WeaponController weaponController in myWeaponControllers)
        {
            Vector3 targetPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            targetPos.x = targetPos.x * Screen.width;
            targetPos.y = targetPos.y * Screen.height;

            weaponController.targetPos = targetPos;
        }




    }
}
