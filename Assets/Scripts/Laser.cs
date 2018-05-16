using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public Transform shotSpawn;
    public int laserLength;
    public float fireRate;
    public float damage;
    public GameObject shooter;
    float fireTime;

    LineRenderer lr;


    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        fireTime = Time.time + fireRate;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Time.time >= fireTime)
        {
            Destroy(this.gameObject);
        }

        RaycastHit hit;
        lr.SetPosition(0, shotSpawn.position);
 

        if (Physics.Raycast(shotSpawn.transform.position, transform.up, out hit, laserLength))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
           // Debug.Log("Hitting " + hit.collider.name);
            if (hit.collider.tag == "Shield")
            {
                hit.collider.GetComponent<Shield>().DamageShield(damage, shooter);
          //      Debug.Log("Hitting the shield");
            }
            else if(hit.collider.tag == "AIShip" || hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<Hull>().DoDamage(damage, shooter);
          //      Debug.Log("Hitting the Hull");
            }
        }
        else
        {
            lr.SetPosition(1, shotSpawn.transform.position + shotSpawn.transform.up * laserLength);
        }
        lr.enabled = true;
    }
}
