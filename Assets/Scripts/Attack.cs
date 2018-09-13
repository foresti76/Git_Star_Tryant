using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : NPCBaseFSM {

    //Rigidbody targetRigidbody; 
    WeaponController[] myWeaponControllers;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int LayerIndex)
    {
        base.OnStateEnter(animator, stateinfo, LayerIndex);
        target = NPC.GetComponent<AIBehavior>().target;
        //targetRigidbody = target.GetComponent<Rigidbody>();  // Maybe do something with this to help the AI work better.
        myWeaponControllers = NPC.GetComponentsInChildren<WeaponController>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (target)
        {
            Vector3 directionToTarget = target.transform.position - NPC.transform.position;
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

            // turn each turret toward the target. Also need to fire.
            foreach (WeaponController weaponController in myWeaponControllers)
            {
                weaponController.targetPos = Camera.main.WorldToScreenPoint(target.transform.position);
                float angleToGetTo = weaponController.fireAngle;
               
                float currentAngle = weaponController.transform.rotation.eulerAngles.y;
                if (angleToGetTo <= currentAngle + accuracy && angleToGetTo >= currentAngle - accuracy)
                {
                    weaponController.firing = true;
                }
                else if (weaponController.firing == true)
                {
                    weaponController.firing = false;
                }
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
        }
        else
        {
            animator.SetBool("IsAggro", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (WeaponController weaponController in myWeaponControllers)
        {
                 weaponController.firing = false;
        }

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
