using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    public GameObject target;
    public int damage;
    public float speed;
    public float seekRate;
    public GameObject shooter;
    public float lifeTime;
    public ParticleSystem engineCore;
    public ParticleSystem engineFlare;

    private Rigidbody myRigidbody;
	// Use this for initialization
	void Start () {
        if (shooter)
        {
            myRigidbody = GetComponent<Rigidbody>();
            lifeTime = Time.time + lifeTime;
            myRigidbody.velocity = shooter.GetComponent<Rigidbody>().velocity;
        }
        else
        {
            Debug.Log("ERROR: a missile has been spawend without a shooter defined.");
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            var targetRot = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, seekRate * Time.deltaTime);
        }

        if (Time.time >= lifeTime)
        {
            Destroy(gameObject);
        }
        var coreMain = engineCore.main;
        var flareMain = engineFlare.main;
        coreMain.startRotationZ = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        flareMain.startRotationZ = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
    }

    void LateUpdate()
    {
        myRigidbody.velocity += transform.forward * speed;
    }
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Hitting " + other.name);
        if (other.gameObject.tag == "Shield")
        {
            other.gameObject.GetComponent<Shield>().DamageShield(damage, shooter);
            // Debug.Log("Hitting the shield");
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "AIShip" || other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Hull>().DoDamage(damage, shooter);
            //  Debug.Log("Hitting the Hull");
            Destroy(gameObject);
        }
    }
}
