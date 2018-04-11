using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : NPCBaseFSM {

    GameObject[] waypoints;
    int currentWP;

    private void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //todo make it so that it just uses the regular movement fucntionality like in attack to do this.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //if (agent.enabled == false)
        //{
        //    agent.enabled = true;
        //}
        currentWP = 0;
        agent.autoBraking = false;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (waypoints.Length == 0) return;

        if (Vector3.Distance(waypoints[currentWP].transform.position, NPC.transform.position) <= agent.stoppingDistance)
        {
            currentWP++;
            if (currentWP >= waypoints.Length)
            {
                currentWP = 0;
            }
        }

        Vector3 targetPos = waypoints[currentWP].transform.position;
        Vector3 directionToTarget = targetPos - NPC.transform.position;
        float targetAngle = Vector3.Angle(NPC.transform.forward, directionToTarget);
        //Debug.Log("Angle: " + angle);
        if (targetAngle >= accuracy)
        {
            var rotateDir = Vector3.Cross(NPC.transform.forward, directionToTarget).y;
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

        if (directionToTarget.magnitude > agent.stoppingDistance && targetAngle <= accuracy)
        {
            speedingUp = true;
            stopping = false;
        }
        else
        {
            stopping = true;
            speedingUp = false;
        }

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
        //agent.SetDestination(waypoints[currentWP].transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // agent.enabled = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
