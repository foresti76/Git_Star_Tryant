using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int turretRotationRate = 1;
    public float turretRotationLimit = 360;
    public float firingArc;
    public Transform shotSpawn;
    public float fireRate;
    public GameObject shot;
    public int shotDamage = 10;

    private float nextFire;
    private GameObject player;
    Quaternion targetRot;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        this.update_rotation_y();

        float angleToGetTo = targetRot.eulerAngles.y;
        float currentAngle = transform.rotation.eulerAngles.y;

        if (angleToGetTo <= currentAngle + firingArc && angleToGetTo >= currentAngle - firingArc)
        {
            Fire();
        }
    }

    void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            shot.GetComponent<Mover>().damage = shotDamage;
        }
    }
    void update_rotation_y()
    {

        Vector3 playerPos = player.transform.position;

        Vector3 turretPos = transform.parent.position;
        Vector3 targetDir = playerPos - turretPos;
        
        targetRot = Quaternion.LookRotation(targetDir);
        
         //Figure out how to clamp the angle and get values higher than 180 or -1
        Vector3 angle = targetRot.eulerAngles;//Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        //get angle of enemy ship and compare and clamp
        
        float angleDelta = angle.y - transform.parent.eulerAngles.y;
        

       if (angleDelta < -180)
        {
            angleDelta += 360;
        }
       // there is a bug here where it will always go to the upper limit
        if (angleDelta > turretRotationLimit)
        {
            angleDelta = turretRotationLimit;
        }
        else if (angleDelta < -turretRotationLimit)
        {
            angleDelta = -turretRotationLimit;
        }

        float newAngle = transform.parent.eulerAngles.y + angleDelta;
        
        
        Quaternion clampedTargetRot = Quaternion.Euler(0, newAngle, 0);
        
        //Quaternion rot = Quaternion(transform.eulerAngles);
        
        // turn the turret in the desired direction scaled by the turret rotation dampaning value.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, clampedTargetRot, turretRotationRate * Time.deltaTime);

    }
}
