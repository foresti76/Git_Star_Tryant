using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    public GameObject target;
    public int damage;
    public float speed;
    public float seekRate;
    public GameObject shooter;

    private Rigidbody myRigidbody;
	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (target)
        {
            var targetRot = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, seekRate * Time.deltaTime);
        }
        myRigidbody.velocity = Vector3.forward * speed;
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
