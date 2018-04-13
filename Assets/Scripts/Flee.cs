using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : NPCBaseFSM {
    bool hasStartedFleeing = false;
    float fleeStartTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        target = Ai.target;
        float fleeStartTime = Time.time;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!target)
        {
            return;
        }
        Vector3 directionToFlee = NPC.transform.position - target.transform.position;
        
        // Note the time we are starting to flee

        float targetAngle = Vector3.Angle(NPC.transform.forward, directionToFlee);
  
        if (targetAngle >= accuracy)
        {
            var rotateDir = Vector3.Cross(NPC.transform.forward, directionToFlee).y;
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

        if (directionToFlee.magnitude < agent.stoppingDistance*10 && targetAngle <= accuracy)
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

        //if we have been running for a while and our enemy is not near stop running
        if(fleeStartTime +20.0f >+Time.time && directionToFlee.magnitude >= agent.stoppingDistance * 10)
        {
            target = null;
            animator.SetBool("IsFleeing", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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
