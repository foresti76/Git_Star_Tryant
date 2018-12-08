using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    public GameObject projectilePrefab;
    public GameObject missilePrefab;
    public GameObject laserLinePrefab;
    public Transform shotSpawn;
    public int shotDamage;
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
    public float speed;
    public float seek_rate;
    public float lifeTime = 25.0f;
    public int projectilesPerShot;
    public int maxAmmo;
    public int currentAmmo;
    public string weaponName;

    delegate GameObject FiringMode();
    FiringMode firingMode;
    private float nextFire;
    private Generator myShipGenerator;
    LineRenderer currentLaser;
    //LayerMask myLayerMask;  I might want this back if I am dynamically setting the layermask.
    private Radar myRadar;
    private int shotsLeft;
    HUD HUDScript;
    bool isPlayer = false;

    // Use this for initialization
    void Start()
    {
        myShipGenerator = this.GetComponentInParent<Generator>();
        //myLayerMask = gameObject.layer;
        myRadar = GetComponentInParent<Radar>();
        HUDScript = GameObject.Find("HUD").GetComponent<HUD>();
        if (transform.GetComponentInParent<Ship>().playerShip)
        {
            isPlayer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (firing && Time.time >= nextFire && myShipGenerator.currentPower >= energyCost && currentAmmo > 0)
        {
            myShipGenerator.currentPower -= energyCost;
            currentAmmo--;
            if (isPlayer)
            {
                HUDScript.UpdateAmmoDisplay(slotID, currentAmmo, maxAmmo);
            }
            firingMode();
        }
        Update_Rotation_Y();
    }

    public void SetFiringType()
    {
        if(weaponType == "projectile")
        {
            firingMode = FireProjectile;
        }
        else if (weaponType == "laser")
        {
            firingMode = FireLaser;
        }
        else if(weaponType == "missile")
        {
            firingMode = FireMissile;
        }
    }

    void Update_Rotation_Y()
    {  
        Vector3 turretPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 targetDir = targetPos - turretPos;
        float angle = Mathf.Atan2(targetDir.x, targetDir.y) * Mathf.Rad2Deg;
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

    public GameObject FireProjectile()
    {
        nextFire = Time.time + fireRate;
        GameObject shot = Instantiate(projectilePrefab, shotSpawn.position, shotSpawn.rotation);
        Mover shotScript = shot.GetComponent<Mover>();
        shotScript.shooter = this.gameObject;
        shotScript.shooter = transform.parent.gameObject;
        shotScript.damage = shotDamage;
        if(projectilesPerShot == 1)
        {
            return shot;
        }
        else if (projectilesPerShot == 2)
        {
            if (shotsLeft == 2)
            {
                shot.transform.rotation = Quaternion.Euler(shot.transform.eulerAngles.x, shot.transform.eulerAngles.y, shot.transform.eulerAngles.z + 10);
                Invoke("FireProjectile", 0.0f);
                shotsLeft--;
            }
            else
            {
                shot.transform.rotation = Quaternion.Euler(shot.transform.eulerAngles.x, shot.transform.eulerAngles.y, shot.transform.eulerAngles.z - 10);
                shotsLeft = projectilesPerShot;
            }
            return shot;
            
        }
        else if (projectilesPerShot == 3)
        {
            if (shotsLeft == 3)
            {
                Invoke("FireProjectile", 0.0f);
                shotsLeft--;
            }
            else if (shotsLeft == 2)
            {
                shot.transform.rotation = Quaternion.Euler(shot.transform.eulerAngles.x, shot.transform.eulerAngles.y, shot.transform.eulerAngles.z + 10);
                Invoke("FireProjectile", 0.0f);
                shotsLeft--;
            }
            else
            {
                shot.transform.rotation = Quaternion.Euler(shot.transform.eulerAngles.x, shot.transform.eulerAngles.y, shot.transform.eulerAngles.z - 10);
                shotsLeft = projectilesPerShot;
            }
            return shot;
        }
        else if (projectilesPerShot == 5)
        {
            if (shotsLeft == 5)
            {
                Invoke("FireProjectile", 0.0f);
                shotsLeft--;
            }
            else if (shotsLeft == 4)
            {
                shot.transform.rotation = Quaternion.Euler(shot.transform.eulerAngles.x, shot.transform.eulerAngles.y, shot.transform.eulerAngles.z + 10);
                Invoke("FireProjectile", 0.0f);
                shotsLeft--;
            }
            else if (shotsLeft == 3)
            {
                shot.transform.rotation = Quaternion.Euler(shot.transform.eulerAngles.x, shot.transform.eulerAngles.y, shot.transform.eulerAngles.z - 10);
                Invoke("FireProjectile", 0.0f);
                shotsLeft--;
            }
            else if (shotsLeft == 2)
            {
                shot.transform.rotation = Quaternion.Euler(shot.transform.eulerAngles.x, shot.transform.eulerAngles.y, shot.transform.eulerAngles.z - 20);
                Invoke("FireProjectile", 0.0f);
                shotsLeft--;
            }
            else
            {
                shot.transform.rotation = Quaternion.Euler(shot.transform.eulerAngles.x, shot.transform.eulerAngles.y, shot.transform.eulerAngles.z + 20);
                shotsLeft = projectilesPerShot;
            }
            return shot;
        }
        else
        {
            return shot;
        }
 
    }

    public GameObject FireLaser()
    {
        nextFire = Time.time + fireRate;
        //RaycastHit hit;
        GameObject currentLaser = Instantiate(laserLinePrefab, shotSpawn.transform.position, shotSpawn.rotation, shotSpawn.transform);
        Laser laserScript = currentLaser.GetComponent<Laser>();
        laserScript.damage = shotDamage;
        laserScript.fireRate = fireRate;
        laserScript.shotSpawn = shotSpawn;
        laserScript.laserLength = laserLength;
        laserScript.shooter = transform.parent.gameObject;
        currentAmmo = maxAmmo;
        return currentLaser;
    }

    //not yet implemented
    public GameObject FireMissile()
    {
        nextFire = Time.time + fireRate;
        GameObject missile = Instantiate(missilePrefab, shotSpawn.position, Quaternion.Euler(0,shotSpawn.eulerAngles.y,0));
        Missile missileScript = missile.GetComponent<Missile>();
        //missileScript.parentRidgidbody = GetComponentInParent<Rigidbody>();
        missileScript.shooter = this.transform.parent.gameObject;
        missileScript.damage = shotDamage;
        missileScript.speed = speed;
        missileScript.lifeTime = lifeTime;

        if (myRadar.targetLock && myRadar.target)
        {
            missileScript.seekRate = seek_rate;
            missileScript.target = myRadar.target;
        }
        else
        {
            missileScript.target = null;
        }

        return missile;
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
        shotsLeft = projectilesPerShot;
        if (isPlayer)
        {
            HUDScript.UpdateAmmoDisplay(slotID, currentAmmo, maxAmmo);
        }
    }
}
