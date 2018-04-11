using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    public GameObject shot;
    public int shotDamage;
    public LineRenderer lr;
    //public float shotSpeed;
    public Transform shotSpawn;
    public float fireRate;
    public float energyCost;
    public int slotID;
    public bool firing = false;
    public int turretRotationRate;
    public int turretRotationLimit;
    public Vector3 targetPos;
    public float fireAngle;
    public string weaponType;
    public int laserLength;

    delegate void FiringMode();
    FiringMode firingMode;
    private float nextFire;
    private ShipGenerator myShipGenerator;
    LineRenderer currentLaser;
    LayerMask myLayerMask;
    // Use this for initialization
    void Start()
    {
        myShipGenerator = this.GetComponentInParent<ShipGenerator>();
        myLayerMask = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {

        if (firing && Time.time >= nextFire && myShipGenerator.currentPower >= energyCost)
        {
            myShipGenerator.currentPower -= energyCost;
            firingMode();
        }
        Update_Rotation_Y();
    }

    public void SetFiringType()
    {
        if(weaponType == "projectile")
        {
            firingMode = FireProjectile;
        } else if (weaponType == "laser")
        {
            firingMode = FireLaser;
        } else if(weaponType == "missile")
        {
            firingMode = FireMissile;
        }
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

    public void FireProjectile()
    {
        nextFire = Time.time + fireRate;
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        Mover shotScript = shot.GetComponent<Mover>();
        shotScript.parentRidgidbody = GetComponentInParent<Rigidbody>();
        shotScript.shooter = transform.parent.gameObject;
        shotScript.damage = shotDamage;
    }

    public void FireLaser()
    {
        nextFire = Time.time + fireRate;
        //RaycastHit hit;
        currentLaser = Instantiate(lr, shotSpawn.transform.position, shotSpawn.rotation, shotSpawn.transform);
        Laser laserScript = currentLaser.GetComponent<Laser>();
        laserScript.damage = shotDamage;
        laserScript.fireRate = fireRate;
        laserScript.shotSpawn = shotSpawn;
        laserScript.laserLength = laserLength;
        laserScript.shooter = transform.parent.gameObject;

        Debug.DrawRay(shotSpawn.transform.position, shotSpawn.transform.up * laserLength, Color.red, fireRate);
        // todo for some reason this is extending out past the laserLength if I hit something
        //if (Physics.Raycast(shotSpawn.transform.position, shotSpawn.transform.up, out hit, laserLength, myLayerMask)){
        //    //Debug.Log(hit);
        //     if (hit.collider)
        //    {
        //        //Debug.Log("I hit something");
        //        currentLaser.SetPosition(1, hit.point);
        //        //if I hit something do damage to it
        //        if (hit.collider.tag == "Shield")
        //        {
        //            hit.collider.GetComponent<ShieldBehavior>().DamageShield(shotDamage, transform.parent.gameObject);
        //        }
        //        else if (hit.collider.tag == "AIShip" || hit.collider.tag == "Player")
        //        {
        //            hit.collider.GetComponent<Hull>().DoDamage(shotDamage, transform.parent.gameObject);
        //        }
        //    }
        //}            
    }

    //not yet implemented
    public void FireMissile()
    {
        //    nextFire = Time.time + fireRate;
        //    Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        //    Mover shotScript = shot.GetComponent<Mover>();
        //    shotScript.parentRidgidbody = GetComponentInParent<Rigidbody>();
        //    shotScript.firer = this.transform.parent.gameObject;
        //    shotScript.damage = shotDamage;
    }
}
