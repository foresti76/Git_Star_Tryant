using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    GameObject playerShip;
    ShipMovement shipMovement;
    WeaponController[] myWeaponControllers;
    MiningLaser miningLaser;
    Radar myRadar;
    public bool uiOpen = false;

	// Use this for initialization
	void Start () {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        shipMovement = playerShip.GetComponent<ShipMovement>();
        // todo set up fire groups
        myWeaponControllers = playerShip.GetComponentsInChildren<WeaponController>();
        miningLaser = GetComponent<MiningLaser>();
        myRadar = GetComponent<Radar>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!uiOpen)
        {
            // movement controls
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
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

            //firing
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
            //if (Input.GetKey(KeyCode.Keypad1) && miningLaser.firingLaser == false && GetComponentInParent<Rigidbody>().velocity.magnitude == 0)
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit hit;
            //    int layerMask = 1 << 16;
            //    //Debug.DrawRay(ray.origin, ray.direction, Color.red, 3.0f);
            //    if (Physics.Raycast(ray, out hit, 50.0f, layerMask))
            //    {
            //        if (hit.transform.CompareTag("Asteroid") || hit.transform.CompareTag("AIShip"))
            //        {
            //            myRadar.target= hit.transform.gameObject;
            //        }
            //    }
            //}
            //Todo Lock onto a target
            if (Input.GetKeyDown(KeyCode.R))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.Log("Trying to lock on to something.");
                int layerMask = 1 << 16;
                Debug.DrawRay(ray.origin, ray.direction*100, Color.red, 3.0f);

                if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.NameToLayer("AI Shot")))
                {

                    Debug.Log("I cast a ray");
                    if (hit.transform.CompareTag("Asteroid") || hit.transform.CompareTag("AIShip"))
                    {
                        myRadar.target = hit.transform.gameObject;
                        myRadar.RadarLock();
                        Debug.Log("I hit something: " + hit.transform.gameObject.name);
                    }
                }
            }

        }
    }
}
