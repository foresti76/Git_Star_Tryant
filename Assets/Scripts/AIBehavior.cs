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
            if(angle >= 20)
            {
                var rotateDir = Vector3.Cross(transform.forward, directionToTarget).y;
                Debug.Log("Rotate Dir " + rotateDir);
                if (rotateDir >= 0)
                {
                    turningRight = true; 
                }

                else if (rotateDir < 0)
                {
                    turningLeft = true;
                }
            }
            else if(turningRight || turningLeft)
            {
                turningLeft = false;
                turningRight = false;
            }

            // turn each turret toward the target. Also need to fire.
            foreach (WeaponController weaponController in myWeaponControllers)
            {
                weaponController.targetPos = Camera.main.WorldToScreenPoint(target.transform.position);
                targetRot = Quaternion.LookRotation(directionToTarget);
                float angleToGetTo = targetRot.eulerAngles.y;
                float currentAngle = transform.rotation.eulerAngles.y;

                if (angleToGetTo <= currentAngle + accuracy && angleToGetTo >= currentAngle - accuracy)
                {
                    weaponController.firing = true;
                } else if (weaponController.firing == true)
                {
                    weaponController.firing = false;
                }
            }
        }


        if (directionToTarget.magnitude > agent.stoppingDistance)
        {
           // speedingUp = true;
        } else
        {
            speedingUp = false;
            stopping = true;
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

