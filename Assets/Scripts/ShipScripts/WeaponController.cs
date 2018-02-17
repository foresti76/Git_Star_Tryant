using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    public GameObject shot;
    public int shotDamage;
    //public float shotSpeed;
    public Transform shotSpawn;
    public float fireRate;
    public float energyCost;
    public int slotID;
    public bool firing = false;
    public int turretRotationRate;
    public float turretRotationLimit = 30;
    public Vector3 targetPos;
    public float fireAngle;

    private float nextFire;
    private ShipGenerator myShipGenerator; 
    // Use this for initialization
    void Start()
    {
        myShipGenerator = this.GetComponentInParent<ShipGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firing && Time.time >= nextFire && myShipGenerator.currentPower >= energyCost)
        {
            myShipGenerator.currentPower -= energyCost;
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Mover shotScript = shot.GetComponent<Mover>();
            shotScript.damage = shotDamage;
            shotScript.firer = "player";
        }
        Update_Rotation_Y();
    }

    void Update_Rotation_Y()
    {  
        Vector3 turretPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 targetDir = targetPos - turretPos;
       // Debug.Log("Target Dir:" + targetDir);
        float angle = Mathf.Atan2(targetDir.x, targetDir.y) * Mathf.Rad2Deg;
        //get angle of ship and compare and clamp
        float angleDelta = angle - transform.parent.eulerAngles.y;

        if (angleDelta < -180)
        {
            angleDelta += 360;
        }

        if (angleDelta > turretRotationLimit)
        {
            angleDelta = turretRotationLimit;
        }
        else if (angleDelta < -turretRotationLimit)
        {
            angleDelta = -turretRotationLimit;
        }

        fireAngle = transform.parent.eulerAngles.y + angleDelta;

        Quaternion targetRot = Quaternion.Euler(0, fireAngle, 0);
        Quaternion rot = Quaternion.Euler(transform.eulerAngles);

        // turn the turret in the desired direction scaled by the turret rotation dampaning value.
        transform.rotation = Quaternion.RotateTowards(rot, targetRot, turretRotationRate * Time.deltaTime);

    }
}
