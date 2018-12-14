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
    float damageMultiplier = 0.0f;
    LineRenderer lr;
    GameObject currentDamageTarget;
    GameObject lastDamageTarget;

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
            currentDamageTarget = hit.collider.gameObject;
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
            if (hit.collider.tag == "Shield")
            {
                hit.collider.GetComponent<Shield>().DamageShield(damage * (1.0f + damageMultiplier), shooter);
                CheckIfTargetChanged();
            }
            else if(hit.collider.tag == "AIShip" || hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<Hull>().DoDamage((1.0f + damageMultiplier), shooter);
                CheckIfTargetChanged();
            }
        }
        else
        {
            lr.SetPosition(1, shotSpawn.transform.position + shotSpawn.transform.up * laserLength);
            damageMultiplier = 0.0f;
        }
        lr.enabled = true;
    }

    void CheckIfTargetChanged()
    {
        if (currentDamageTarget == lastDamageTarget)
        {
            damageMultiplier += 0.1f;
        }
        else if(damageMultiplier > 0.0f)
        {
            damageMultiplier = 0.0f;
            lastDamageTarget = currentDamageTarget;
            Debug.Log("Damage Multiplier reset for new target object");
        }
    }
}
