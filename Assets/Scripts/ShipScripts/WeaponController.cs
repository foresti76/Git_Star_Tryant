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
    // Use this for initialization
    void Start()
    {
        myShipGenerator = this.GetComponentInParent<ShipGenerator>();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(shotSpawn.transform.position, shotSpawn.transform.up, Color.red);

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
        shotScript.firer = this.transform.parent.gameObject;
        shotScript.damage = shotDamage;
    }

    public void FireLaser()
    {
        nextFire = Time.time + fireRate;
        RaycastHit hit;
        currentLaser = Instantiate(lr, shotSpawn.transform.position, shotSpawn.rotation, shotSpawn.transform);
        currentLaser.GetComponent<Laser>().fireRate = fireRate;
        currentLaser.GetComponent<Laser>().shotSpawn = shotSpawn;
        currentLaser.GetComponent<Laser>().laserLength = laserLength;

        // not sure if this should be in a new object the laser itself
        if (Physics.Raycast(shotSpawn.transform.position, transform.up, out hit)){
             if (hit.collider)
            {
                currentLaser.SetPosition(1, hit.point);
                //if I hit something do damage to it
                if (hit.collider.tag == "Shield")
                {
                    hit.collider.GetComponent<ShieldBehavior>().DamageShield(shotDamage);
                }
                else if (hit.collider.tag == "AIShip" || hit.collider.tag == "Player")
                {
                    hit.collider.GetComponent<Hull>().DoDamage(shotDamage);
                }
            }
        }            
    }

    public void FireMissile()
    {
        nextFire = Time.time + fireRate;
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        Mover shotScript = shot.GetComponent<Mover>();
        shotScript.parentRidgidbody = GetComponentInParent<Rigidbody>();
        shotScript.firer = this.transform.parent.gameObject;
        shotScript.damage = shotDamage;
    }
}
