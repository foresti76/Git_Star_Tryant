using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;
    public float timeOutTime = 5.0f;
    public float damage;
    public GameObject shooter;
    public Rigidbody parentRidgidbody;

    private Rigidbody rigidbody;

	// Use this for initialization
	void Awake ()
    {
        rigidbody = GetComponent<Rigidbody>();
	}

    void Start()
    {
        rigidbody.velocity = parentRidgidbody.velocity;
        rigidbody.velocity += transform.up * speed;
        timeOutTime = Time.time + timeOutTime;
    }

    void Update()
    {
        if (Time.time >= timeOutTime)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hitting " + other.name);
        if (other.tag == "Shield")
        {
            other.GetComponent<Shield>().DamageShield(damage, shooter);
           // Debug.Log("Hitting the shield");
            Destroy(gameObject);
        }
        else if (other.tag == "AIShip" || other.tag == "Player")
        {
            other.GetComponent<Hull>().DoDamage(damage, shooter);
          //  Debug.Log("Hitting the Hull");
            Destroy(gameObject);
        }
    }
}
