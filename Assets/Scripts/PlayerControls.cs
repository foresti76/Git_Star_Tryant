using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    GameObject playerShip;
    ShipMovement shipMovement;
    WeaponController[] myWeaponControllers;
    MiningLaser miningLaser;
	// Use this for initialization
	void Start () {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        shipMovement = playerShip.GetComponent<ShipMovement>();
        // todo set up fire groups
        myWeaponControllers = playerShip.GetComponentsInChildren<WeaponController>();
        miningLaser = GetComponent<MiningLaser>();
    }
	
	// Update is called once per frame
	void Update () {

        // movement controls
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            shipMovement.acclerating = true;
        }
        else if (shipMovement.acclerating)
        {
            shipMovement.acclerating = false;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            shipMovement.decelerating = true;
        }
        else if (shipMovement.decelerating)
        {
            shipMovement.decelerating = false;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            shipMovement.turningRight = true;
        }
        else if (shipMovement.turningRight)
        {
            shipMovement.turningRight = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
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
        //todo this should be set to use a generic subsystem type and not hard coded to the mining laser
        if (Input.GetKey(KeyCode.Keypad1) && miningLaser.firingLaser == false && GetComponentInParent<Rigidbody>().velocity.magnitude == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Asteroid"))
                {
                    GameObject target = hit.transform.gameObject;
                    miningLaser.ActivateLaser(target);
                }
            }
        }
        //Todo Lock onto a target
        if (Input.GetKey(KeyCode.R))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag(""))
                {
                    GameObject target = hit.transform.gameObject;
                    //Radar.SetTarget(target);
                }
            }
        }
    }
}
