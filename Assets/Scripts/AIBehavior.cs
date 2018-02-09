using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour {

    public GameObject target;

    Rigidbody targetRigidbody;
    Vector3 targetPosition;
    Vector3 distanceToTarget;


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
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        UpdateTarget(player);
        myShipMovement = this.GetComponent<ShipMovement>();
        //agent.speed;
    }

    void UpdateTarget(GameObject newTarget)
    {
        targetRigidbody = newTarget.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.transform.position);
        if (target)
        {
            targetPosition = target.transform.position;
            distanceToTarget = target.transform.position - this.transform.position; 
        }


        if (distanceToTarget.magnitude > agent.stoppingDistance)
        {

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

