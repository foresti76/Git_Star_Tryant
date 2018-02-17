using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour {

    public GameObject target;
    public float accuracy;

    Rigidbody targetRigidbody;
    Vector3 targetPos;
    Vector3 directionToTarget;
    Quaternion targetRot;
    WeaponController[] myWeaponControllers;

    UnityEngine.AI.NavMeshAgent agent;
    ShipMovement myShipMovement;
    //todo bring in the ship data



    //variables to tell which way i am moving
    bool speedingUp;
    bool turningLeft;
    bool turningRight;
    bool slowingDown;
    bool stopping;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        UpdateTarget(player);
        myShipMovement = GetComponent<ShipMovement>();
        myWeaponControllers = GetComponentsInChildren<WeaponController>();
        //agent.speed;
    }

    void UpdateTarget(GameObject newTarget)
    {
        target = newTarget;
        targetRigidbody = newTarget.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.transform.position);
        if (target)
        {
            targetPos = target.transform.position;
            directionToTarget = target.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            Debug.Log("Angle: " + angle);
            if (angle >= accuracy)
            {
                var rotateDir = Vector3.Cross(transform.forward, directionToTarget).y;
                //Debug.Log("Rotate Dir " + rotateDir);
                if (rotateDir >= 0)
                {
                    turningRight = true;
                    turningLeft = false;
                }

                else if (rotateDir < 0)
                {
                    turningRight = false;
                    turningLeft = true;
                }
            }
            else
            {
                turningLeft = false;
                turningRight = false;
            }

            // turn each turret toward the target. Also need to fire.
            foreach (WeaponController weaponController in myWeaponControllers)
            {
                weaponController.targetPos = Camera.main.WorldToScreenPoint(target.transform.position);
                float angleToGetTo = weaponController.fireAngle;
                //Debug.Log("weapon Angle to get " + angleToGetTo);
                float currentAngle = weaponController.transform.rotation.eulerAngles.y;
                //Debug.Log("Weapon Current Angle " + currentAngle);
                if (angleToGetTo <= currentAngle + accuracy && angleToGetTo >= currentAngle - accuracy)
                {
                    weaponController.firing = true;
                } else if (weaponController.firing == true)
                {
                    weaponController.firing = false;
                }
            }
        }

       // Debug.Log("Direction to target Magnatue " + directionToTarget.magnitude);
        if (directionToTarget.magnitude > agent.stoppingDistance)
        {
           speedingUp = true;
            stopping = false;
        } else
        {
            speedingUp = false;
        }

        //stop when you get close to the target
        if (directionToTarget.magnitude <= agent.stoppingDistance && GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            stopping = true;
            speedingUp = false;
        } else if (!speedingUp)
        {
            stopping = false;
        }
        //todo hook up the bools so that they are true when things are happening
        //set off the effects when you are turning
        
        if (speedingUp)
        {
            myShipMovement.acclerating = true;
        }
        else if (myShipMovement.acclerating)
        {
            myShipMovement.acclerating = false;
        }

        if (slowingDown)
        {
            myShipMovement.decelerating = true;
        }
        else if (myShipMovement.decelerating)
        {
            myShipMovement.decelerating = false;
        }

        if (turningRight)
        {
            myShipMovement.turningRight = true;
        }
        else if (myShipMovement.turningRight)
        {
            myShipMovement.turningRight = false;
        }

        if (turningLeft)
        {
            myShipMovement.turningLeft = true;
        }
        else if (myShipMovement.turningLeft)
        {
            myShipMovement.turningLeft = false;
        }

        if (stopping)
        {
            myShipMovement.stopping = true;
        }
        else if (myShipMovement.stopping)
        {
            myShipMovement.stopping = false;
        } 

    }

           
        
}

