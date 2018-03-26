using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;
    public float timeOutTime = 5.0f;
    public float damage;
    public GameObject firer;
    public Rigidbody parentRidgidbody;
	// Use this for initialization
	void Start () {
        Rigidbody shipRigidbody = parentRidgidbody;
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        //note this is uses the player ship for everything!!!
        rigidbody.velocity = shipRigidbody.velocity;
        rigidbody.velocity += transform.up * speed;
        timeOutTime = Time.time + timeOutTime;
	}
	
    void Update()
    {
        if(Time.time >= timeOutTime)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hitting " + other.name);
        if (other.tag == "Shield")
        {
            other.GetComponent<ShieldBehavior>().DamageShield(damage);
            Debug.Log("Hitting the shield");
            Destroy(gameObject);
        }
        else
        {
            other.GetComponent<Hull>().DoDamage(damage);
            Debug.Log("Hitting the Hull");
            Destroy(gameObject);
        }
    }
}
