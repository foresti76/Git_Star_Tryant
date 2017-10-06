using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShipMovement : MonoBehaviour {
    public float engineThrust = 10f;
    public float reverseThrust = 5f;
    public float maxSpeed = 10f;
    public float rotateThrust = 10f;
    public ParticleSystem engineFlare;
    public ParticleSystem engineCore;
    public ParticleSystem rightManuverJetRight;
    public ParticleSystem rightManuverJetLeft;
    public ParticleSystem leftManuverJetLeft;
    public ParticleSystem leftManuverJetRight;
    public ParticleSystem frontJet;
    public GameObject shot;
    public int shotDamage;
    //public float shotSpeed;
    public Transform shotSpawn;
    public float fireRate;

    private Rigidbody myRigidBody;
    private float nextFire;
    

    // Use this for initialization
    void Start () {
        myRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            shot.GetComponent<Mover>().damage = shotDamage;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if(myRigidBody.velocity.magnitude >= maxSpeed)
        {
            myRigidBody.velocity = myRigidBody.velocity.normalized * maxSpeed;
        } 
        

        if (Input.GetKey(KeyCode.UpArrow))
        {
            myRigidBody.AddRelativeForce(Vector3.forward * engineThrust *Time.deltaTime);
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

        if (Input.GetKey(KeyCode.DownArrow))
        {
            myRigidBody.AddRelativeForce(Vector3.back * reverseThrust * Time.deltaTime);
            var frontMain = frontJet.main;
            frontMain.startRotationZ = (transform.rotation.eulerAngles.y + 180) * Mathf.Deg2Rad;
            frontJet.gameObject.SetActive(true);
        } else
        {
            frontJet.gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, rotateThrust * Time.deltaTime);
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

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down, rotateThrust * Time.deltaTime);
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
        

        if (Input.GetKey(KeyCode.Space))
        {
            var curSpeed = myRigidBody.velocity.magnitude;
            var newSpeed = curSpeed - reverseThrust / 20 * Time.deltaTime;

            if (newSpeed < .01)
            {
                newSpeed = 0;
            }

            myRigidBody.velocity = myRigidBody.velocity.normalized * newSpeed;
        }

        //Debug.Log ("Speed = " + myRigidBody.velocity.magnitude);

    }
}
