using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShipMovement : MonoBehaviour {
    public float engineThrust = 10f;
    public float reverseThrust = 5f;
    public float maxSpeed = 10f;
    public float rotateThrust = 10f;
    public float engineEnergyCost = 10f;
    public float rcsEnergyCost = 10F;
    public bool acclerating = false;
    public bool decelerating = false;
    public bool turningRight = false;
    public bool turningLeft = false;
    public bool stopping = false;

    public ParticleSystem engineFlare;
    public ParticleSystem engineCore;
    public ParticleSystem rightManuverJetRight;
    public ParticleSystem rightManuverJetLeft;
    public ParticleSystem leftManuverJetLeft;
    public ParticleSystem leftManuverJetRight;
    public ParticleSystem frontJet;


    private Rigidbody myRigidBody;
    private Generator myGenerator;
    private float myYPos;

    // Use this for initialization
    void Start () {
        myRigidBody = GetComponent<Rigidbody>();
        myGenerator = GetComponent<Generator>();
        myYPos = transform.position.y;
    }

	
	// Update is called once per frame
	void FixedUpdate () {

        if(myRigidBody.velocity.magnitude >= maxSpeed)
        {
            myRigidBody.velocity = myRigidBody.velocity.normalized * maxSpeed;
        } 
        

        if (acclerating && myGenerator.currentPower >= engineEnergyCost)
        {
            // consume energy
            myGenerator.currentPower -= engineEnergyCost;
            // add thrust
            myRigidBody.AddRelativeForce(Vector3.forward * engineThrust * Time.deltaTime);
            // play VFX out the back
            var coreMain = engineCore.main;
            var flareMain = engineFlare.main;
            coreMain.startRotationZ = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
            flareMain.startRotationZ = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
            engineFlare.gameObject.SetActive(true);
            engineCore.gameObject.SetActive(true);
        } else
        {
            engineFlare.gameObject.SetActive(false);
            engineCore.gameObject.SetActive(false);
        }

        if (decelerating && myGenerator.currentPower >= engineEnergyCost)
        {
            myGenerator.currentPower -= engineEnergyCost;
            myRigidBody.AddRelativeForce(Vector3.back * reverseThrust * Time.deltaTime);
            var frontMain = frontJet.main;
            frontMain.startRotationZ = (transform.rotation.eulerAngles.y + 180) * Mathf.Deg2Rad;
            frontJet.gameObject.SetActive(true);
        } else
        {
            frontJet.gameObject.SetActive(false);
        }

        if (turningRight && myGenerator.currentPower >= rcsEnergyCost)
        {
            myGenerator.currentPower -= rcsEnergyCost;
            //transform.Rotate(Vector3.up, rotateThrust * Time.deltaTime);
            myRigidBody.AddTorque(transform.up * rotateThrust * Time.deltaTime);
            var rjrMain = rightManuverJetRight.main;
            rjrMain.startRotationZ = (transform.rotation.eulerAngles.y - 90) * Mathf.Deg2Rad;
            var rjlMain = rightManuverJetLeft.main;
            rjlMain.startRotationZ = (transform.rotation.eulerAngles.y + 90) * Mathf.Deg2Rad;
            rightManuverJetRight.gameObject.SetActive(true);
            rightManuverJetLeft.gameObject.SetActive(true);

        }
        else
        {
            rightManuverJetRight.gameObject.SetActive(false);
            rightManuverJetLeft.gameObject.SetActive(false);
        }

        if (turningLeft && myGenerator.currentPower >= rcsEnergyCost)
        {
            myGenerator.currentPower -= rcsEnergyCost;
            //transform.Rotate(Vector3.down, rotateThrust * Time.deltaTime);
            myRigidBody.AddTorque(-transform.up * rotateThrust * Time.deltaTime);
            var ljlMain = leftManuverJetLeft.main;
            var ljrMain = leftManuverJetRight.main;
            ljlMain.startRotationZ = (transform.rotation.eulerAngles.y + 90) * Mathf.Deg2Rad;
            ljrMain.startRotationZ = (transform.rotation.eulerAngles.y - 90) * Mathf.Deg2Rad;
            leftManuverJetLeft.gameObject.SetActive(true);
            leftManuverJetRight.gameObject.SetActive(true);


        } else
        {
            leftManuverJetLeft.gameObject.SetActive(false);
            leftManuverJetRight.gameObject.SetActive(false);
        }      

        if (stopping && myGenerator.currentPower >= rcsEnergyCost)
        {
            myGenerator.currentPower -= rcsEnergyCost;
            var curSpeed = myRigidBody.velocity.magnitude;
            var newSpeed = curSpeed - reverseThrust / 20 * Time.deltaTime;

            if (newSpeed < .01)
            {
                newSpeed = 0;
            }

            myRigidBody.velocity = myRigidBody.velocity.normalized * newSpeed;
            //myRigidBody.angularVelocity = myRigidBody.angularVelocity * newSpeed;
        }

        //Debug.Log ("Speed = " + myRigidBody.velocity.magnitude);

    }

    private void LateUpdate()
    {
        if(transform.position.y != myYPos)
        {
            transform.position = new Vector3(transform.position.x, myYPos, transform.position.z);
        }

        if (transform.rotation.eulerAngles.x != 0)
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }
}
