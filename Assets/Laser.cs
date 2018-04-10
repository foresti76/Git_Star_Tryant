using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public Transform shotSpawn;
    public int laserLength;
    public float fireRate;
    float fireTime;

    LineRenderer lr;


    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        fireTime = Time.time + fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= fireTime)
        {
            Destroy(this.gameObject);
        }

        RaycastHit hit;
        lr.SetPosition(0, shotSpawn.position);
 

        if (Physics.Raycast(shotSpawn.transform.position, transform.up, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else
        {
            lr.SetPosition(1, shotSpawn.transform.position + shotSpawn.transform.up * laserLength);
        }
        lr.enabled = true;
    }
}
