using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;
    public float timeOutTime = 5.0f;
    public float damage;
    public string firer;
	// Use this for initialization
	void Start () {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        GameObject playerShip = GameObject.Find("vehicle_playerShip");
        Rigidbody shipRigidbody = playerShip.GetComponent<Rigidbody>();
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
        if(firer == "player")
        {
            return;
        }
        if (firer != "player")
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Hull>().DoDamage(damage);
                Debug.Log("Hitting the Hull");
                Destroy(gameObject);
            }
            if (other.tag == "Shield")
            {
                other.GetComponent<ShieldBehavior>().DamageShield(damage);
                Debug.Log("Hitting the shield");
                Destroy(gameObject);
            }
        }
    }
}
